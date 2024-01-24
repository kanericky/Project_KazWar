using System;
using System.Collections.Generic;
using Card;
using Deck;
using GameMode;
using TMPro;
using UI;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(GameModeManager))]
    public class LevelManager : MonoBehaviour
    {
        
        public static LevelManager Instance;

        [Header("Gameplay Components")]
        [SerializeField] private DeckBase playerDeck;
        [SerializeField] private DiceSlotPool playerDiceSlotPool;

        [Header("UI Display")] 
        [SerializeField] private TMP_Text debugUIText;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameModeManager.Instance.InitGameMode();
        }

        public void InitReference()
        {
            // Get Reference
            playerDeck = FindObjectOfType<DeckBase>();
            playerDiceSlotPool = FindObjectOfType<DiceSlotPool>();
            
            // Change the state machine phase
            GameModeManager.Instance.ChangeGMPhase(GMPhaseType.PlayerTurn);
        }

        public void PlayerTurnFinish()
        {
            CalculatePlayerTurnDamage();
        }

        private void CalculatePlayerTurnDamage()
        {
            int totalDamageInThisTurn = 0;
        
            int diceSlotNum = playerDiceSlotPool.GetDiceSlotNum();
        
            for (int i = 0; i < diceSlotNum; i++)
            {
                int currentDamage = 0;
                List<Transform> playerCards = playerDeck.GetAllCardsInDeck();
                DiceSlot currentSlot = playerDiceSlotPool.GetAllDiceSlots()[i];
                DiceBase currentDice = currentSlot.GetDiceInSlot();

                if (currentDice == null) return; 
                
                int currentDiceNum = currentDice.GetCurrentDiceNum();

                foreach (var cardTransform in playerCards)
                {
                    CardBase card = cardTransform.GetComponent<CardBase>();
                    if (card.CheckCanBeActivated(currentDiceNum))
                    {
                        card.ActivateCard();
                        Debug.Log($"{card} has done damage: {card.GetCardCurrentDamage()}");
                        currentDamage += card.GetCardCurrentDamage();
                    }
                }

                totalDamageInThisTurn += currentDamage;
            }
        
            UpdateDebugUI(totalDamageInThisTurn);
        }

        public void UpdateDebugUI(int totalDamageThisTurn)
        {
            UIBattlePlayerTurn battlePlayerTurnUI = (UIBattlePlayerTurn)
                UIManager.Instance.FindUIComponentInPool(UIManager.UIBattlePlayerTurnPrefabName);

            if (battlePlayerTurnUI == null) return;

            string text = "Total Damage: " + totalDamageThisTurn;
            battlePlayerTurnUI.UpdateDebugDamageUIText(text);
        }

        public void ReloadLevel()
        {
            GameManager.Instance.LoadLevel(GameManager.DevLevelName);
        }
    }
}