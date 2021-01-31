using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelBase
{
    public Dictionary<Transform, Vector2[]> BBCol;

    private int randomEventNum = 0;

    public GameObject RedCell;
    public GameObject WhiteCell;

    void Awake()
    {
        BBCol = new Dictionary<Transform, Vector2[]>();
        PolygonCollider2D[] bodyAreasCol = Tile.GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D bc in bodyAreasCol)
        {
            BBCol.Add(bc.transform.parent, bc.points);
        }
        
        //foreach (Vector2[] col in BBCol.Values)
        //{
        //    for (int i = 0; i < col.GetLength(0); i++)
        //    {
        //        col[i].x *= 2;
        //        col[i].y *= 2;
        //    }
        //}

        Health[] healthList = Tile.GetComponentsInChildren<Health>();
        foreach(Health h in healthList)
        {
            ValueManager.Instance.BodyList.Add(h.transform.GetChild(0).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        randomEventNum = 0;
        InvokeRepeating("RandomEvent", 0, 30);
        InsRedCell();
        InsWhiteCell();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //生成红细胞
    void InsRedCell()
    {
        for(int i=0;i<5;i++)
        {
            Transform ranBody = Tile.transform.GetChild((int)Random.Range(0, 6));

            //如果在lung里
            if (ranBody.name.Contains("lung"))
            {
                ranBody.GetComponentInChildren<Lung>().InsRedCell();
            }
            else
            {
                ranBody.GetComponentInChildren<NormalBody>().InsRedCell();
            }
        }
           
    }

    //生成白细胞
    void InsWhiteCell()
    {
        Transform ranBody = Tile.transform.GetChild((int)Random.Range(0, 6));

        for (int i = 0; i < 2; i++)
        {
            //如果在lung里
            if (ranBody.name.Contains("lung"))
            {
                ranBody.GetComponentInChildren<Lung>().InsWhiteCell();
            }
            else
            {
                ranBody.GetComponentInChildren<NormalBody>().InsWhiteCell();
            }
        }
    }

    void RandomEvent()
    {
        string eventName = "";
        if (randomEventNum == 0)
        {
            eventName = "无事情发生";
        }
        else if (randomEventNum == 1)
        {
            eventName = "生成病毒";
            InsVirusEvent();
        }
        else
        {
            int ran = Random.Range(0, 5);
            
            switch (ran)
            {
                case 0:
                    eventName = "无事情发生";
                    break;
                case 1:
                    //生成病毒
                    InsVirusEvent();
                    eventName = "生成病毒";
                    break;
                case 2:
                    //生成抗生素
                    InsResistEvent();
                    eventName = "生成抗生素";
                    break;
                case 3:
                    //生成铁剂
                    InsFeEvent();
                    eventName = "生成铁剂";
                    break;
                case 4:
                    //出血
                    HemorrhageEvent();
                    eventName = "出血";
                    break;
                default:
                    break;
            }
            
        }
        UIManager.Instance.gameWnd.UpdateEventTxt(eventName);
        randomEventNum++;
    }

    void InsVirusEvent()
    {
        //不在lung中生成病毒
        Transform ranBody = Tile.transform.GetChild((int)Random.Range(1, 6));

        // 随机生成病毒细菌数量
        int ranNum = (int)Random.Range(3, 6);

        for (int i = 0; i < ranNum; i++)
        {
            ranBody.GetComponentInChildren<NormalBody>().InsVirus();
        }
    }

    void InsResistEvent()
    {
        Transform ranBody = Tile.transform.GetChild((int)Random.Range(0, 6));

        if (ranBody.name.Contains("lung"))
        {
            ranBody.GetComponentInChildren<Lung>().InsResist();
        }
        else
        {
            ranBody.GetComponentInChildren<NormalBody>().InsResist();
        }
    }

    void InsFeEvent()
    {
        Transform ranBody = Tile.transform.GetChild((int)Random.Range(0, 6));

        if (ranBody.name.Contains("lung"))
        {
            ranBody.GetComponentInChildren<Lung>().InsFe();
        }
        else
        {
            ranBody.GetComponentInChildren<NormalBody>().InsFe();
        }
    }

    void HemorrhageEvent()
    {
        int ranNum = Random.Range(2, 5);
        for(int i=0;i<ranNum;i++)
        {
            int ran = Random.Range(0, ValueManager.Instance.RedCellList.Count);
            Destroy(ValueManager.Instance.RedCellList[ran]);
            ValueManager.Instance.RedCellList.RemoveAt(ran);
        }
    }
}
