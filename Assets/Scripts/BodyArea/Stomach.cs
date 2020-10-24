using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach : BodyBase
{
    public GameObject Resist;
    public GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        InvokeRepeating("InsCO2", 0f, 5f);
        InvokeRepeating("RandomEvent", 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InsCO2()
    {
        Vector2 RanPoint = GetRandomInCol();
        GameObject go = Instantiate(CO2, new Vector3(RanPoint.x, RanPoint.y, 0) + this.transform.position, Quaternion.identity, this.transform.parent);

        go.transform.localScale = new Vector3(
           Mathf.Abs(go.transform.localScale.x),
           go.transform.localScale.y,
           go.transform.localScale.z);
    }

    void RandomEvent()
    {
        float ran = Random.Range(0f, 1f);
        //生成病毒细菌
        if (ran>0.4)
        {
            Transform ranBody = tile.transform.GetChild((int)Random.Range(0, 6));
            if(ranBody.name.Contains("lung"))
            {
                ranBody.GetComponentInChildren<Lung>().InsVirus();
            }
            else if(ranBody.name.Contains("stomach"))
            {
                Vector2 RanPoint = ranBody.GetComponentInChildren<Stomach>().GetRandomInCol();
                GameObject go = Instantiate(Virus, new Vector3(RanPoint.x, RanPoint.y, 0) + this.transform.position, Quaternion.identity, this.transform.parent);

                go.transform.localScale = new Vector3(
                   Mathf.Abs(go.transform.localScale.x),
                   go.transform.localScale.y,
                   go.transform.localScale.z);
            }
            else
            {
                ranBody.GetComponentInChildren<NormalBody>().InsVirus();
            }
           
        }
        //生成抗生物质
        else
        {
            Vector2 RanPoint = GetRandomInCol();
            Instantiate(Resist, new Vector3(RanPoint.x, RanPoint.y, 0) + this.transform.position, Quaternion.identity, this.transform.parent);
        }
    }
}
