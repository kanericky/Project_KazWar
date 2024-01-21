using System;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIBattlePlayerTurn : UIBase
    {
        private const string RollButtonName = "Roll Button";
        private const string StartButtonName = "Start Button";
        private const string ReloadButtonName = "Reload Button";

        private void Awake()
        {
            Register(RollButtonName).OnClick += RollDice;
            Register(StartButtonName).OnClick += PlayerTurnEnd;
            Register(ReloadButtonName).OnClick += ReloadLevel;
        }

        private void RollDice(GameObject gameObject, PointerEventData pData)
        {
            DicePool.Instance.RollAllDicesInPool();
        }

        private void PlayerTurnEnd(GameObject gameObject, PointerEventData pData)
        {
            LevelManager.Instance.PlayerTurnFinish();
        }

        private void ReloadLevel(GameObject gameObject, PointerEventData pData)
        {
            GameManager.Instance.LoadLevel(GameManager.DevLevelName);
        }

        private void OnDisable()
        {
            Register(RollButtonName).OnClick -= RollDice;
            Register(StartButtonName).OnClick -= PlayerTurnEnd;
            Register(ReloadButtonName).OnClick -= ReloadLevel;
        }
    }
}