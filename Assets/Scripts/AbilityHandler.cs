using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private AbilityBase abilityToHandle;
    [SerializeField] private CardBase ownerCard;


    public void RegisterAbility(AbilityBase abilityBase, CardBase cardBase)
    {
        abilityToHandle = abilityBase;
        ownerCard = cardBase;

        AbilityConditionType abilityConditionType = abilityToHandle.GetAbilityConditionType();
        switch (abilityConditionType)
        {
            case AbilityConditionType.OnActivated:
                ownerCard.OnCardActivated += HandleAbility;
                break;
            
            case AbilityConditionType.OnBuffed:
                ownerCard.OnCardBuffed += HandleAbility;
                break;
            
            case AbilityConditionType.OnDamaged:
                ownerCard.OnCardDamaged += HandleAbility;
                break;
            
            case AbilityConditionType.OnAfterDamage:
                ownerCard.OnCardDamageApplied += HandleAbility;
                break;
            
            case AbilityConditionType.OnDestroyed:
                ownerCard.OnCardDead += HandleAbility;
                break;
            
        }
    }

    private void HandleAbility()
    {
        AbilityTargetType abilityTargetType = abilityToHandle.GetAbilityTargetType();
        AbilityActionBase abilityAction = abilityToHandle.GetAbilityAction();
        
        List<CardBase> eligibleTargetCards = new List<CardBase>();
        
        switch (abilityTargetType)
        {
            case AbilityTargetType.Self:
                eligibleTargetCards.Add(ownerCard);
                break;

            case AbilityTargetType.AllAllies:
                DeckBase cardOwnerDeck = ownerCard.GetOwnerDeck();
                List<CardBase> allCardsInDeck = cardOwnerDeck.GetAllCardsInDeck();

                foreach (var card in allCardsInDeck)
                {
                    if (card != ownerCard)
                    {
                        eligibleTargetCards.Add(card);
                    }
                }
                break;
        }
        
        abilityAction.ExecuteAction(eligibleTargetCards, ownerCard);
        
        eligibleTargetCards.Clear();
    }
}