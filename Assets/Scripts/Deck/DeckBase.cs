using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Deck
{
    public class DeckBase : MonoBehaviour
    {
        [Header("Cards")]
        [SerializeField] private List<CardData> cardsToDisplay;

        [Header("Card info")]
        [SerializeField] private CardBase cardTemplate;
        [SerializeField] private float cardWidth;
        [SerializeField] private float cardHeight;

        [Header("Deck settings")] 
        [SerializeField][Range(0,1)] private float cardGap;

        [Header("Runtime")] 
        [SerializeField] private List<CardBase> spawnedCardGameObjects;

        private void Start()
        {
            InitDeck();
        }

        private void InitDeck()
        {
            int cardsAmount = cardsToDisplay.Count;
            Vector2 spawnPosition = Vector2.zero;
        
            // TODO: Fix auto height/weight getter
            cardWidth = cardTemplate.transform.localScale.x;
            cardHeight = cardTemplate.transform.localScale.y;

            for (int i = 0; i < cardsAmount; i++)
            {
                if (cardsAmount % 2 == 1)
                {
                    spawnPosition = new Vector2((i - cardsAmount / 2 - 1 / 2) * cardWidth + 
                                                (i - cardsAmount / 2) * cardGap, transform.localPosition.y);
                }
                else
                {
                    //spawnPosition = new Vector2((i - (cardsAmount / 2) - 1) * cardGap +  (i - (cardsAmount / 2) - 1) * ) ;
                }

                CardBase card = Instantiate(cardTemplate, (Vector3) spawnPosition, Quaternion.identity, transform);
                card.name = "Card - " + (i + 1);
                card.SetupCardData(cardsToDisplay[i], this);
            
                spawnedCardGameObjects.Add(card);
            
            }
        }

        public List<CardBase> GetAllCardsInDeck()
        {
            return spawnedCardGameObjects;
        }

        public int GetCardsNumInDeck()
        {
            return spawnedCardGameObjects.Count;
        }
    
    }
}
