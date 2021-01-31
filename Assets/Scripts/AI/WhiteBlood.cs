using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WhiteBlood : MonoBehaviour
{
    public GameObject target;

    public float speed = 200f;

    public float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private bool isStayBody = false;
    private float timeStayBody = 5f;

    public bool isGoWork = false;
    

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        target = ValueManager.Instance.GetNextRandomNormalBody();
    }


    void Update()
    {
        if (ValueManager.Instance.VirusList.Count > 0 && !isGoWork)
        {
            GameObject go = ValueManager.Instance.GetRandomVirus(this.gameObject);
            if (go != null)
            {
                target = go;
                isGoWork = true;
            }
            else
                return;
        }
        else
        {
            SwitchBodyArea();
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count && !isStayBody)
        {
            isStayBody = true;
            timeStayBody = 5f;
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void UpdatePath()
    {
        if (target)
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void SwitchBodyArea()
    {
        
        if (isStayBody)
        {
            timeStayBody -= Time.deltaTime;
            if(timeStayBody<=0)
            {
                target = ValueManager.Instance.GetNextRandomNormalBody();
                isStayBody = false;
            }
        }
    }



    GameObject FindNearestO2()
    {
        GameObject ngo = null;
        float max = 1000;
        foreach (GameObject go in ValueManager.Instance.O2List)
        {
            if (Vector3.Distance(go.transform.position, this.transform.position) < max && go.GetComponent<Grabable>().Grabber == null)
            {
                max = Vector3.Distance(go.transform.position, this.transform.position);
                ngo = go;
            }
        }
        return ngo;
    }


    GameObject FindNearestCO2()
    {
        GameObject ngo = null;
        float max = 1000;
        foreach (GameObject go in ValueManager.Instance.CO2List)
        {
            if (Vector3.Distance(go.transform.position, this.transform.position) < max)
            {
                max = Vector3.Distance(go.transform.position, this.transform.position);
                ngo = go;
            }
        }
        return ngo;
    }
}