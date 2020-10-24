using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    public static ValueManager Instance = null;
    private UIManager uiManager = null;

    public void Init()
    {
        Instance = this;
        uiManager = UIManager.Instance;
        Debug.Log("Init ValueManager...");
    }
}
