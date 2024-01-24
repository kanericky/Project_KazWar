using System.Collections.Generic;
using Card;
using Deck;
using UnityEngine;

namespace Ability
{
    public class PlayerAbilityHandler : AbilityHandler
    {
        protected override void HandleAbility()
        {
            base.HandleAbility();
            
            AbilityTargetType abilityTargetType = abilityToHandle.GetAbilityTargetType();
            AbilityActionBase abilityAction = abilityToHandle.GetAbilityAction();
        
            List<CardBase> eligibleTargetCards = new List<CardBase>();
        
            // Get target which the ability is applied to
            switch (abilityTargetType)
            {
                case AbilityTargetType.Self:
                    eligibleTargetCards.Add(ownerCard);
                    break;

                case AbilityTargetType.AllAllies:
                    DeckBase cardOwnerDeck = ownerCard.GetOwnerDeck();
                    List<Transform> allCardsInDeck = cardOwnerDeck.GetAllCardsInDeck();

                    foreach (var card in allCardsInDeck)
                    {
                        if (card != ownerCard)
                        {
                            eligibleTargetCards.Add(card.GetComponent<CardBase>());
                        }
                    }
                    break;
            
                case AbilityTargetType.Enemy:
                    break;
            }
        
            abilityAction.ExecuteAction(eligibleTargetCards, ownerCard);
        
            eligibleTargetCards.Clear();
        }
    }
}