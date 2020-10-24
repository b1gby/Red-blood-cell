using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
    public static LevelManager Instance = null;
    private UIManager uiManager;
    public void Init()
    {
        Instance = this;
        uiManager = UIManager.Instance;
        Debug.Log("Init LevelManager...");
    }

    public void NextScence()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1<SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            uiManager.menuWnd.gameObject.SetActive(false);
            uiManager.gameWnd.SetWindSate(true);
        }
        else
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        uiManager.menuWnd.SetWindSate(true);
    }
}