using UnityEngine;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour 
{
    protected LevelManager levelManager = null;
    protected ValueManager valueManager = null;
    protected UIManager uIManager = null;
    public void SetWindSate(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
        {
            SetActive(gameObject, isActive);
            if (isActive)
            {
                InitWnd();
            }
            else
            {
                ClearWnd();
            }
        }
        
    }

    protected virtual void InitWnd()
    {
        levelManager = LevelManager.Instance;
        valueManager = ValueManager.Instance;
        uIManager = UIManager.Instance;
    }

    protected virtual void ClearWnd()
    {
        levelManager = null;
        valueManager = null;
    }

    protected void SetActive(GameObject go, bool isActive = true)
    {
        go.SetActive(isActive);
    }
    protected void SetActive(Transform trans, bool state = true) { trans.gameObject.SetActive(state); }
    protected void SetActive(RectTransform rectTrans, bool state = true) { rectTrans.gameObject.SetActive(state); }
    protected void SetActive(Image img, bool state = true) { img.transform.gameObject.SetActive(state); }
    protected void SetActive(Text txt, bool state = true) { txt.transform.gameObject.SetActive(state); }
    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }
    protected void SetText(Transform trans, int num = 0)
    {
        SetText(trans.GetComponent<Text>(), num);
    }
    protected void SetText(Transform trans, string context = "")
    {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }
}