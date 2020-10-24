using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameWnd : WindowRoot 
{

    public Text timerTxt;
    [SerializeField]
    private int TotalTime;

    public Slider slider_heart;
    public Slider slider_lung;
    public Slider slider_liver;
    public Slider slider_stomach;
    public Slider slider_kidney;
    public Slider slider_spleen;

    public int dead_num = 0;

    protected override void InitWnd()
    {
        base.InitWnd();
        TotalTime = 720;
        StartCoroutine(CountDown());
    }
    protected override void ClearWnd()
    {

    }

    private void Update()
    {
        DetectGameOver();
    }

    public void UpdateSlider(int health, string slider_name)
    {
        switch (slider_name)
        {
            case "heart":
                slider_heart.value = health;
                break;
            case "lung":
                slider_lung.value = health;
                break;
            case "liver":
                slider_liver.value = health;
                break;
            case "stomach":
                slider_stomach.value = health;
                break;
            case "kidney":
                slider_kidney.value = health;
                break;
            case "spleen":
                slider_spleen.value = health;
                break;
        }


    }

    /// <summary>
    /// 更新时间
    /// </summary>
    /// <param name="time"></param>
    public void UpdateTimer(int time)
    {
        int minute = time / 60;
        int second = time % 60;

        timerTxt.text = minute.ToString().PadLeft(2,'0') + " : " + second.ToString().PadLeft(2, '0');
    }

    IEnumerator CountDown()
    {
        while (TotalTime >= 0)
        {
            UpdateTimer(TotalTime);
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
        

        SetWindSate(false);
        uIManager.checkoutWnd.SetWindSate();
    }

    void DetectGameOver()
    {
        dead_num =
            slider_kidney.value == 0 ? 1 : 0 +
            slider_lung.value == 0 ? 1 : 0 +
            slider_liver.value == 0 ? 1 : 0 +
            slider_spleen.value == 0 ? 1 : 0 +
            slider_stomach.value == 0 ? 1 : 0;
        PlayerState state = GameObject.Find("Player").GetComponent<PlayerController>().state;
        if(slider_heart.value == 0 || dead_num >= 3 ||state == PlayerState.dead)
        {
            SetWindSate(false);
            uIManager.checkoutWnd.SetWindSate();
        }
    }
}