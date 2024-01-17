using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSlot : MonoBehaviour
{
    [Header("Runtime")] 
    [SerializeField] private DiceBase diceInSlot = null;
    [SerializeField] private bool isFilled = false;

    public bool FillSlot(DiceBase dice)
    {
        if (!isFilled && diceInSlot == null)
        {
            diceInSlot = dice;
            isFilled = true;
            return true;
        }

        return false;
        Debug.Log($"{transform.name} Dice Slot is full.");
    }
    
    // TODO: Implement when needed
    public void ClearSlot()
    {
        diceInSlot = null;
    }

    public bool GetSlotIsFilled()
    {
        return isFilled;
    }

    public DiceBase GetDiceInSlot()
    {
        return diceInSlot;
    }
    
    
}
