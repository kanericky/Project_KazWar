using Framework;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    [RequireComponent(typeof(UIManager))]
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
            UIManager.Instance.CloseAllUI();
            SceneManager.LoadScene(levelIndex);
        }
    
        public void LoadLevel(string levelName)
        {
            UIManager.Instance.CloseAllUI();
            SceneManager.LoadScene(levelName);
        }
    }
}
