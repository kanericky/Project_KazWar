using System;
using System.Collections.Generic;
using Card;
using Framework;
using Shop;
using UI;
using Deck;
using UnityEngine;

namespace Managers
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Instance;

        [Header("Deck Positions")] 
        [SerializeField] private Vector2 shopDeckPosition;
        [SerializeField] private Vector2 playerDeckPosition;

        [Header("Debug")]
        [SerializeField] private List<PlayerCardData> shopCardPool;
        [SerializeField] private List<PlayerCardData> cardsAvailable;
            
        private readonly string _shopComponentPath = "Prefabs/Shop/";
        private readonly string _shopItemsDeckName = "Shop Items Deck";
        private readonly string _shopCardPrefabName = "Shop Card Template";
        private readonly string _shopPlayerCardDeckName = "Shop Player Cards Deck";
        private readonly string _shopPlayerCardPrefabName = "Shop Player Card Template";

        private ShopItemDeck _shopItemDeck;
        private ShopCard _shopCardTemplate;
        private InShopPlayerDeck _inShopPlayerDeck;
        private PlayerShopCard _playerShopCard;

        private int _cardNumToSell = 0;
        
        // Events
        public Action OnShopItemDeckCardNumChanged;
        public Action OnPlayerDeckCardNumChanged;

        private void Awake()
        {
            Instance = this;
            
            // Register events
            OnPlayerDeckCardNumChanged += UpdatePlayerDeckCardPosition;
            OnShopItemDeckCardNumChanged += UpdateShopDeckCardPosition;
        }

        public void InitShopManager(List<PlayerCardData> cardsInShop, List<PlayerCardData> playerCardData, int numOfCardsToSell)
        {
            // Data init
            if (numOfCardsToSell <= 0) return;
            if (numOfCardsToSell > cardsInShop.Count) return;
            
            shopCardPool = cardsInShop;
            _cardNumToSell = numOfCardsToSell;
            
            // UI
            UIManager.Instance.CloseUI<UIMainMenu>(UIManager.UIMainMenuPrefabName);
            UIManager.Instance.ShowUI<UIShop>(UIManager.UIShopPrefabName);
            
            // Actions
            ToolsUtilities.Instance.Shuffle(ref shopCardPool);
            SelectNumOfCards(numOfCardsToSell);
            
            SetupShopItemDeck(cardsAvailable);
            InitPlayerDeck(playerCardData);
        }

        public void InitPlayerDeck(List<PlayerCardData> playerCardDataList)
        {
            SetupPlayerCardDeck(playerCardDataList);
        }
        
        private void SelectNumOfCards(int num)
        {
            if (num <= 0) return;
            if (num >= shopCardPool.Count) return;
            
            for (int i = 0; i < num; i++)
            {
                cardsAvailable.Add(shopCardPool[i]);
            }
        }

        private void SetupShopItemDeck(List<PlayerCardData> shopAvailableCardDataList)
        {
            GameObject obj = Instantiate(Resources.Load(_shopComponentPath + _shopItemsDeckName), transform) as GameObject;
            _shopCardTemplate = ToolsUtilities.LoadResource<ShopCard>(_shopComponentPath + _shopCardPrefabName);
            
            if (obj != null)
            {
                obj.name = _shopItemsDeckName;
                _shopItemDeck = obj.AddComponent<ShopItemDeck>();
                _shopItemDeck.transform.position = shopDeckPosition;
                _shopItemDeck.InitDeck<List<PlayerCardData>, ShopCard>(shopAvailableCardDataList, _shopCardTemplate, .2f);
            }
        }

        private void SetupPlayerCardDeck(List<PlayerCardData> playerCardDataList)
        {
            GameObject obj = Instantiate(Resources.Load(_shopComponentPath + _shopPlayerCardDeckName), transform) as GameObject;
            _playerShopCard = ToolsUtilities.LoadResource<PlayerShopCard>(_shopComponentPath + _shopPlayerCardPrefabName);
            
            if (obj != null)
            {
                obj.name = _shopPlayerCardDeckName;
                _inShopPlayerDeck = obj.AddComponent<InShopPlayerDeck>();
                _inShopPlayerDeck.transform.position = playerDeckPosition;
                _inShopPlayerDeck.InitDeck<List<PlayerCardData>, PlayerShopCard>(playerCardDataList, _playerShopCard, .2f);
            }
        }

        public void BuyCard(PlayerCardData cardData, Transform shopCard)
        {
            _shopItemDeck.RemoveCardFromDeck(cardData, shopCard);
            _inShopPlayerDeck.AddCardToDeck(cardData, shopCard);
            
            shopCard.parent = _inShopPlayerDeck.transform;
            shopCard.GetComponent<CardBase>().SetOwnerDeck(_inShopPlayerDeck);
        }

        public void SellCard(PlayerCardData cardData, Transform playerCard)
        {
            _inShopPlayerDeck.RemoveCardFromDeck(cardData, playerCard);
            _shopItemDeck.AddCardToDeck(cardData, playerCard);
            
            playerCard.parent = _shopItemDeck.transform;
            playerCard.GetComponent<CardBase>().SetOwnerDeck(_shopItemDeck);
        }

        public void RedeemCard(PlayerCardData cardData, Transform playerCard)
        {
            
        }

        public void ResetCard(PlayerCardData cardData, Transform card)
        {
            CardBase playerCardComp = card.GetComponent<CardBase>();
            DeckBase cardOwnerDeck = playerCardComp.GetOwnerDeck();
            Debug.Log(cardOwnerDeck);
            if (cardOwnerDeck == _shopItemDeck)
            {
                _shopItemDeck.AddCardToDeck(cardData, card);
                card.parent = _shopItemDeck.transform;
            }

            if (cardOwnerDeck == _inShopPlayerDeck)
            {
                Debug.Log("1");
                _inShopPlayerDeck.AddCardToDeck(cardData, card);
                card.parent = _inShopPlayerDeck.transform;
            }
        }

        public void RemoveFromOwnerDeck(PlayerCardData cardData, Transform card)
        {
            CardBase playerCardComp = card.GetComponent<CardBase>();
            DeckBase cardOwnerDeck = playerCardComp.GetOwnerDeck();
            
            if (cardOwnerDeck == _shopItemDeck)
            {
                _shopItemDeck.RemoveCardFromDeck(cardData, card);
            }

            if (cardOwnerDeck == _inShopPlayerDeck)
            {
                _inShopPlayerDeck.RemoveCardFromDeck(cardData, card);
            }

            card.parent = transform;
        }

        public void UpdateShopDeckCardPosition()
        {
            _shopItemDeck.UpdateCardPosition();
        }

        public void UpdatePlayerDeckCardPosition()
        {
            _inShopPlayerDeck.UpdateCardPosition();
        }

        public DeckBase GetShopItemDeck()
        {
            return _shopItemDeck;
        }

        public DeckBase GetPlayerInShopDeck()
        {
            return _inShopPlayerDeck;
        }

    }
}