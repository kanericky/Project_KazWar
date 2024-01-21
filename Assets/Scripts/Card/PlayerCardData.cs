using System.Collections.Generic;
using Ability;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "PlayerCardData", menuName = "Game Data/Card Data/Player Card Data", order = 0)]
    public class PlayerCardData : CardData
    {
        public List<PlayerAbility> playerAbilities;
    }
}
