using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Ability,
    Attack
}
public abstract class AttackBase : MonoBehaviour, iAttack
{
    public AttackBase inst;
    public iUnit User;
    public string attackName;
    public string description;
    public Vector3 startPosition;
    public float duration = 1f;
    public AttackType attackType;

    void Awake()
    {
        inst = this;
        this.setAttackType();
    }
    public void setUser (iUnit user) {
        User = user;
        startPosition = User.getSpriteObj().transform.position;
    }

    public abstract void setAttackType();

    public void attackSelected () {
        User.handleSelectedAttack(inst);
    }

    public abstract List<iUnit> possibleTargets();
    public abstract void doAttack();

    public IEnumerator SlideToPosition(Vector3 targetPosition) {
        float timeElapsed = 0;
        User.getSpriteObj().GetComponent<SpriteRenderer>().sortingOrder = 10;

        while (timeElapsed < duration)
        {
            User.getSpriteObj().transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        User.getSpriteObj().transform.position = targetPosition;
    }

    public IEnumerator SlideToStart() {
        float timeElapsed = 0;
        Vector3 currentPosition = User.getSpriteObj().transform.position;

        while (timeElapsed < duration)
        {
            User.getSpriteObj().transform.position = Vector3.Lerp(currentPosition, startPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        User.getSpriteObj().transform.position = startPosition;
        User.getSpriteObj().GetComponent<SpriteRenderer>().sortingOrder = 5;
    }
}
