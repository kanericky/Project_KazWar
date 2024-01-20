using UnityEngine;

namespace UI
{
    public class UIBase : MonoBehaviour
    {

        /// <summary>
        /// Register event to interactive UI component, e.g. button
        /// </summary>
        /// <param name="uiComponentName"></param> The name of that component under template prefab
        /// <returns></returns>
        public UIEventTrigger Register(string uiComponentName)
        {
            Transform uiComponent = transform.Find(uiComponentName);
            return UIEventTrigger.Get(uiComponent.gameObject);
        }
    
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}
