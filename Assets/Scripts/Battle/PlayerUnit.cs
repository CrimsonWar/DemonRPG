using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit: UnitAbstract
{
    void Start()
    {
        this.setupUnit();
        this.setupPlayer();
    }

    public void setHP (int hp) 
    {
        currentHP = hp;
        HPBar.SetHealth(hp);
    }

    private void setupPlayer () {
        switch (PartySlot)
        {
            case 1:
                this.setHP(PartyManager.inst.getHealthSlot1());
                break;
            case 2:
                this.setHP(PartyManager.inst.getHealthSlot2());
                break;
            case 3:
                this.setHP(PartyManager.inst.getHealthSlot3());
                break;
        }
    }

    public void startTurn () {
        this.OnTurnStart();
        if (unitState != UnitState.Dead)
        {
            GetComponent<iUnitControl>().takeTurn(this);
        }
        else
        {
            StartCoroutine(Died());
        }
    }

    public override Vector3 getTargetPos() {
        float distance = -0.5f;
        Vector3 targetPos = Target.getSpriteObj().transform.position;
        targetPos.x = targetPos.x + distance;
        return targetPos;
    }

    IEnumerator Died() {
        yield return new WaitForSeconds(3);
        this.OnTurnEnd();
    }
}
