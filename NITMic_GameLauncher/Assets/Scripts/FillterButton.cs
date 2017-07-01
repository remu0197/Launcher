using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class FillterButton : MonoBehaviour
{
    private RectTransform m_MyRect;
    private Vector2 m_DefaultSize;
    private Vector3 m_DefaultPos;

    [SerializeField]
    private GameObject Marker;

    public void OnMouseOver()
    {
        m_MyRect.sizeDelta = new Vector2(Mathf.Lerp(m_MyRect.sizeDelta.x, m_DefaultSize.x * 2, 0.3f), m_DefaultSize.y);

        this.gameObject.transform.position = Vector3.Lerp(
            this.gameObject.transform.position,
            new Vector3(Marker.transform.position.x, m_DefaultPos.y, m_DefaultPos.z),
            0.3f
            );
    }

    public void OnMouseExit()
    {
        GameObject child = transform.Find("Categories").gameObject;
        if(!child)
        {
            Debug.Log("Error");
        }
        child.SetActive(false);
    }

    private bool IsPointerOver()
    {
        EventSystem current = EventSystem.current;
        if(current != null)
        {
            if(current.IsPointerOverGameObject())
            {
                return true;
            }
        }
        return false;
    }

	// Use this for initialization
	void Start ()
    {
        m_MyRect = this.GetComponent<RectTransform>();
        m_DefaultSize = m_MyRect.sizeDelta;
        m_DefaultPos = this.gameObject.transform.position;

        Debug.Log("test");
    }

    // Update is called once per frame
    void Update ()
    {
        //if (this.OnMouseEnter())
        //{
        //    m_MyRect.sizeDelta = new Vector2(Mathf.Lerp(m_MyRect.sizeDelta.x, m_DefaultSize.x * 2, 0.3f), m_DefaultSize.y);

        //    this.gameObject.transform.position = Vector3.Lerp(
        //        this.gameObject.transform.position,
        //        new Vector3(Marker.transform.position.x, m_DefaultPos.y, m_DefaultPos.z),
        //        0.3f
        //        );
        //}
        //else if (this.OnMouseExit())
        //{
        //    this.gameObject.transform.position = m_DefaultPos;
        //    m_MyRect.sizeDelta = m_DefaultSize;
        //}
    }
}
