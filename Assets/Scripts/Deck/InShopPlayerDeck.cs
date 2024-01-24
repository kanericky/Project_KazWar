using System.Collections.Generic;
using Card;
using UnityEngine;
using Managers;
using Shop;

namespace Deck
{
    public class InShopPlayerDeck : DeckBase
    {
        private List<PlayerCardData> _cardDataInDeck;

        public void InitDeck<T1, T2>(T1 cardsToDisplay, T2 cardTemplate, float gap) 
            where T1 : List<PlayerCardData>
            where T2 : CardBase
        {
            CardTemplate = cardTemplate;
            CardGap = gap;
            _cardDataInDeck = cardsToDisplay;
            
            DisplayCards<PlayerCardData, PlayerShopCard>(_cardDataInDeck, CardTemplate);
        }
        
        public void AddCardToDeck(PlayerCardData cardData, Transform card)
        {
            _cardDataInDeck.Add(cardData);
            spawnedCardGameObjects.Add(card);
            ShopManager.Instance.OnPlayerDeckCardNumChanged?.Invoke();
        }

        public void RemoveCardFromDeck(PlayerCardData cardData, Transform card)
        {
            // TODO: Handle invalid value
            _cardDataInDeck.Remove(cardData);
            spawnedCardGameObjects.Remove(card);
            ShopManager.Instance.OnPlayerDeckCardNumChanged?.Invoke();
        }
    }
}