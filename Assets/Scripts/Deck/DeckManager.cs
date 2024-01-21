using System;
using UnityEngine;

namespace Deck
{
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        
        
    }
}