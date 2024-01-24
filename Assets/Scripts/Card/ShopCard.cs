using System;
using Deck;
using DG.Tweening;
using Framework;
using Managers;
using TMPro;
using UnityEngine;

namespace Card
{
    public class ShopCard : CardBase, IDraggable
    {
        protected PlayerCardData PlayerCardData;
        
        // Text components
        protected TMP_Text HealthText;
        protected TMP_Text DamageText;
        protected TMP_Text CardPriceText;
        protected TMP_Text CardDescriptionText;
        protected TMP_Text CardDiceNumText;

        // Text components' names
        private const string HealthTextName = "Health Text";
        private const string DamageTextName = "Damage Text";
        private const string CardPriceTextName = "Card Price Text";
        private const string DiceNumTextName = "Dice Num Text";
        private const string AbilityDescriptionText = "Ability Description Text";
        
        protected Vector3 OriginalScale;
        protected Vector3 OriginalPosition;
        protected Vector3 Offset = Vector2.zero;

        protected bool IsTraded = false;

        public override void SetupCardData(CardData newCardData, DeckBase ownerDeck)
        {
            PlayerCardData = (PlayerCardData) newCardData;
            
            OriginalScale = transform.localScale;
            OriginalPosition = transform.position;
            
            InitShopCard(PlayerCardData);
        }

        public void InitShopCard(PlayerCardData playerCardData)
        {
            PlayerCardData = playerCardData;

            BindComponent(ref HealthText, HealthTextName);
            BindComponent(ref DamageText, DamageTextName);
            BindComponent(ref CardPriceText, CardPriceTextName);
            BindComponent(ref CardDescriptionText, AbilityDescriptionText);
            BindComponent(ref CardDiceNumText, DiceNumTextName);

        }
        
        public T BindComponent<T>(ref T uiComponent, string uiComponentName)
        {
            if (transform.Find(uiComponentName) == null)
            {
                Debug.LogError($"{uiComponentName} is not found under the {transform}!!!");
            }
            
            uiComponent = transform.Find(uiComponentName).GetComponent<T>();
            return uiComponent;
        }

        public void UpdateCardDisplay()
        {
            HealthText.text = PlayerCardData.health.ToString();
            DamageText.text = PlayerCardData.damage.ToString();
            CardPriceText.text = PlayerCardData.cardPrice.ToString();
            CardDescriptionText.text = PlayerCardData.playerAbilities[0].GetAbilityDescription();
            
            string activatedDiceNum = "";
        
            foreach (var diceNum in PlayerCardData.activatedDiceNums)
            {
                if(diceNum != -1) activatedDiceNum += "|" + diceNum + "|";
            }
        
            CardDescriptionText.text = activatedDiceNum;
            
        }

        public virtual void OnMouseDown()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Offset = transform.position - mousePos;
            
            ShopManager.Instance.RemoveFromOwnerDeck(PlayerCardData, transform);
        }

        public virtual void OnMouseUp()
        {
            CheckAndPlace();
        }

        public virtual void OnMouseDrag()
        {
            if (Camera.main == null) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = (Vector2) (mousePos + Offset);
        }

        public virtual void OnMouseOver()
        {
            transform.DOScale(OriginalScale * 1.1f, .1f);
        }

        public virtual void OnMouseExit()
        {
            transform.DOScale(OriginalScale, .1f);
        }

        public virtual void CheckAndPlace()
        {
            int layerMask = ~(1 << LayerMask.NameToLayer("Draggable"));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);
            
            if (hit.collider != null)
            {
                // This shop item card has been brought!
                if (hit.collider.CompareTag($"PlayerSquadArea"))
                {
                    ShopManager.Instance.BuyCard(PlayerCardData, transform);
                    Debug.Log("Card sold to player");
                }
                // This shop item has not been brought yet...
                else if (hit.collider.CompareTag($"ShopArea"))
                {
                    if (GetOwnerDeck() == ShopManager.Instance.GetPlayerInShopDeck())
                    {
                        // TODO: Implement redeem logic
                        ShopManager.Instance.ResetCard(PlayerCardData, transform);
                    }
                    else
                    {
                        ShopManager.Instance.ResetCard(PlayerCardData, transform);
                    }
                }
                // Hit invalid stuff, reset its position...
                else
                {
                    ShopManager.Instance.ResetCard(PlayerCardData, transform);
                }
            }
            // Hit nothing, reset its position...
            else
            {
                ShopManager.Instance.ResetCard(PlayerCardData, transform);
            }
        }

    }
}