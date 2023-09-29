using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LevelSelect;
    public GameObject GamePlayCanvas;
    public GameObject PauseScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        LevelSelect.SetActive(false);
        GamePlayCanvas.SetActive(false);
        PauseScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    public void OpenLevelSelect()
    {
        MainMenu.SetActive(false);
        LevelSelect.SetActive(true);
        GamePlayCanvas.SetActive(false);
        PauseScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    public void OpenGameScreen()
    {
        MainMenu.SetActive(false);
        LevelSelect.SetActive(false);
        GamePlayCanvas.SetActive(true);
        PauseScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    public void OpenPauseScreen()
    {
        MainMenu.SetActive(false);
        LevelSelect.SetActive(false);
        GamePlayCanvas.SetActive(false);
        PauseScreen.SetActive(true);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    public void OpenWinScreen()
    {
        MainMenu.SetActive(false);
        LevelSelect.SetActive(false);
        GamePlayCanvas.SetActive(false);
        PauseScreen.SetActive(false);
        WinScreen.SetActive(true);
        LoseScreen.SetActive(false);
    }

    public void OpenLoseScreen()
    {
        MainMenu.SetActive(false);
        LevelSelect.SetActive(false);
        GamePlayCanvas.SetActive(false);
        PauseScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(true);
    }
}
