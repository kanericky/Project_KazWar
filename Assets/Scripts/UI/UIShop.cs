using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIShop : UIBase
    {

        // UI Components 
        private TMP_Text _playerCoinsText;
        
        // UI Component Names
        private readonly string _playerCoinTextName = "Player Coin Text";
        
        private void Awake()
        {
            BindUIComponent(ref _playerCoinsText, _playerCoinTextName);
        }

        private void UpdatePlayerCoinText(string newText)
        {
            
        }
        
    }
}