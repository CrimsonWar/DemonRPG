using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePunch : AttackBase
{
    public int damageValue = 10;

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
                
        StartCoroutine(doDamage());
        
    }
    
    IEnumerator doDamage() {
        Vector3 targetPos = User.getTargetPos();
        yield return this.SlideToPosition(targetPos);
        iUnit Target = User.getTarget();
        User.PlayAttack();
        Target.takeDamage(damageValue);
        yield return this.SlideToStart();
        User.turnDone = true;
        BattleManager.inst.finishTurn();
    }

    public override void setAttackType() {
        attackType = AttackType.Attack;
    }
}
