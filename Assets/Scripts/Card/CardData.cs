using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardData : ScriptableObject
    {
        public int health = 0;
        public int damage = 0;
        public List<int> activatedDiceNums = new List<int>();
    }
}
