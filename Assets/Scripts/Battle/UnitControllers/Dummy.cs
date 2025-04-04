using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, iUnitControl
{
    public UnitAbstract User;
    public void takeTurn(UnitAbstract unit){
        User = unit;
        StartCoroutine(HandleTurn());
    }
    public IEnumerator HandleTurn(){
        AttackBase[] attacks = User.getAttacks();
        attacks[0].setUser(User);
        yield return new WaitForSeconds(3);
        attacks[0].attackSelected();
        
    }

    public void handleAttack (AttackBase attack) {
        List<iUnit> Targets = attack.possibleTargets();
        iUnit target = getPlayerTarget(Targets);
        User.setTarget(target);
        attack.doAttack();
    }

    public iUnit getPlayerTarget (List<iUnit> targets) {
        iUnit target = null;
        for(int i = 0; i < 3; i++) {
            if(target == null) {
                if ((targets[i] != null) && !targets[i].isDead())
                {
                    target = targets[i];
                }
            }
        }
        return target;
    }
}
