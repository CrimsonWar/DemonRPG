using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        else
        {
            StartCoroutine(Died());
        }
    }

    public override Vector3 getTargetPos() {
        float distance = 0.5f;
        Vector3 targetPos = Target.getSpriteObj().transform.position;
        targetPos.x = targetPos.x + distance;
        return targetPos;
    }

    IEnumerator Died() {
        yield return new WaitForSeconds(3);
        this.OnTurnEnd();
    }
}
