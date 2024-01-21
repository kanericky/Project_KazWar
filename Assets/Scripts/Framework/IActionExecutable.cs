using System.Collections.Generic;
using Card;

public interface IActionExecutable
{
    public void ExecuteAction(List<CardBase> targetCards, CardBase instigatorCard);
}