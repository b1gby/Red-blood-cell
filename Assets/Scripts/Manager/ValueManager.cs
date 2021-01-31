using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    public static ValueManager Instance = null;
    private UIManager uiManager = null;
    public List<GameObject> O2List = new List<GameObject>();
    public List<GameObject> CO2List = new List<GameObject>();
    public List<GameObject> BodyList = new List<GameObject>();
    public List<GameObject> RedCellList = new List<GameObject>();
    public List<GameObject> WhiteCellList = new List<GameObject>();
    public List<GameObject> VirusList = new List<GameObject>();

    public void Init()
    {
        Instance = this;
        uiManager = UIManager.Instance;
        Debug.Log("Init ValueManager...");
    }

    public GameObject GetWorstBody()
    {
        int min_Health = 9999;
        int index = 0;
        for (int i = 0; i < BodyList.Count; i++)
        {
            if(BodyList[i].transform.parent.name.Contains("lung"))
            {
                continue;
            }
            if (BodyList[i].transform.parent.GetComponent<Health>().health < min_Health)
            {
                min_Health = BodyList[i].transform.parent.GetComponent<Health>().health;
                index = i;
            }
        }
        return BodyList[index];
    }

    public GameObject GetLungBody()
    {
        
        return BodyList[0];
    }


    public GameObject GetNextRandomNormalBody()
    {
        int ran = Random.Range(1, 6);
        return BodyList[ran];
    }

    public GameObject GetRandomVirus(GameObject source)
    {
        foreach(GameObject go in ValueManager.Instance.VirusList)
        {
            if(go.GetComponent<Virus>().BelongWhiteCell == null)
            {
                go.GetComponent<Virus>().BelongWhiteCell = source;
                return go;
            }
        }
        return null;
    }
}
