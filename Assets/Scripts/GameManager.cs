using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public float hp;
    public float maxHP;
    public TextMeshProUGUI hpText;
    public float xp;
    public float toNextLevel;
    public TextMeshProUGUI xpText;
    public float level;
    public TextMeshProUGUI levelText;
    public float score;
    public TextMeshProUGUI scoreText;
    public float coins;
    public TextMeshProUGUI coinsText;
    public float specialCollects;
    public TextMeshProUGUI collectsText;

    public static int gameManagerCount;

    public UIManager uiManager;

    private int toAdd;

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
                    uiManager.OpenMainMenu();
                    break;
                case GameState.LevelSelect:
                    uiManager.OpenLevelSelect();
                    break;
                case GameState.GamePlay:
                    uiManager.OpenGameScreen();
                    break;
                case GameState.Pause:
                    uiManager.OpenPauseScreen();
                    break;
                case GameState.Win:
                    uiManager.OpenWinScreen();
                    break;
                case GameState.Lose:
                    uiManager.OpenLoseScreen();
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
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                gameState = GameState.MainMenu;
                break;
            default:
                gameState = GameState.GamePlay;
                break;
        }
    }

    //Saves Data to File (doesn't work on web)
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");

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
                hpText.text = "HP: " + hp + "/" + maxHP;
                xpText.text = "XP: " + xp + "/" + toNextLevel;
                levelText.text = "LVL: " + level;
                scoreText.text = "SCORE: " + score;
                coinsText.text = "COINS: " + coins;
                collectsText.text = "COLLECTIBLES: " + specialCollects;
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

    public void SetAdd(int toSet)
    {
        toAdd = toSet;
    }

    public void ModifyXP(bool isUp)
    {
        if (isUp) xp += toAdd;
        else xp -= toAdd;
        while(xp >= toNextLevel)
        {
            level++;
            xp -= toNextLevel;
        }
    }

    public void ModifyHealth(bool isUp)
    {
        if (isUp) hp += toAdd;
        else hp -= toAdd;
        if (hp <= 0)
        {
            gameState = GameState.Lose;
            hp = maxHP;
        }
        if(hp > maxHP) hp = maxHP;
    }

    public void ModifyScore(bool isUp)
    {
        if (isUp) score += toAdd;
        else score -= toAdd;
    }

    public void ModifyCoins(bool isUp)
    {
        if (isUp) coins += toAdd;
        else coins -= toAdd;
    }

    public void ModifyCollects(bool isUp)
    {
        if (isUp) specialCollects += toAdd;
        else specialCollects -= toAdd;
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
