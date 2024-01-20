using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {

        public static LevelManager instance;
    
        [Header("Runtime")]
        [SerializeField] private LevelState levelState = LevelState.NotSet;

        [Header("Gameplay Components")]
        [SerializeField] private DeckBase playerDeck;
        [SerializeField] private DiceSlotPool playerDiceSlotPool;


        [Header("UI Display")] 
        [SerializeField] private TMP_Text debugUIText;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            levelState = LevelState.LevelInitializing;
            playerDeck = FindObjectOfType<DeckBase>();
            playerDiceSlotPool = FindObjectOfType<DiceSlotPool>();
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
                List<CardBase> playerCards = playerDeck.GetAllCardsInDeck();
                DiceSlot currentSlot = playerDiceSlotPool.GetAllDiceSlots()[i];
                DiceBase currentDice = currentSlot.GetDiceInSlot();
                int currentDiceNum = currentDice.GetCurrentDiceNum();

                foreach (var card in playerCards)
                {
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
            debugUIText.text = "Total damage: " + totalDamageThisTurn;
        }

        public void ReloadLevel()
        {
            GameManager.Instance.LoadLevel(GameManager.DevLevelName);
        }
    }

    public enum LevelState
    {
        NotSet,
        LevelInitializing,
        PlayerTurn,
        EnemyTurn,
        CombatSummary,
    }
}