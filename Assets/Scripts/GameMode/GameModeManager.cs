using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace GameMode
{
    public class GameModeManager : MonoBehaviour
    {
        public static GameModeManager Instance;
        public GMPhaseBase CurrentGmPhase;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (CurrentGmPhase == null) return;
            
            CurrentGmPhase.OnUpdate();
        }

        public void InitGameMode()
        {
            ChangeGMPhase(GMPhaseType.Init);
        }

        public void ChangeGMPhase(GMPhaseType GMPhaseType)
        {
            switch (GMPhaseType)
            {
                case GMPhaseType.None:
                    break;
                case GMPhaseType.Init:
                    if(CurrentGmPhase != null) CurrentGmPhase.Exit();
                    CurrentGmPhase = new GMPhaseInit();
                    break;
                case GMPhaseType.PlayerTurn:
                    CurrentGmPhase.Exit();
                    CurrentGmPhase = new GMPhasePlayerTurn();
                    break;
                case GMPhaseType.EnemyTurn:
                    CurrentGmPhase.Exit();
                    CurrentGmPhase = new GMPhaseEnemyTurn();
                    break;
                case GMPhaseType.Win:
                    CurrentGmPhase.Exit();
                    CurrentGmPhase = new GMPhaseWin();
                    break;
                case GMPhaseType.Lose:
                    CurrentGmPhase.Exit();
                    CurrentGmPhase = new GMPhaseLose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(GMPhaseType), GMPhaseType, null);
            }
            
            UIManager.Instance.CloseAllUI();
            CurrentGmPhase.Init();
        }
    }



    public enum GMPhaseType
    {
        None,
        Init,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }
}