using System.Collections.Generic;
using System;
using System.Linq;


public class AI
{
    public Unit ChooseUnitToDeploy(List<Unit> availableUnits)
    {
        Random rand = new Random();
        return availableUnits[rand.Next(availableUnits.Count)];
    }
}
