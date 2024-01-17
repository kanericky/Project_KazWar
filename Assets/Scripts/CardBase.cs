using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AbilityHandler))]
public class CardBase : MonoBehaviour
{
    [SerializeField] private CardData cardData;

    [Header("Runtime")] 
    [SerializeField] private int cardHealth = 0;
    [SerializeField] private int cardDamage = 0;
    [SerializeField] private bool[] cardActivatedNum = new bool[]{false, false, false, false, false, false};
    [SerializeField] private AbilityBase cardAbility;

    [SerializeField] private DeckBase ownerCardDeck;

    [Header("UI Display")] 
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text activatedDiceNumText;

    private AbilityHandler _abilityHandler;
    
    [Header("Debug")]
    [SerializeField] private bool isBuffed = false;


    // Events
    public event Action OnCardActivated;
    public event Action OnCardDamaged;
    public event Action OnCardBuffed;
    public event Action OnCardDamageApplied;
    public event Action OnCardDead;

    private void OnEnable()
    {
        OnCardActivated += UpdateUIDisplay;
        OnCardDamaged += UpdateUIDisplay;
        OnCardBuffed += UpdateUIDisplay;
        OnCardDamageApplied += UpdateUIDisplay;
        OnCardDead += UpdateUIDisplay;
    }

    public void SetupCardData(CardData newCardData, DeckBase ownerDeck)
    {
        // Init Reference
        InitReference();
        
        // Init default data
        cardData = newCardData;

        cardHealth = cardData.health;
        cardDamage = cardData.damage;
        cardActivatedNum = cardData.activatedDiceNums;
        cardAbility = cardData.cardAbility;

        ownerCardDeck = ownerDeck;
        
        // Register ability to handler
        RegisterAbilityToHandler();

        InitCardDisplay();
    }

    private void InitReference()
    {
        _abilityHandler = GetComponent<AbilityHandler>();
        
    }

    private void RegisterAbilityToHandler()
    {
        _abilityHandler.RegisterAbility(abilityBase: cardAbility, cardBase: this);
    }

    private void InitCardDisplay()
    {
        if (cardData == null)
        {
            Debug.LogError("Card data is null");
            return;
        }

        healthText.text = cardData.health.ToString();
        damageText.text = cardData.damage.ToString();

        string activatedDiceNum = "";
        int counter = 0;
        foreach (var diceNum in cardActivatedNum)
        {
            counter++;
            if(diceNum == true) activatedDiceNum += "|" + counter + "|";
        }
        
        activatedDiceNumText.text = activatedDiceNum;
    }

    private void UpdateUIDisplay()
    {
        healthText.text = cardHealth.ToString();
        damageText.text = cardDamage.ToString();
    }

    public int GetCardCurrentDamage()
    {
        return cardDamage;
    }

    public bool CheckCanBeActivated(int num)
    {
        if (cardActivatedNum[num-1])
        {
            return true;
        }

        return false;
    }

    public void ActivateCard()
    {
        OnCardActivated?.Invoke();
        Debug.Log($"{gameObject.name} has been activated!");
    }

    public void BuffCard(int healthAmount, int damageAmount, CardBase instigatorCard)
    {
        IncreaseCardDataInRuntime(healthAmount, damageAmount);
        if (instigatorCard != this || (instigatorCard == this && isBuffed == false))
        {
            if (instigatorCard == this)
            {
                isBuffed = true;
            }
            OnCardBuffed?.Invoke();
        }
    }

    public void DamageCard(int damageDeal)
    {
        OnCardDamaged?.Invoke();
        DecreaseCardDataInRuntime(damageDeal, 0);
        OnCardDamageApplied?.Invoke();
    }

    private void IncreaseCardDataInRuntime(int healthAmount, int damageAmount)
    {
        cardHealth += healthAmount;
        cardDamage += damageAmount;
    }

    private void DecreaseCardDataInRuntime(int healthAmount, int damageAmount)
    {
        cardHealth -= healthAmount;
        cardDamage -= damageAmount;
    }

    public DeckBase GetOwnerDeck()
    {
        return ownerCardDeck;
    }

    public bool GetCardIsBuffed()
    {
        return isBuffed;
    }

    //TODO: Delete after finishing debug
    private void OnMouseDown()
    {
        DamageCard(1);
    }
}
