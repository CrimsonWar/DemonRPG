using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindupPunch : AttackBase
{
    public int damageValue = 10;
    public int baseDamage = 10;
    public GameObject QTE;
    public int Length;
    public int extraDamagePerTest = 5;
    public CoroutineQueue queue;

    private void Start() {
        queue = new CoroutineQueue(1,StartCoroutine);
    }

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

        queue.Run(attackSlide());
        queue.Run(Mash());
        queue.Run(backSlide());
        queue.Run(EndTurn());        
        yield return null;
    }

    public override void setAttackType() {
        attackType = AttackType.Attack;
    }

    IEnumerator attackSlide() {
        Vector3 targetPos = User.getTargetPos();
        yield return this.SlideToPosition(targetPos);
    }

    IEnumerator Mash(){
        User.PlayWindup();
        GameObject QTEObj = Instantiate(QTE, BattleManager.inst.UI);
        BaseQTE QTEHandler = QTEObj.GetComponent<BaseQTE>();
        QTEHandler.MaxPoints = extraDamagePerTest;
        QTEHandler.QTESeconds = Length;
        yield return new WaitForSeconds(Length);
        int points = QTEHandler.pointsGathered;
        damageValue = baseDamage + points;
        int moddedDamage = User.HandleDamageDoneStatus(damageValue);
        Destroy(QTEObj);
        iUnit Target = User.getTarget();
        User.PlayAttack();
        Target.takeDamage(moddedDamage, User.gameObject);
        yield return null;
    }

    IEnumerator backSlide(){
        yield return this.SlideToStart();
    }
    IEnumerator EndTurn(){
        User.turnDone = true;
        BattleManager.inst.finishTurn();
        yield return null;
    }
}