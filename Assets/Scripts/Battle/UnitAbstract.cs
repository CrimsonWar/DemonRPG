using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum UnitState
{
    Active,
    OnTheBrink,
    Dead
}

public abstract class UnitAbstract : MonoBehaviour,iUnit
{

    public Sprite sprite;
    public string unitName;
    public int maxHP = 100;
    public int currentHP = 0;
    public int speed;
    public float saveValue = 10f;
    public HPBar HPBar;
    public SpriteRenderer spriteRenderer;
    public AttackBase[] attacks;
    public AttackBase[] abilities;
    public Sprite menuIcon;
    public UnitState unitState;
    public iUnit Target;
    public StatusBase[] statuses;
    public GameObject DamagePopup, HealPopup, TextPopup;
    public bool turnDone = false;

    private Animator _animator;
    private bool isDone = false;
    private bool doingAnother = false;
    protected int PartySlot;

    public void setupUnit () {
        currentHP = maxHP;
        HPBar.SetMaxHealth(maxHP);
        spriteRenderer.sprite = sprite;
        SetupAttacks();
        unitState = UnitState.Active;
    }

    private void SetupAttacks (){
        foreach(var element in attacks) {
            element.setUser(GetComponent<UnitAbstract>());
        }
        foreach(var element in abilities) {
            element.setUser(GetComponent<UnitAbstract>());
        }
    }

    public void setPartySlot (int Slot) {
        PartySlot = Slot;
    }

    public void setTarget(iUnit target) {
        Target = target;
    }

    public iUnit getTarget()
    {
        return Target;
    }

    public abstract Vector3 getTargetPos();

    public AttackBase[] getAttacks () {
        return attacks;
    }

    public AttackBase[] getAbilities () {
        return abilities;
    }

    public void setAnimator (Animator specificAnimator) {
        _animator = specificAnimator;
    }

    public virtual void takeDamage(int damageTaken) {
        GameObject dmgText = Instantiate(DamagePopup, gameObject.transform);
        dmgText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damageTaken.ToString());
        if(unitState == UnitState.OnTheBrink) {
            saveValue++;
            this.DeathSave();
        } else {
            currentHP -= damageTaken;
            if(currentHP <= 0) {
                currentHP = 0;
                GameObject brinkText = Instantiate(TextPopup, gameObject.transform);
                brinkText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("HOLDING ON!");
                unitState = UnitState.OnTheBrink;
            }
            HPBar.SetHealth(currentHP);
        }
    }

    public void heal(int healAmount) {
        if(unitState != UnitState.Dead) {
            GameObject healText = Instantiate(HealPopup, gameObject.transform);
            healText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(healAmount.ToString());
            currentHP += healAmount;
            if(currentHP > maxHP) {
                currentHP = maxHP;
            }
            HPBar.SetHealth(currentHP);
            if(currentHP > 0 && unitState == UnitState.OnTheBrink) {
                unitState = UnitState.Active;
            }
        }
    }

    public GameObject getSpriteObj() {
        return spriteRenderer.gameObject;
    }

    public Vector3 getUnitPos(){
        Vector3 pos = transform.position;
        return pos;
    }
    
    public int getSlot () {
        return PartySlot;
    }

    public void DeathSave () {
        float RolledSave = Random.Range(1, 20);
        if(RolledSave < saveValue) {
            GameObject deadText = Instantiate(TextPopup, gameObject.transform);
            deadText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("DEAD!");
            unitState = UnitState.Dead;
        } 
        else
        {
            GameObject brinkText = Instantiate(TextPopup, gameObject.transform);
            brinkText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("HOLDING ON!");
        }

    }
    public bool isDead () {
        if(unitState == UnitState.Dead) {
            return true;
        } else {
            return false;
        }
    }
    public bool isTurnDone(){
        if(isDone) {
            isDone = false;
            return true;
        } else {
            return false;
        }
    }

    public void OnTurnStart () {
        isDone = false;
        if(unitState == UnitState.OnTheBrink) {
            this.DeathSave();
        }
    }

    public void OnTurnEnd () {
       //for status later use os status
    }

    public void handleSelectedAttack (AttackBase attack) {
        GetComponent<iUnitControl>().handleAttack(attack);
    }

    public void PlayAttack () {
        //play attack animation
    }

    public void PlayWindup () {
        //play windup animation
    }
    public void PlaySupport () {
        //play support animation
    }

    public void PlayDamageTaken () {
        //play damage taken animation
    }

    private void IdleAnimation () {
        if(!doingAnother) {
            switch (unitState)
            {
                case UnitState.Active:
                break;
                case UnitState.OnTheBrink:
                break;
                case UnitState.Dead:
                break;
            }
        }
    }
}