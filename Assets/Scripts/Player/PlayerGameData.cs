using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Player
{
    public class PlayerGameData
    {
        public List<PlayerCardData> PlayerCards { get; private set; }
        
        private const int MaxPlayerCardNum = 6;

        public bool AddCardToPlayerCards(PlayerCardData cardDataToAdd)
        {
            if (PlayerCards.Count >= MaxPlayerCardNum)
            {
                Debug.Log("Reach maximum cards that player can hold...");
                return false;
            }

            PlayerCards.Add(cardDataToAdd);
            return true;
        }

        public PlayerGameData()
        {
            PlayerCards = new List<PlayerCardData>();
        }
    }
}