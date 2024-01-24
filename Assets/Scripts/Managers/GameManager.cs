using Card;
using Framework;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    [RequireComponent(typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        // Level names
        public const string MainMenuLevelName = "Gym_MainMenu";
        public const string DevLevelName = "Gym_DevScene";

        public PlayerGameData PlayerGameData = new PlayerGameData();

        private void Start()
        {
            // Show main menu UI
            UIManager.Instance.ShowUI<UIMainMenu>(UIManager.UIMainMenuPrefabName);
            
            PlayerGameData.AddCardToPlayerCards(ScriptableObject.CreateInstance<PlayerCardData>());
            PlayerGameData.AddCardToPlayerCards(ScriptableObject.CreateInstance<PlayerCardData>());
            PlayerGameData.AddCardToPlayerCards(ScriptableObject.CreateInstance<PlayerCardData>());
            PlayerGameData.AddCardToPlayerCards(ScriptableObject.CreateInstance<PlayerCardData>());
            PlayerGameData.AddCardToPlayerCards(ScriptableObject.CreateInstance<PlayerCardData>());
            
            ShopManager.Instance.InitShopManager(PlayerGameData.PlayerCards, PlayerGameData.PlayerCards, 3);
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
