using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUnit : UnitAbstract
{
    // Start is called before the first frame update
    void Start()
    {
        this.setupUnit();
    }

    public void startTurn () {
        this.OnTurnStart();
        if (unitState != UnitState.Dead)
        {
            GetComponent<iUnitControl>().takeTurn(this);
        }
    }

    public override Vector3 getTargetPos() {
        float distance = 0.5f;
        Vector3 targetPos = Target.getSpriteObj().transform.position;
        targetPos.x = targetPos.x + distance;
        return targetPos;
    }

    public override void takeDamage(int damageTaken, GameObject triggerUnit)
    {
        damageTaken = HandleDamageTakenStatus(damageTaken, triggerUnit);
        GameObject dmgText = Instantiate(DamagePopup, gameObject.transform);
        dmgText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damageTaken.ToString());
        currentHP -= damageTaken;
        if(currentHP <= 0) {
            currentHP = 0;
            GameObject deadText = Instantiate(TextPopup, gameObject.transform);
            deadText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("DEAD!");
            unitState = UnitState.Dead;
        }
        HPBar.SetHealth(currentHP);
    }
}
