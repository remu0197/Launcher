using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    private GameObject x;

	// Use this for initialization
    // ohyearaaaaa
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject obj = getClickObject();
        if(obj != null)
        {
            GameDataManger gm = x.GetComponent<GameDataManger>();
            Debug.Log((gm != null));
            gm.SetShowDataList();
        }
	}

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
        }
        return result;
    }
}
