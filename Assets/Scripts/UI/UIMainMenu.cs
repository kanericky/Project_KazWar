using System;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIMainMenu : UIBase
    {

        private readonly string _startButtonName = "Start Button";
        private readonly string _optionButtonName = "Option Button";
        private readonly string _quitButtonName = "Quit Button";
        private readonly string _devButtonName = "Dev Scene Button";
        
        private void Awake()
        {
            Register(_startButtonName).OnClick += OnStartButtonClicked;
            Register(_optionButtonName).OnClick += OnOptionButtonClicked;
            Register(_quitButtonName).OnClick += OnQuitButtonClicked;
            Register(_devButtonName).OnClick += OnDevSceneButtonClicked;
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

        private void OnDisable()
        {
            Register(_startButtonName).OnClick -= OnStartButtonClicked;
            Register(_quitButtonName).OnClick -= OnQuitButtonClicked;
            Register(_devButtonName).OnClick -= OnDevSceneButtonClicked;
        }
    }
}
