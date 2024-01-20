using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIMainMenu : UIBase
    {

        private const string StartButtonName = "Start Button";
        private const string OptionButtonName = "Option Button";
        private const string QuitButtonName = "Quit Button";
        private const string DevButtonName = "Dev Scene Button";
        
        private void Awake()
        {
            Register(StartButtonName).OnClick += OnStartButtonClicked;
            Register(QuitButtonName).OnClick += OnQuitButtonClicked;
            Register(DevButtonName).OnClick += OnDevSceneButtonClicked;
        }

        private void OnStartButtonClicked(GameObject gameObject, PointerEventData pData)
        {
            Close();
        }
        
        private void OnQuitButtonClicked(GameObject gameObject, PointerEventData pData)
        {
            GameManager.Instance.QuitGame();
        }

        private void OnDevSceneButtonClicked(GameObject gameObject, PointerEventData pData)
        {
            GameManager.Instance.LoadLevel(GameManager.DevLevelName);
        }

        private void OnDestroy()
        {
            Register(StartButtonName).OnClick -= OnStartButtonClicked;
            Register(QuitButtonName).OnClick -= OnQuitButtonClicked;
            Register(DevButtonName).OnClick -= OnDevSceneButtonClicked;
        }
    }
}
