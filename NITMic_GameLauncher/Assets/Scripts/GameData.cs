using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum textDataCategory
{
    title,
    year,
    zenre,
    engine,
    explain
}

public class GameData : MonoBehaviour
{
    private List<string> TextData = new List<string>
    {
        "",
        "",
        "",
        "",
        ""
    };

    private string m_ExePath;
    private GameObject m_Icon;

    public GameData(string TitleName, string CreatedYear, string ZenreText, string GameEngine, string ExplainText, string ExePath, GameObject prefab, Vector3 Pos, Transform ParentTransform)
    {
        TextData[(int)textDataCategory.title] = TitleName;
        TextData[(int)textDataCategory.year] = CreatedYear;
        TextData[(int)textDataCategory.zenre] = ZenreText;
        TextData[(int)textDataCategory.engine] = GameEngine;
        TextData[(int)textDataCategory.explain] = ExplainText;
        m_ExePath = ExePath;
        m_Icon = (GameObject)Instantiate(
            prefab,
            Pos,
            Quaternion.identity
            );

        m_Icon.transform.SetParent(ParentTransform, false);
    }

    public string GetGameData(textDataCategory category)
    {
        return TextData[(int)category];
    }

    public string GetExePath()
    {
        return m_ExePath;
    }

    public void MoveIcon(float movingAmount)
    {
        Vector3 pos = m_Icon.transform.position;
        m_Icon.transform.position = new Vector3(pos.x, pos.y + movingAmount, pos.z);
    }

    public void SetIcon(bool isActive, int index = 0)
    {
        m_Icon.SetActive(isActive);
        if (isActive)
        {
            m_Icon.transform.position = new Vector3(-200 + (index % 4) * 300, 175 - (index / 4) * 300, 0);
        }
    }
}
