using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class LaunchManager : MonoBehaviour
{
    public struct DetailedData
    {
        public string TitleName;
        public string ExplanationText;
        public string ExePath;
        public GameObject GameIcon;

        public DetailedData(string titleName, string explanationText, string exePath, GameObject gameIcon)
        {
            TitleName = titleName;
            ExplanationText = explanationText;
            ExePath = exePath;
            GameIcon = gameIcon;
        }
    };


    System.Diagnostics.Process exProcess;

    static List<DetailedData> GameData = new List<DetailedData>();

    [SerializeField]
    static Text GameTitle;

    [SerializeField]
    static Text GameInfoText;

    [SerializeField]
    static GameObject PreviewPanel;

    [SerializeField]
    GameObject IconPrefab;

    [SerializeField]
    GameObject Marker;

    [SerializeField]
    static GameObject Marker2;

    static bool m_isReadyToLaunch;
    static DetailedData previewGame;

    private void Awake()
    {
        PreviewPanel = transform.Find("PreviewPanel").gameObject;
        Marker2 = transform.Find("Marker2").gameObject;

        GameTitle = PreviewPanel.transform.Find("GameTitle").GetComponent<Text>();
        GameInfoText = PreviewPanel.transform.Find("ScrollBar").transform.Find("ExplanationText").GetComponent<Text>();

        Debug.Log(GameTitle.text);

        //File Read Function
        string Path = Application.dataPath + "/../Games";
        Debug.Log(Path);

        List<string> FileList = new List<string>(System.IO.Directory.GetDirectories(Path, "*", System.IO.SearchOption.TopDirectoryOnly));
        Debug.Log(FileList[0]);

        for(int i = 0; i < 1; ++i)
        {
            string TextPath = System.IO.Directory.GetFiles(FileList[i] + "/GameData", "*", System.IO.SearchOption.TopDirectoryOnly)[0];
            string ExePath = System.IO.Directory.GetFiles(FileList[i], "*.exe", System.IO.SearchOption.AllDirectories)[0];

            StreamReader TextReader = new StreamReader(TextPath, Encoding.ASCII);

            GameObject GameIcon = (GameObject)Instantiate(
                IconPrefab,
                Marker.transform.position,
                Quaternion.identity
                );

            string title = TextReader.ReadLine();

            //行数カウントがいる
            string explanation = "";
            int explanationLineCount = 0;

            string temp = TextReader.ReadLine();

            while(temp != null)
            {
                ++explanationLineCount;
                explanation += temp + "\n";
                temp = TextReader.ReadLine();
            }

            TextReader.ReadToEnd();

            GameIcon.GetComponent<GameIcon>().SetDataName(title);
            GameIcon.transform.SetParent(this.transform, false);

            GameData.Add(new DetailedData(
                title,
                explanation,
                ExePath,
                GameIcon
                ));
        }
    }

    // Use this for initialization
    void Start ()
    {
        m_isReadyToLaunch = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //launcher終了
            Application.Quit();
        }

        if(exProcess != null && exProcess.HasExited)
        {
            //実行中のプロセスの停止
            exProcess.Dispose();
            exProcess = null;
        }
	}

    public void Launch()
    {
        if (exProcess == null)
        {
            //説明を表示させた後、起動
            exProcess = new System.Diagnostics.Process();

            exProcess.StartInfo.FileName = previewGame.ExePath;

            exProcess.Start();
        }
    }

    public void BackToList()
    {
        m_isReadyToLaunch = false;
        PreviewPanel.SetActive(false);

        previewGame.GameIcon.transform.position = Marker.transform.position;
    }

    public static bool SetLaunchGame(string dataName)
    {
        m_isReadyToLaunch = true;
        PreviewPanel.SetActive(true);

        previewGame = GameData[0];

        previewGame.GameIcon.transform.position = Marker2.transform.position;

        GameTitle.text = previewGame.TitleName;
        GameInfoText.text = previewGame.ExplanationText;

        return true;
    }
}
