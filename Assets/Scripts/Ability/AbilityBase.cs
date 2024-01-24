using System;
using UnityEngine;

[Serializable]
public class AbilityBase
{
    [SerializeField] protected string abilityDescription;
    [SerializeField] protected AbilityConditionType abilityConditionType;
    [SerializeField] protected AbilityTargetType abilityTargetType;
    [SerializeField] protected AbilityActionBase abilityActionBase;

    public string GetAbilityDescription()
    {
        return abilityDescription;
    }

    public AbilityConditionType GetAbilityConditionType()
    {
        return abilityConditionType;
    }

    public AbilityTargetType GetAbilityTargetType()
    {
        return abilityTargetType;
    }

    public AbilityActionBase GetAbilityAction()
    {
        return abilityActionBase;
    }
}



[Serializable]
public enum AbilityConditionType
{
    OnActivated,
    OnDamaged,
    OnAfterDamage,
    OnBuffed,
    OnDestroyed
}

public enum AbilityTargetType
{
    Enemy,
    Self,
    RandomAlly,
    NearAllies,
    AllyOnLeft,
    AllyOnRight,
    AllAllies,
    AllAlliesOnLeft,
    AllAlliesOnRight,
    AllyOnTheLeftmost,
    AllyOnTheRightmost,
    AllEnemies,
}