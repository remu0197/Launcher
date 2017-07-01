using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameDataManger : MonoBehaviour
{
    private List<GameData> GameDataList = new List<GameData>();
    private List<GameData> ShowDataList = new List<GameData>();

    [SerializeField]
    private GameObject ScrollBar;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private List<GameObject> Markers;

    bool m_isClickedOnBar = false;

	// Use this for initialization
	void Awake ()
    {
        //Debug
        List<string> FileList = new List<string>(System.IO.Directory.GetDirectories(Application.dataPath + "/../Games", "*", System.IO.SearchOption.TopDirectoryOnly));
        //Release
        //List<string> FileList = new List<string>(System.IO.Directory.GetDirectories(Application.dataPath + "../Games", "*", System.IO.SearchOption.TopDirectoryOnly));
        
        for(int index = 0; index < FileList.Count; ++index)
        {
            StreamReader sr = new StreamReader(System.IO.Directory.GetFiles(FileList[index] + "/GameData", "*.txt", System.IO.SearchOption.TopDirectoryOnly)[0], Encoding.UTF8);

            //FileInfo exe = new FileInfo(System.IO.Directory.GetFiles(FileList[index], "*.exe", System.IO.SearchOption.AllDirectories)[0]);
            //string exeFile = FileList[index] + "\\" + exe.ToString();
            //exeFile = exeFile.Replace("/", "\\");

            GameDataList.Add(new GameData(
                sr.ReadLine().Replace("タイトル：", ""),
                sr.ReadLine().Replace("作成された年：", ""),
                sr.ReadLine().Replace("ジャンル：", ""),
                sr.ReadLine().Replace("開発エンジン：", ""),
                sr.ReadToEnd().Replace("説明：", ""),
                "",//exeFile,
                prefab,
                new Vector3(-200 + (index % 4) * 300, 175 - (index / 4) * 300, 0),
                this.transform
                )
            );
        }

        ShowDataList = GameDataList;

        Debug.Log(GameDataList[0].GetGameData(textDataCategory.title));
	}
	
	// Update is called once per frame
	void Update ()
    {
        //あとで別の場所に
        getClickObject();
        if (m_isClickedOnBar)
        {
            //クリック時の処理
            Vector3 temp = ScrollBar.transform.position;
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(IsBarTouchMarker(0) && tapPoint.y - temp.y >= 0)
            {
                tapPoint.y = temp.y;
            }
            else if (IsBarTouchMarker(1) && tapPoint.y - temp.y <= 0)
            {
                tapPoint.y = temp.y;
            }

            ScrollBar.transform.position = new Vector3(temp.x, tapPoint.y, temp.z);

            for (int index = 0; index < GameDataList.Count; ++index)
            {
                GameDataList[index].MoveIcon(temp.y - ScrollBar.transform.position.y);
            }
        }
	}

    //おなじく
    private GameObject getClickObject()
    {
        GameObject result = null;
        // 左クリックされた場所のオブジェクトを取得
        if (Input.GetMouseButton(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
                result = collition2d.transform.gameObject;
            }
            if(result == ScrollBar)
            {
                m_isClickedOnBar = true;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_isClickedOnBar = false;
        }
        return result;
    }

    private bool IsBarTouchMarker(int index)
    {
        Collider2D collider = Physics2D.OverlapPoint(Markers[index].transform.position);
        if(collider != null && collider.transform.gameObject == ScrollBar)
        {
            return true;
        }

        return false;
    }

    public void SetShowDataList()
    {
        ShowDataList.Clear();
        int j = 0;
        for(int i = 0; i < GameDataList.Count; ++i)
        {
            if (GameDataList[i].GetGameData(textDataCategory.year) == "2016")
            {
                GameDataList[i].SetIcon(false, j);
                ShowDataList.Add(GameDataList[i]);
                j++;
            }
            else
            {
                GameDataList[i].SetIcon(true);
            }
        }
    }
}
