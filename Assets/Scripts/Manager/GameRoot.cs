using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    public static GameRoot Instance = null;


    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        ClearUIRoot();
        Init();

        Debug.Log("Game Start...");
    }
    private void Init()
    {
        UIManager uIManager = GetComponent<UIManager>();
        uIManager.Init();
        LevelManager levelManager = GetComponent<LevelManager>();
        levelManager.Init();      
        InputManager inputManager = GetComponent<InputManager>();
        inputManager.Init();
        ValueManager valueManager = GetComponent<ValueManager>();
        valueManager.Init();


        levelManager.LoadMenu();
    }
    private void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }

    }
}