using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSlotPool : MonoBehaviour
{
    [Header("Dice Slot Pool Data")] 
    [SerializeField] private DiceSlot diceSlotTemplate;
    [SerializeField][Range(1, 10)] private int slotNum;
    
    [SerializeField] private float slotSize;
    [SerializeField] private float gap;

    [Header("Runtime")] 
    [SerializeField] private List<DiceSlot> spawnedDiceSlots;
    
    // TODO: Delete after debug
    private void Start()
    {
        InitDiceSlotPool(slotNum);
    }

    public void InitDiceSlotPool(int poolSlotNum)
    {
        slotNum = poolSlotNum;
        slotSize = diceSlotTemplate.transform.localScale.x; // TODO: Fix the correct dice size

        for (int i = 0; i < slotNum; i++)
        {
            var poolPosition = transform.position;
            Vector2 spawnPosition = new Vector2(poolPosition.x + i * slotSize + (i - 1) * gap, poolPosition.y);
            DiceSlot spawnedDice = Instantiate(diceSlotTemplate, spawnPosition, Quaternion.identity, transform);
            spawnedDiceSlots.Add(spawnedDice);
        }
    }

    public int GetDiceSlotNum()
    {
        return slotNum;
    }

    public List<DiceSlot> GetAllDiceSlots()
    {
        return spawnedDiceSlots;
    }
}
