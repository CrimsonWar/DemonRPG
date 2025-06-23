using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iUnit
{
    GameObject gameObject { get; }

    public void setTarget(iUnit target);

    public iUnit getTarget();

    public Vector3 getTargetPos();

    public void takeDamage(int damageTaken, GameObject triggerUnit);

    public void heal(int healAmount, GameObject triggerUnit);

    public AttackBase[] getAttacks();

    public AttackBase[] getAbilities();

    public GameObject getSpriteObj();

    public int getSlot();

    public Vector3 getUnitPos();

    public void OnTurnEnd();

    public bool isDead();

    public void PlayAttack();

    public void PlayWindup();

    public void PlaySupport();

    public void PlayDamageTaken();

    public void handleSelectedAttack(AttackBase attack);

    public void ApplyStatus(GameObject status);

    public void CheckStatusExpiration();
}
