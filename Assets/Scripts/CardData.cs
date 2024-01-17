using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Game Data/Card Data", order = 0)]
public class CardData : ScriptableObject
{
    public int health = 0;
    public int damage = 0;
    public bool[] activatedDiceNums = new bool[]{false, false, false, false, false, false};
    public AbilityBase cardAbility;
}
