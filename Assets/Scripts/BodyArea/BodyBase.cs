using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject O2;
    [SerializeField]
    protected GameObject CO2;
    [SerializeField]
    protected GameObject Virus;
    //玩家信息
    [SerializeField]
    protected GameObject player;

    [SerializeField]
    protected PolygonCollider2D polyCol;

    protected Vector2[] Points;

    public float max_y = -9999f, max_x = -9999f, min_y = 9999f, min_x = 9999f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected void Init()
    {
        player = GameObject.Find("Player");
        polyCol = this.transform.GetComponent<PolygonCollider2D>();
        Points = polyCol.points;
        for (int i = 0; i < Points.GetLength(0); i++)
        {
            if (Points[i].y > max_y)
            {
                max_y = Points[i].y;
            }
            if (Points[i].x > max_x)
            {
                max_x = Points[i].x;
            }
            if (Points[i].y < min_y)
            {
                min_y = Points[i].y;
            }
            if (Points[i].x < min_x)
            {
                min_x = Points[i].x;
            }
        }
    }

    public Vector2 GetRandomInCol()
    {
        while (true)
        {
            Vector2 point = new Vector2(Random.Range(min_x, max_x), Random.Range(min_y, max_y));
            if (isPolygonContainsPoint(Points, point))
            {
                return point;
            }
        }
    }

    public bool isPolygonContainsPoint(Vector2[] points_list, Vector2 point)
    {
        int nCross = 0;
        for (int i = 0; i < points_list.GetLength(0); i++)
        {
            Vector2 p1 = points_list[i];
            Vector2 p2 = points_list[(i + 1) % points_list.GetLength(0)];
            //p1 p2是水平的，要么没有交点，要么有无限个交点
            if (p1.y == p2.y)
                continue;
            //point 在p1p2底部，无交点
            if (point.y < Mathf.Min(p1.y, p2.y))
                continue;
            //point 在p1p2顶部，无交点
            if (point.y >= Mathf.Max(p1.y, p2.y))
                continue;
            //计算point点水平线于p1p2的交点
            float x = (point.y - p1.y) * (p2.x - p1.x) / (p2.y - p1.y) + p1.x;
            if (x > point.x)
            {
                nCross++;
            }
        }

        //交点为偶数，点在外面
        return (nCross % 2 == 1);
    }
}
