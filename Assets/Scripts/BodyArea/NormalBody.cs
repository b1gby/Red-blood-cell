using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBody : BodyBase
{
    // Start is called before the first frame update
    void Start()
    {
        
        Init();
        InvokeRepeating("InsCO2", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InsCO2()
    {
        Vector2 RanPoint = GetRandomInCol();
        GameObject go = Instantiate(CO2,new Vector3(RanPoint.x,RanPoint.y,0) + this.transform.position, Quaternion.identity,this.transform.parent);
        go.transform.localScale = new Vector3(
            Mathf.Abs(go.transform.localScale.x),
            go.transform.localScale.y,
            go.transform.localScale.z); 
    }

    public void InsVirus()
    {
        Vector2 RanPoint = GetRandomInCol();
        GameObject go = Instantiate(Virus, new Vector3(RanPoint.x, RanPoint.y, 0) + this.transform.position, Quaternion.identity, this.transform.parent);
        go.transform.localScale = new Vector3(
            Mathf.Abs(go.transform.localScale.x),
            go.transform.localScale.y,
            go.transform.localScale.z);
    }
}
