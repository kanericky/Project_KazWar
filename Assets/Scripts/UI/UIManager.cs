using System.Collections.Generic;
using Framework;
using UnityEngine;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Transform _uiCanvas;
        private Dictionary<string, UIBase> _uiComponentPool;

        public static string UIComponentPath = "UI/";
        public static string UIMainMenuPrefabName = "Main Menu HUD";
        public static string UIBattlePlayerTurnPrefabName = "Battle Player Turn HUD";
        public static string UIShopPrefabName = "Shop HUD";

        protected override void Awake()
        {
            base.Awake();
            
            _uiCanvas = FindObjectOfType<Canvas>().transform;
            _uiComponentPool = new Dictionary<string, UIBase>();
        }


        public UIBase ShowUI<T>(string uiComponentName) where T : UIBase
        {
            UIBase uiComponentToShow = FindUIComponentInPool(uiComponentName);
            _uiCanvas = FindObjectOfType<Canvas>().transform;

            if (uiComponentToShow == null)
            {
                GameObject spawnedObj = Instantiate(Resources.Load(UIComponentPath + uiComponentName), _uiCanvas) as GameObject;
                if (spawnedObj != null)
                {
                    spawnedObj.name = uiComponentName;
                    uiComponentToShow = spawnedObj.AddComponent<T>();
                    _uiComponentPool.Add(uiComponentName, uiComponentToShow);
                }
            }
            
            uiComponentToShow.Show();

            return uiComponentToShow;
        }

        public void HideUI<T>(string uiComponentName) where T : UIBase
        {
            UIBase uiComponentToShow = FindUIComponentInPool(uiComponentName);

            if (uiComponentToShow == null)
            {
                Debug.LogError($"[Hide UI] Cannot find {uiComponentName} in spawned UI pool, please check the name provided or corresponding logic");
                return;
            }
        
            uiComponentToShow.Hide();
        }
    
        public void CloseUI<T>(string uiComponentName) where T : UIBase
        {
            UIBase uiComponentToShow = FindUIComponentInPool(uiComponentName);

            if (uiComponentToShow == null)
            {   
                Debug.LogError($"[Close UI] Cannot find {uiComponentName} in spawned UI pool, please check the name provided or corresponding logic");
                return;
            }

            _uiComponentPool.Remove(uiComponentName);
            uiComponentToShow.Close();
        }

        public UIBase FindUIComponentInPool(string uiComponentName)
        {
            if (!_uiComponentPool.ContainsKey(uiComponentName))
            {
                return null;
            }
        
            return _uiComponentPool[uiComponentName];
        }

        public void CloseAllUI()
        {
            foreach (var pair in _uiComponentPool)
            {
                pair.Value.Close();
            }
            
            _uiComponentPool.Clear();
        }
    }
}
