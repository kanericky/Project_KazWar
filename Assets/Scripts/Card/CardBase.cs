using System;
using System.Collections.Generic;
using Ability;
using Deck;
using TMPro;
using UnityEngine;

namespace Card
{
    [RequireComponent(typeof(AbilityHandler))]
    public class CardBase : MonoBehaviour
    {
        [Header("Runtime")] 
        [SerializeField] protected int cardHealth = 0;
        [SerializeField] protected int cardDamage = 0;
        [SerializeField] protected List<int> cardActivatedNum;

        [SerializeField] protected DeckBase ownerCardDeck;

        [Header("UI Display")] 
        [SerializeField] protected TMP_Text healthText;
        [SerializeField] protected TMP_Text damageText;
        [SerializeField] protected TMP_Text activatedDiceNumText;

        [Header("Debug")]
        [SerializeField] protected bool isBuffed = false;
    
        protected AbilityHandler AbilityHandler;


        // Events
        public event Action OnCardActivated;
        public event Action OnCardDamaged;
        public event Action OnCardBuffed;
        public event Action OnCardDamageApplied;
        public event Action OnCardDead;

        private void OnEnable()
        {
            OnCardActivated += UpdateUIDisplay;
            OnCardDamaged += UpdateUIDisplay;
            OnCardBuffed += UpdateUIDisplay;
            OnCardDamageApplied += UpdateUIDisplay;
            OnCardDead += UpdateUIDisplay;
        }

        public virtual void SetupCardData(CardData newCardData, DeckBase ownerDeck)
        {
        }

        protected virtual void InitReference(){}

        protected virtual void InitCardDisplay() { }

        private void UpdateUIDisplay()
        {
            healthText.text = cardHealth.ToString();
            damageText.text = cardDamage.ToString();
        }

        public int GetCardCurrentDamage()
        {
            return cardDamage;
        }

        public bool CheckCanBeActivated(int num)
        {
            for (int i = 0; i < cardActivatedNum.Count; i++)
            {
                if (cardActivatedNum[i] == num)
                {
                    return true;
                }
            }

            return false;
        }

        public void ActivateCard()
        {
            OnCardActivated?.Invoke();
            Debug.Log($"{gameObject.name} has been activated!");
        }

        public void BuffCard(int healthAmount, int damageAmount, CardBase instigatorCard)
        {
            IncreaseCardDataInRuntime(healthAmount, damageAmount);
            if (instigatorCard != this || (instigatorCard == this && isBuffed == false))
            {
                if (instigatorCard == this)
                {
                    isBuffed = true;
                }
                OnCardBuffed?.Invoke();
            }
        }

        public void DamageCard(int damageDeal)
        {
            OnCardDamaged?.Invoke();
            DecreaseCardDataInRuntime(damageDeal, 0);
            OnCardDamageApplied?.Invoke();
        }

        private void IncreaseCardDataInRuntime(int healthAmount, int damageAmount)
        {
            cardHealth += healthAmount;
            cardDamage += damageAmount;
        }

        private void DecreaseCardDataInRuntime(int healthAmount, int damageAmount)
        {
            cardHealth -= healthAmount;
            cardDamage -= damageAmount;
        }

        public DeckBase GetOwnerDeck()
        {
            return ownerCardDeck;
        }

        public void SetOwnerDeck(DeckBase ownerDeck)
        {
            ownerCardDeck = ownerDeck;
        }

        public bool GetCardIsBuffed()
        {
            return isBuffed;
        }

        
    }
}
