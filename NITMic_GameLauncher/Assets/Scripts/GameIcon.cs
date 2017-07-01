using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIcon : MonoBehaviour
{
    private string m_dataName;

    public void SetDataName(string dataName)
    {
        m_dataName = dataName;
    }
    
    public void SetLaunchGame()
    {
        LaunchManager.SetLaunchGame(m_dataName);
    }
}
