using Card;
using UnityEngine;

namespace Ability
{
    public class AbilityHandler : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] protected AbilityBase abilityToHandle;
        [SerializeField] protected CardBase ownerCard;


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

        protected virtual void HandleAbility() { }
    }
}