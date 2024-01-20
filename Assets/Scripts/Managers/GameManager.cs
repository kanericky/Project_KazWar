using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Dev names
    public const string MainMenuLevelName = "Gym_MainMenu";
    public const string DevLevelName = "Gym_DevScene";

    private void Start()
    {
        UIManager.Instance.ShowUI<UIMainMenu>(UIManager.UIMainMenuPrefabName);
    }

    public void QuitGame()
    {
        UIManager.Instance.CloseAllUI();
        Application.Quit();
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
