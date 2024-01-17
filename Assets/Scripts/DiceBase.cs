using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceBase : MonoBehaviour
{
    [Header("Dice Data")] 
    [SerializeField] private int diceFaceAmount = 6;
    [SerializeField] private int currentNumber = 1;

    [Header("UI Display")] 
    [SerializeField] private TMP_Text diceAmountText;
    
    [Header("Runtime")]
    private Vector3 _offset = Vector3.zero;
    private Vector3 _defaultPos = Vector3.zero;

    private DicePool _dicePool = null;
    
    [SerializeField] private GameObject colliderDetection = null;

    public void InitDice(DicePool dicePool)
    {
        // Init UI
        diceAmountText.text = currentNumber.ToString();

        // Init runtime data
        if (dicePool == null) return;
        _dicePool = dicePool;
        _defaultPos = transform.position;
        
        // Roll
        Roll();
    }

    public int GetCurrentDiceNum()
    {
        return currentNumber;
    }

    public void Roll()
    {
        int newNumber = Random.Range(1, diceFaceAmount+1);
        currentNumber = newNumber;
        UpdateDiceUIDisplay();
    }

    private void UpdateDiceUIDisplay()
    {
        diceAmountText.text = currentNumber.ToString();
    }
    
    private void OnMouseDown()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - MousePos;
    }

    private void OnMouseDrag()
    {
        if (Camera.main == null) return;
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = (Vector2) (MousePos + _offset);
    }

    private void OnMouseUp()
    {
        if (colliderDetection == null)
        {
            transform.position = _defaultPos;
            return;
        }

        // Target is eligible to put dice 
        if (colliderDetection.CompareTag("DiceSlot"))
        {
            DiceSlot targetDiceSlot = colliderDetection.GetComponent<DiceSlot>();
            bool actionResult = targetDiceSlot.FillSlot(this);
            if (actionResult)
            {
                transform.position = colliderDetection.transform.position;
                transform.parent = targetDiceSlot.transform;
                _dicePool.RemoveDiceFromPool(this);
                GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                transform.position = _defaultPos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DiceSlot"))
        {
            if(!collision.GetComponent<DiceSlot>().GetSlotIsFilled())
                colliderDetection = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("DiceSlot"))
            return;
            
        if(!collision.GetComponent<DiceSlot>().GetSlotIsFilled() && colliderDetection == null)
            colliderDetection = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliderDetection = null;
    }
}
