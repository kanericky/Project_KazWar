using System;
using Managers;
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
            Register(OptionButtonName).OnClick += OnOptionButtonClicked;
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
            UIManager.Instance.CloseAllUI();
            GameManager.Instance.LoadLevel(GameManager.DevLevelName);
        }

        private void OnOptionButtonClicked(GameObject gameObject, PointerEventData pData)
        {
            
        }

        private void OnDestroy()
        {
            Register(StartButtonName).OnClick -= OnStartButtonClicked;
            Register(QuitButtonName).OnClick -= OnQuitButtonClicked;
            Register(DevButtonName).OnClick -= OnDevSceneButtonClicked;
        }
    }
}
