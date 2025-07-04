using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHeal : AttackBase
{
    public int healAmount = 10;

     public override List<iUnit> possibleTargets() {
        List<iUnit> targets = new List<iUnit>{};
        targets.Add(BattleManager.inst.PlayerUnit1);
        targets.Add(BattleManager.inst.PlayerUnit2);
        targets.Add(BattleManager.inst.PlayerUnit3);
        targets.Add(BattleManager.inst.EnemyUnit1);
        targets.Add(BattleManager.inst.EnemyUnit2);
        targets.Add(BattleManager.inst.EnemyUnit3);
        return targets;
    }
    public override void doAttack() {
                
        StartCoroutine(doHeal());
        
    }

    IEnumerator doHeal(){
        iUnit Target = User.getTarget();
        int moddedHeal = User.HandleHealingDoneStatus(healAmount);
        User.PlaySupport();
        Target.heal(moddedHeal, User.gameObject);
        yield return new WaitForSeconds(3);
        User.turnDone = true;
        BattleManager.inst.finishTurn();
    }
    
    public override void setAttackType() {
        attackType = AttackType.Ability;
    }
}
