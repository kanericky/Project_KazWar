using System.Collections.Generic;

public interface IActionExecutable
{
    public void ExecuteAction(List<CardBase> targetCards, CardBase instigatorCard);
}