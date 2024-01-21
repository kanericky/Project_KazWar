using System.Collections;
using System.Collections.Generic;
using Deck;
using GameMode;
using UI;
using UnityEngine;

public class GMPhasePlayerTurn : GMPhaseBase
{
    private DeckBase playerDeck;
    private DiceSlotPool playerDiceSlotPool;
    
    public override void Init()
    {
        base.Init();
        
        UIManager.Instance.ShowUI<UIBattlePlayerTurn>("Battle Player Turn HUD");
    }
}
