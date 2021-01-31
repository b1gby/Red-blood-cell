using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public GameObject player;

    private GameObject root;

    private bool isGrabStatusChanged = false;
    private float GrabStatusChangedTime = 0.5f;
    private bool isChild = false;

    Dictionary<Transform, Vector2[]> bodyAreasCol;

    public bool isNotme = false;

    public GameObject Grabber = null;

    void Awake()
    {
        bodyAreasCol = GameObject.FindGameObjectWithTag("Level").GetComponent<Level1>().BBCol;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        root = transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

        if(Vector2.Distance(player.transform.position,this.transform.position) <= 0.8f && 
            (Input.GetButton("Joystick X") || Input.GetKey(KeyCode.E)))
        {
            if(!isChild && !isGrabStatusChanged)
            {
                isChild = true;
                isGrabStatusChanged = true;
                GrabStatusChangedTime = 0.5f;
            }
               
            else if(isChild && !isGrabStatusChanged)
            {
                isChild = false;
                isGrabStatusChanged = true;
                GrabStatusChangedTime = 0.5f;
            }
        }

        // 放下
        if (!isChild && !isNotme)
        {
            DropIt();
        }
        // 抓取
        else if(!isNotme)
        {
            GrabIt(player.transform);
        }
    }

    void Timer()
    {
        //抓取状态改变
        if(isGrabStatusChanged)
        {
            GrabStatusChangedTime-=Time.deltaTime;
            if(GrabStatusChangedTime <= 0)
            {
                isGrabStatusChanged = false;
            }
        }
    }

    public void GrabIt(Transform source)
    {
        if(Grabber == null)
        {
            this.transform.SetParent(source);
            this.transform.localPosition = new Vector2(0, 0.1f);
            this.Grabber = source.gameObject;
        }
    }

    public void DropIt()
    {
        foreach (KeyValuePair<Transform, Vector2[]> bc in bodyAreasCol)
        {
            Vector2 point = this.transform.position - bc.Key.position;

            if (BodyBase.isPolygonContainsPoint(bc.Value, point))
            {
                this.transform.SetParent(bc.Key.parent);
                break;
            }
        }
        this.Grabber = null;
    }
}
