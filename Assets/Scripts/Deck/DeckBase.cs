using System.Collections.Generic;
using Card;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Deck
{
    public class DeckBase : MonoBehaviour
    {
        protected CardBase CardTemplate;

        private float _cardWidth;
        private float _cardHeight;

        protected float CardGap;

        [Header("Runtime")] 
        [SerializeField] protected List<Transform> spawnedCardGameObjects = new List<Transform>();

        public virtual void DisplayCards<T1, T2>(List<T1> cardsToDisplay, CardBase cardTemplate) where T1 : CardData where T2 : CardBase
        {
            int cardsAmount = cardsToDisplay.Count;
            Vector2 spawnPosition;
        
            // TODO: Fix auto height/weight getter
            _cardWidth = cardTemplate.transform.localScale.x;
            _cardHeight = cardTemplate.transform.localScale.y;

            for (int i = 0; i < cardsAmount; i++)
            {
                if (cardsAmount % 2 == 1)
                {
                    spawnPosition = new Vector2((i - cardsAmount / 2) * _cardWidth + 
                                                (i - cardsAmount / 2) * CardGap, transform.localPosition.y);
                }
                else
                {
                    spawnPosition = new Vector2((i - cardsAmount / 2 + 0.5f) * _cardWidth +
                                                (i - cardsAmount / 2 + 0.5f) * CardGap, transform.localPosition.y);
                }

                T2 card = Instantiate(cardTemplate, (Vector3) spawnPosition, Quaternion.identity, transform) as T2;
                card.name = transform.name + "- Card " + (i + 1);
                card.SetOwnerDeck(this);
                card.SetupCardData(cardsToDisplay[i], this);
                spawnedCardGameObjects.Add(card.transform);
            }
        }

        public void UpdateCardPosition()
        {
            int cardsAmount = spawnedCardGameObjects.Count;
            Vector2 targetPos;

            for (int i = 0; i < cardsAmount; i++)
            {
                if (cardsAmount % 2 == 1)
                {
                    targetPos = new Vector2((i - cardsAmount / 2) * _cardWidth + 
                                            (i - cardsAmount / 2) * CardGap, transform.localPosition.y);
                }
                else
                {
                    targetPos = new Vector2((i - cardsAmount / 2 + 0.5f) * _cardWidth +
                                            (i - cardsAmount / 2 + 0.5f) * CardGap, transform.localPosition.y);
                }

                spawnedCardGameObjects[i].transform.DOMove(targetPos, .2f);
            }
        }

        public virtual List<Transform> GetAllCardsInDeck()
        {
            return spawnedCardGameObjects;
        }

        public virtual int GetCardsNumInDeck()
        {
            return spawnedCardGameObjects.Count;
        }
    
    }
}
