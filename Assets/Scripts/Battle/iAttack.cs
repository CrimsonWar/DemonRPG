using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iAttack
{
    GameObject gameObject { get ; } 

    void attackSelected ();
    public List<iUnit> possibleTargets();
    public void doAttack();
}
