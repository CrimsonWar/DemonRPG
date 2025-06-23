using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatDefence : AttackBase
{
    public GameObject DefenseStatus;
     public override List<iUnit> possibleTargets()
    {
        List<iUnit> targets = new List<iUnit> { };
        targets.Add(BattleManager.inst.PlayerUnit1);
        targets.Add(null);
        targets.Add(null);
        targets.Add(null);
        targets.Add(null);
        targets.Add(null);
        return targets;
    }
    
    public override void doAttack()
    {
        StartCoroutine(ApplyDefense());
    }
    
    IEnumerator ApplyDefense(){
        iUnit Target = User.getTarget();
        User.PlaySupport();
        Target.ApplyStatus(DefenseStatus);
        yield return new WaitForSeconds(3);
        User.turnDone = true;
        BattleManager.inst.finishTurn();
    }
    
    public override void setAttackType()
    {
        attackType = AttackType.Ability;
    }
}
