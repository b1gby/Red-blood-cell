using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutWnd : WindowRoot
{
    protected override void InitWnd()
    {
        Time.timeScale = 0;
        base.InitWnd();

        
    }

    protected override void ClearWnd()
    {
        Time.timeScale = 1;
        base.ClearWnd();
    }

    public void OnBackMenuBtnClick()
    {
        levelManager.LoadMenu();
        SetWindSate(false);
    }
}
