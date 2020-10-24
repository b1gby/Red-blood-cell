using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public MenuWnd menuWnd;
    public GameWnd gameWnd;
    public CheckoutWnd checkoutWnd;
    public static UIManager Instance = null;
    private LevelManager levelManager = null;
    

    public void Init()
    {
        Instance = this;
        levelManager = LevelManager.Instance;
        Debug.Log("Init UIManager...");
    }
}