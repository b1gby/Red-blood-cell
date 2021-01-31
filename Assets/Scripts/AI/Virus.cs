using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public GameObject BelongWhiteCell = null;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LossHealth", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LossHealth()
    {
        this.transform.parent.GetComponent<Health>().LossByVirus();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.GetComponent<PlayerController>().state == PlayerState.resist)
            {
                ValueManager.Instance.VirusList.Remove(this.gameObject);
                Destroy(this.gameObject);
                
            }
            else
            {
                collision.transform.GetComponent<PlayerController>().state = PlayerState.dead;
                Debug.Log("Player is dead!");
            }
            
        }
        if(collision.tag == "WhiteCell")
        {
            BelongWhiteCell.GetComponent<WhiteBlood>().isGoWork = false;
            ValueManager.Instance.VirusList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
