using System.Collections.Generic;
using Ability;
using Deck;
using UnityEngine;

namespace Card
{
    public class PlayerCard : CardBase
    {
        [SerializeField] private PlayerCardData playerCardData;
        [SerializeField] private List<PlayerAbility> cardAbilities;

        protected override void InitReference()
        {
            AbilityHandler = GetComponent<PlayerAbilityHandler>();
        }

        public override void SetupCardData(CardData newCardData, DeckBase ownerDeck)
        {
            base.SetupCardData(newCardData, ownerDeck);
        
            // Init default data
            playerCardData = (PlayerCardData) newCardData;

            cardHealth = playerCardData.health;
            cardDamage = playerCardData.damage;
            cardActivatedNum = playerCardData.activatedDiceNums;
            cardAbilities = playerCardData.playerAbilities;

            ownerCardDeck = ownerDeck;
        
            // Register ability to handler
            RegisterAbilityToHandler();

            // Display the card
            InitCardDisplay();
        }
        
        private void RegisterAbilityToHandler()
        {
            foreach (var cardAbility in cardAbilities)
            {
                AbilityHandler.RegisterAbility(abilityBase: cardAbility, cardBase: this);
            }
        }

        protected override void InitCardDisplay()
        {
            base.InitCardDisplay();
            
            if (playerCardData == null)
            {
                Debug.LogError("Card data is null");
                return;
            }

            healthText.text = playerCardData.health.ToString();
            damageText.text = playerCardData.damage.ToString();

            string activatedDiceNum = "";
        
            foreach (var diceNum in cardActivatedNum)
            {
                if(diceNum != -1) activatedDiceNum += "|" + diceNum + "|";
            }
        
            activatedDiceNumText.text = activatedDiceNum;
        }
    }
}