using UnityEngine;

public class InputManager : MonoBehaviour 
{
    public static InputManager Instance = null;
    private LevelManager levelManager = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelManager.LoadMenu();
        }
    }
    public void Init()
    {
        Instance = this;
        levelManager = LevelManager.Instance;
        Debug.Log("Init InputManager...");
    }
}