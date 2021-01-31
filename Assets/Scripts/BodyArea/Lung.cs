using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lung : BodyBase
{
    // Start is called before the first frame update
    void Start()
    {

        Init();
        InvokeRepeating("InsO2", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //this.ge
    }

    void InsO2()
    {
        Vector2 RanPoint = GetRandomInCol();
        GameObject go = Instantiate(O2, new Vector3(RanPoint.x, RanPoint.y, 0) + this.transform.position, Quaternion.identity, this.transform.parent);
        go.transform.localScale = new Vector3(
            Mathf.Abs(go.transform.localScale.x),
            go.transform.localScale.y,
            go.transform.localScale.z);
        ValueManager.Instance.O2List.Add(go);
    }

}
