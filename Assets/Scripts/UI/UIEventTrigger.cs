using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIEventTrigger : MonoBehaviour, IPointerClickHandler
    {

        public Action<GameObject, PointerEventData> OnClick;

        public static UIEventTrigger Get(GameObject obj)
        {
            UIEventTrigger trigger = obj.GetComponent<UIEventTrigger>();

            if (trigger == null)
            {
                trigger = obj.AddComponent<UIEventTrigger>();
            }

            return trigger;
        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(gameObject, eventData);
        }
    }
}
