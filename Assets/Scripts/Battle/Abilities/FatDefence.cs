using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatDefence : AttackBase
{
     public override List<iUnit> possibleTargets() {
        List<iUnit> targets = new List<iUnit>{};
        targets.Add(BattleManager.inst.PlayerUnit1);
        return targets;
    }
    public override void doAttack() {
                
        
        
    }
    
    public override void setAttackType() {
        attackType = AttackType.Ability;
    }
}
