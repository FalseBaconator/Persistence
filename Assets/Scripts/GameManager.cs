using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public float hp;
    public float maxHP;
    public float xp;
    public float toNextLevel;
    public float level;
    public float score;
    public float coins;
    public float specialCollects;

    public static int gameManagerCount;

    public UIManager uiManager;


    public enum GameState { MainMenu, LevelSelect, GamePlay, Pause, Win, Lose }

    private GameState _gameState;
    public GameState gameState
    {
        get => _gameState;
        set
        {
            switch (value)
            {
                case GameState.MainMenu:
                    Time.timeScale = 1;
                    uiManager.OpenMainMenu();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.LevelSelect:
                    Time.timeScale = 1;
                    uiManager.OpenLevelSelect();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.GamePlay:
                    Time.timeScale = 1;
                    uiManager.OpenGameScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        //firstPersonSam.UnPause();
                    }
                    catch { }
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GameState.Pause:
                    Time.timeScale = 0;
                    uiManager.OpenPauseScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        //firstPersonSam.Pause();
                    }
                    catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Win:
                    Time.timeScale = 0;
                    uiManager.OpenWinScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        //firstPersonSam.Pause();
                    }
                    catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Lose:
                    Time.timeScale = 0;
                    uiManager.OpenLoseScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        //firstPersonSam.Pause();
                    }
                    catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }
            _gameState = value;
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        if(singleton == null)
        {
            gameManagerCount++;
            DontDestroyOnLoad(gameObject);
            singleton = this;
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Saves Data to File (doesn't work on web)
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);

        SaveData data = new SaveData();
        data.hp = hp;
        data.maxHP = maxHP;
        data.xp = xp;
        data.toNextLevel = toNextLevel;
        data.level = level;
        data.score = score;
        data.coins = coins;
        data.specialCollects = specialCollects;

        bf.Serialize(file, data);
        file.Close();
    }

    //Loads data from File (doesn't work on web)
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            hp = data.hp;
            maxHP = data.maxHP;
            xp = data.xp;
            toNextLevel = data.toNextLevel;
            level = data.level;
            score = data.score;
            coins = data.coins;
            specialCollects = data.specialCollects;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) QuitToMenu(); // press 1
        if (Input.GetKeyDown(KeyCode.Alpha2)) LoadLevel(1); // press 2
        if (Input.GetKeyDown(KeyCode.Alpha3)) LoadLevel(2); // press 3
        if (Input.GetKeyDown(KeyCode.Alpha4)) LoadLevel(3); // press 4

        switch (gameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.LevelSelect:
                break;
            case GameState.GamePlay:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.GamePlay;
                }
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }

    }

    public void GoToLevelSelect()
    {
        gameState = GameState.LevelSelect;
    }

    public void LoadLevel(int scene)
    {
        SceneManager.LoadScene(scene);
        gameState = GameState.GamePlay;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        gameState = GameState.MainMenu;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

[Serializable]
class SaveData
{
    public float hp;
    public float maxHP;
    public float xp;
    public float toNextLevel;
    public float level;
    public float score;
    public float coins;
    public float specialCollects;
}
