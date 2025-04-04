using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iUnitControl
{
    public void takeTurn(UnitAbstract unit);
    public void handleAttack(AttackBase attack);
}