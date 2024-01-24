using Card;
using Deck;
using DG.Tweening;
using Framework;
using Managers;
using UnityEngine;

namespace Shop
{
    public class PlayerShopCard : ShopCard
    {
        public override void SetupCardData(CardData newCardData, DeckBase ownerDeck)
        {
            base.SetupCardData(newCardData, ownerDeck);

            OriginalScale = transform.localScale;
        }

        public override void OnMouseDown()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Offset = transform.position - mousePos;
            
            ShopManager.Instance.RemoveFromOwnerDeck(PlayerCardData, transform);
        }

        public override void CheckAndPlace()
        {
            int layerMask = ~(1 << LayerMask.NameToLayer("Draggable"));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag($"ShopArea"))
                {
                    ShopManager.Instance.SellCard(PlayerCardData, transform);
                    Debug.Log($"{name} is sold!");
                }
                else if (hit.collider.CompareTag($"PlayerSquadArea"))
                {
                    // This card has already been sold!
                    if (GetOwnerDeck() == ShopManager.Instance.GetShopItemDeck())
                    {
                        // TODO: Implement redeem logic
                        ShopManager.Instance.ResetCard(PlayerCardData, transform);
                    }
                    // This player card has not been sold! Reset it back
                    else
                    {
                        ShopManager.Instance.ResetCard(PlayerCardData, transform);
                    }
                }
                // Hit other invalid stuff, reset its position
                else
                {
                    ShopManager.Instance.ResetCard(PlayerCardData, transform);
                }
            }
            // Nothing hit, reset its position
            else
            {
                ShopManager.Instance.ResetCard(PlayerCardData, transform);
            }
        }

        public override void OnMouseOver()
        {
            transform.DOScale(OriginalScale * 1.1f, .1f);
        }

        public override void OnMouseExit()
        {
            transform.DOScale(OriginalScale, .1f);
        }
    }
}