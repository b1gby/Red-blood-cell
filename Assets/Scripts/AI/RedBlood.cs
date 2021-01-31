using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RedBlood : MonoBehaviour
{
    private GameObject target;

    public float speed = 200f;

    public float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private bool isDoFirstWork = false;
    private bool isDoSecondWork = false;
    private bool isDragFirstGO = false;
    private bool isDragSecondGO = false;


    GameObject tmp_O2 = null;
    GameObject tmp_CO2 = null;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        InvokeRepeating("OnceLoop", 0f, 5f);
    }


    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (path == null)
            return;
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

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void UpdatePath()
    {
        if(target)
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

    void OnceLoop()
    {
        if (ValueManager.Instance.O2List.Count > 0 && !isDoFirstWork)
        {
            target = FindNearestO2();
            isDoFirstWork = true;
        }
        else if (!isDragFirstGO)
        {
            try
            {
                tmp_O2 = target;
                if (Vector3.Distance(target.transform.position, this.transform.position) < 0.8f)
                {
                    if (tmp_O2.GetComponent<Grabable>().Grabber == null)
                    {
                        tmp_O2.GetComponent<Grabable>().GrabIt(this.transform);
                        tmp_O2.GetComponent<Grabable>().isNotme = true;
                        target = ValueManager.Instance.GetWorstBody();
                        isDragFirstGO = true;
                    }
                    else
                    {
                        returnInitState();
                    }
                }
            }
            catch
            {
                returnInitState();
            }
        }
        else if (isDragFirstGO && !isDoSecondWork)
        {
            try
            {
                if (reachedEndOfPath && !isDoSecondWork)
                {
                    tmp_O2.GetComponent<Grabable>().DropIt();
                    tmp_O2.GetComponent<Grabable>().isNotme = false;
                    target = FindNearestCO2();

                    isDoSecondWork = true;
                }
            }
            catch
            {
                returnInitState();
            }
            
        }
        else if (isDoSecondWork && !isDragSecondGO)
        {
            try
            {
                tmp_CO2 = target;
                if (Vector3.Distance(target.transform.position, this.transform.position) < 0.8f)
                {
                    if (tmp_CO2.GetComponent<Grabable>().Grabber == null)
                    {
                        tmp_CO2.GetComponent<Grabable>().GrabIt(this.transform);
                        tmp_CO2.GetComponent<Grabable>().isNotme = true;
                        target = ValueManager.Instance.GetLungBody();
                        isDragSecondGO = true;
                    }
                    else
                    {
                        returnInitState();
                    }
                }
            }
            catch
            {
                returnInitState();
            }
        }
        else if (isDragSecondGO)
        {
            if (reachedEndOfPath)
            {
                tmp_CO2.GetComponent<Grabable>().DropIt();
                tmp_CO2.GetComponent<Grabable>().isNotme = false;

                isDragFirstGO = false;
                isDragSecondGO = false;
                isDoFirstWork = false;
                isDoSecondWork = false;
            }
        }
    }

    GameObject FindNearestO2()
    {
        GameObject ngo = null;
        float max = 1000;
        foreach(GameObject go in ValueManager.Instance.O2List)
        {
            if(Vector3.Distance(go.transform.position,this.transform.position)<max && go.GetComponent<Grabable>().Grabber == null)
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
            if (Vector3.Distance(go.transform.position, this.transform.position) < max && go.GetComponent<Grabable>().Grabber == null)
            {
                max = Vector3.Distance(go.transform.position, this.transform.position);
                ngo = go;
            }
        }
        return ngo;
    }

    void returnInitState()
    {
        path = null;
        isDragFirstGO = false;
        isDragSecondGO = false;
        isDoFirstWork = false;
        isDoSecondWork = false;
    }
}