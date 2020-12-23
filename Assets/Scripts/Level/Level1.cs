using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelBase
{
    public Dictionary<Transform, Vector2[]> BBCol = new Dictionary<Transform, Vector2[]>();

    void Awake()
    {
        PolygonCollider2D[] bodyAreasCol = Tile.GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D bc in bodyAreasCol)
        {
            BBCol.Add(bc.transform.parent, bc.points);
        }
        foreach (Transform tf in BBCol.Keys)
        {
            Debug.Log(tf);
        }
        foreach (Vector2[] col in BBCol.Values)
        {
            for (int i = 0; i < col.GetLength(0); i++)
            {
                col[i].x *= 2;
                col[i].y *= 2;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
