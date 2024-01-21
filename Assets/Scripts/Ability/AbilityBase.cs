using System;
using UnityEngine;

[Serializable]
public class AbilityBase
{
    [SerializeField] private string abilityDescription;
    [SerializeField] private AbilityConditionType abilityConditionType;
    [SerializeField] private AbilityTargetType abilityTargetType;
    [SerializeField] private AbilityActionBase abilityActionBase;

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