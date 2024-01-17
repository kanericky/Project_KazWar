using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityActionBase : IActionExecutable
{
    [SerializeField] private AbilityActionType abilityActionType;

    [SerializeField] private BuffData buffData;
    [SerializeField] private DamageData damageData;

    public void ExecuteAction(List<CardBase> targetCards, CardBase instigatorCard)
    {
        switch (abilityActionType)
        {
            case AbilityActionType.BuffType:
                foreach (var targetCard in targetCards)
                {
                    targetCard.BuffCard(healthAmount: buffData.healthBuffValue, 
                        damageAmount: buffData.damageBuffValue, instigatorCard);
                }
                break;
            
            case AbilityActionType.DamageType:
                break;
            
            case AbilityActionType.DestroyType:
                break;
        }
    }
}

public enum AbilityActionType
{
    DamageType,
    BuffType,
    DestroyType,
}

[Serializable]
public struct BuffData
{
    public int healthBuffValue;
    public int damageBuffValue;
}

[Serializable]
public struct DamageData
{
    public int damageValue;
}