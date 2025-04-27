using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, iUnitControl
{
    public UnitAbstract User;
    
    public void takeTurn(UnitAbstract unit){
        User = unit;
        User.turnDone = false;
        StartCoroutine(HandleTurn());
    }
    IEnumerator HandleTurn(){
        while (!User.turnDone)
        {
            yield return null;
        }
    }
    public void handleAttack (AttackBase attack) {
        StopCoroutine(SelectTarget(attack));
        StartCoroutine(SelectTarget(attack));
    }
    
    IEnumerator SelectTarget(AttackBase attack) {
        bool targetSelected = false;
        Selector.inst.resetSelector();
        List<iUnit> possibleTargets = attack.possibleTargets();
        List<iUnit> selectableTargets = new List<iUnit>{};
        iUnit Target = null;
        switch (attack.attackType)
        {
            case AttackType.Attack:
                if(possibleTargets[3] == null || !possibleTargets[3].isDead()) {
                    selectableTargets.Add(possibleTargets[3]);   
                }
                if(possibleTargets[4] == null || !possibleTargets[4].isDead()) {
                    selectableTargets.Add(possibleTargets[4]);
                }
                if(possibleTargets[5] == null || !possibleTargets[5].isDead()) {
                    selectableTargets.Add(possibleTargets[5]);
                }
                break;
            case AttackType.Ability:
                if(possibleTargets[0] == null || !possibleTargets[0].isDead()) {
                    selectableTargets.Add(possibleTargets[0]);
                }
                if(possibleTargets[1] == null || !possibleTargets[1].isDead()) {
                    selectableTargets.Add(possibleTargets[1]);
                }
                if(possibleTargets[2] == null || !possibleTargets[2].isDead()) {
                    selectableTargets.Add(possibleTargets[2]);
                }
                break;
        }
        Selector.inst.activateSelector(selectableTargets);
        while(!targetSelected) {
            Target = encounterCache.Cache.getSelectedTarget();
            if(Target != null) {
                User.setTarget(Target);
                targetSelected = true;
            }
            yield return null;
        }
        Selector.inst.resetSelector();
        attack.doAttack();

    }
}

