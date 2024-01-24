using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceBase : MonoBehaviour, IDraggable
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
    
    public void OnMouseDown()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - MousePos;
    }

    public void OnMouseDrag()
    {
        if (Camera.main == null) return;
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = (Vector2) (MousePos + _offset);
    }

    public void OnMouseUp()
    {
        CheckAndPlace();
    }

    public void CheckAndPlace()
    {
        int layerMask = ~(1 << LayerMask.NameToLayer("Draggable"));
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            DiceSlot diceSlot = hit.collider.GetComponent<DiceSlot>();

            if (diceSlot != null)
            {
                PlaceDiceInSlot(diceSlot);
            }
            else
            {
                ResetToOriginPos();
            }
        }
        else
        {
            ResetToOriginPos();
        }
    }

    private void PlaceDiceInSlot(DiceSlot slot)
    {
        transform.position = slot.transform.position;
        transform.parent = slot.transform;
        slot.FillSlot(this);
    }

    private void ResetToOriginPos()
    {
        transform.position = _defaultPos;
    }
}
