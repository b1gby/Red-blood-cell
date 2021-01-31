using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 1500;
    public int num_O2 = 0, num_CO2 = 0;
    public int lossP = 1;
    private bool isAddBlood = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AutoLoseHealth", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        DetectO2();
        DetectCO2();

        if(health == 0)
        {
            isAddBlood = false;
        }
    }

    private void LateUpdate()
    {
        num_O2 = 0;
        num_CO2 = 0;
        foreach(Transform child in transform)
        {
            if (child.name == "O2(Clone)")
            {
                num_O2++;
            }
            if (child.name == "CO2(Clone)")
            {
                num_CO2++;
            }
        }
        lossP = num_CO2 / 5 + 1;

        UIManager.Instance.gameWnd.UpdateSlider(health,this.transform.name.Replace("_tm",""));
    }

    void AutoLoseHealth()
    {
        if(ValueManager.Instance.CO2List.Count>5)
        {
            health -= 3 * lossP;
        }
        
    }

    void DetectO2()
    {
        if(transform.name == "lung_tm")
        {
            return;
        }
        else
        {
            foreach(Transform child in transform)
            {
                if(child.name == "O2(Clone)")
                {
                    ValueManager.Instance.O2List.Remove(child.gameObject);
                    Destroy(child.gameObject);
                    if(isAddBlood)
                    {
                        health += 50;
                    }
                }
            }
        }
    }

    void DetectCO2()
    {
        if(transform.name == "lung_tm")
        {
            foreach (Transform child in transform)
            {
                if (child.name == "CO2(Clone)")
                {
                    ValueManager.Instance.CO2List.Remove(child.gameObject);
                    Destroy(child.gameObject);
                }
            }
        }
        else
        {
            return;
        }
    }

    public void LossByVirus()
    {
        health -= 5;
    }
}
