using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour {

    enum Pos
    {
        Top,
        Bottom
    }

    [SerializeField]
    GameObject explanationText;

    [SerializeField]
    GameObject[] Markers;

    [SerializeField]
    GameObject ScrollBar;
	
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update ()
    {
        if(ScrollBar == GetClickObject())
        {
            Vector2 BarPos = ScrollBar.transform.position;
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(!IsBarGetTo(Pos.Top) && tapPoint.y - BarPos.y >= 0)
            {
                BarPos.y = tapPoint.y;
                Debug.Log("ok");
            }
            else if(!IsBarGetTo(Pos.Bottom) && tapPoint.y - BarPos.y <= 0)
            {
                BarPos.y = tapPoint.y;
            }

            ScrollBar.transform.position = BarPos;
        }
	}

    private GameObject GetClickObject()
    {
        GameObject result = null;

        if(Input.GetMouseButton(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collision = Physics2D.OverlapPoint(tapPoint);

            if(collision)
            {
                result = collision.transform.gameObject;
                //Debug.Log("ok");
            }
        }

        return result;
    }

    private bool IsBarGetTo(Pos pos)
    {
        Collider2D collision = Physics2D.OverlapPoint(Markers[(int)pos].transform.position);
        if(collision && collision.transform.gameObject == ScrollBar)
        {
            return true;
        }

        return false;
    }
}
