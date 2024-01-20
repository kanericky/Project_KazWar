using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeMananger : MonoBehaviour
{
    public static GameModeMananger Instance;
    public GMPhaseBase currentGMPhase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Instance = this;
        }
    }

    private void Update()
    {
        if (currentGMPhase == null)
        {
            Debug.LogAssertion("Current GM Phase is NULL!");
            return;
        }
        
        currentGMPhase.Update();
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
                currentGMPhase.Exit();
                currentGMPhase = new GMPhaseInit();
                break;
            case GMPhaseType.PlayerTurn:
                currentGMPhase.Exit();
                currentGMPhase = new GMPhasePlayerTurn();
                break;
            case GMPhaseType.EnemyTurn:
                currentGMPhase.Exit();
                currentGMPhase = new GMPhaseEnemyTurn();
                break;
            case GMPhaseType.Win:
                currentGMPhase.Exit();
                currentGMPhase = new GMPhaseWin();
                break;
            case GMPhaseType.Lose:
                currentGMPhase.Exit();
                currentGMPhase = new GMPhaseLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GMPhaseType), GMPhaseType, null);
        }
        
        currentGMPhase.Init();
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