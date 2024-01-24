using System;
using System.Collections.Generic;
using Card;
using Deck;
using Framework;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Shop
{
    public class ShopItemDeck : DeckBase
    {
        private List<PlayerCardData> _cardDataInDeck;

        public void InitDeck<T1, T2>(T1 cardsToDisplay, T2 cardTemplate, float gap) 
            where T1 : List<PlayerCardData>
            where T2 : CardBase
        {
            CardTemplate = cardTemplate;
            CardGap = gap;
            _cardDataInDeck = cardsToDisplay;
            
            DisplayCards<PlayerCardData, ShopCard>(_cardDataInDeck, CardTemplate);
        }
        

        public void AddCardToDeck(PlayerCardData cardData, Transform card)
        {
            _cardDataInDeck.Add(cardData);
            spawnedCardGameObjects.Add(card);
            ShopManager.Instance.OnShopItemDeckCardNumChanged?.Invoke();
        }

        public void RemoveCardFromDeck(PlayerCardData cardData, Transform card)
        {
            _cardDataInDeck.Remove(cardData);
            spawnedCardGameObjects.Remove(card);
            ShopManager.Instance.OnShopItemDeckCardNumChanged?.Invoke();
        }
    }
}