using System.Collections.Generic;
using UnityEngine;

public class DicePool : MonoBehaviour
{
    [Header("Dice Pool Data")] 
    [SerializeField] private DiceBase diceTemplate;
    [SerializeField][Range(1, 10)] private int diceNum = 1;

    [SerializeField] private float diceSize;
    [SerializeField] private float gap;

    [Header("Runtime")] 
    [SerializeField] private List<DiceBase> spawnedDices;
    
    //TODO: Delete after debug...
    private void Start()
    {
        InitDicePool(diceNum);
        RollAllDicesInPool();
    }

    public void InitDicePool(int diceNumInPool)
    {
        diceNum = diceNumInPool;
        diceSize = diceTemplate.transform.localScale.x; // TODO: Fix the correct dice size

        for (int i = 0; i < diceNum; i++)
        {
            var poolPosition = transform.position;
            Vector2 spawnPosition = new Vector2(poolPosition.x + i * diceSize + (i - 1) * gap, poolPosition.y);
            DiceBase spawnedDice = Instantiate(diceTemplate, spawnPosition, Quaternion.identity, transform);
            spawnedDice.InitDice(this);
            spawnedDices.Add(spawnedDice);
        }
    }

    public void RollAllDicesInPool()
    {
        foreach(var dice in spawnedDices)
        {
            dice.Roll();
        }
    }

    public void RemoveDiceFromPool(DiceBase dice)
    {
        if (!spawnedDices.Contains(dice)) return;
        if (dice == null) return;
        
        spawnedDices.Remove(dice);
    }

    public int GetDiceNumInPool()
    {
        return spawnedDices.Count;
    }
    
}
