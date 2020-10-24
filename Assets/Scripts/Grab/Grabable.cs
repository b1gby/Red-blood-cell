using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{

    public GameObject player;

    private GameObject root;

    private bool isChild = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        root = transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position,this.transform.position) <= 0.8f && 
            (Input.GetButtonDown("Joystick X") || Input.GetKeyDown(KeyCode.E)))
        {
            this.transform.SetParent(player.transform);
            this.transform.localPosition = new Vector2(0, 0.1f);
            isChild = true;
            
        }
        if(isChild && 
            (Input.GetButtonUp("Joystick X") || Input.GetKeyUp(KeyCode.E)))
        {
            //this.transform.SetParent(player.transform.parent);
            BodyBase bb = new BodyBase();
            PolygonCollider2D[] bodyAreasCol = root.GetComponentsInChildren<PolygonCollider2D>();
            for(int i=0; i<bodyAreasCol.GetLength(0);i++)
            {
                if(bb.isPolygonContainsPoint( bodyAreasCol[i].points,this.transform.position-bodyAreasCol[i].transform.position))
                {
                    this.transform.SetParent(bodyAreasCol[i].transform.parent);
                    break;
                }
            }
        }
    }
}
