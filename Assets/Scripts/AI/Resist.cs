﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resist : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !UIManager.Instance.gameWnd.ResistUI.transform.GetChild(1).gameObject.activeSelf)
        {
            UIManager.Instance.gameWnd.ResistUI.transform.GetChild(0).gameObject.SetActive(true);
            
            Destroy(this.gameObject);
        }
    }
}
