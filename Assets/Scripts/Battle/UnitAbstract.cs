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

public abstract class UnitAbstract : MonoBehaviour, iUnit
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
    public GameObject DamagePopup, HealPopup, TextPopup;
    public bool turnDone = false;
    public GameObject StatusBar;

    private Animator _animator;
    private bool isDone = false;
    private bool doingAnother = false;
    protected int PartySlot;

    public void setupUnit()
    {
        currentHP = maxHP;
        HPBar.SetMaxHealth(maxHP);
        spriteRenderer.sprite = sprite;
        SetupAttacks();
        unitState = UnitState.Active;
    }

    private void SetupAttacks()
    {
        foreach (var element in attacks)
        {
            element.setUser(GetComponent<UnitAbstract>());
        }
        foreach (var element in abilities)
        {
            element.setUser(GetComponent<UnitAbstract>());
        }
    }

    public void setPartySlot(int Slot)
    {
        PartySlot = Slot;
    }

    public void setTarget(iUnit target)
    {
        Target = target;
    }

    public iUnit getTarget()
    {
        return Target;
    }

    public abstract Vector3 getTargetPos();

    public AttackBase[] getAttacks()
    {
        return attacks;
    }

    public AttackBase[] getAbilities()
    {
        return abilities;
    }

    public void setAnimator(Animator specificAnimator)
    {
        _animator = specificAnimator;
    }

    public virtual void takeDamage(int damageTaken, GameObject triggerUnit)
    {
        damageTaken = HandleDamageTakenStatus(damageTaken, triggerUnit);
        GameObject dmgText = Instantiate(DamagePopup, gameObject.transform);
        dmgText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damageTaken.ToString());
        if (unitState == UnitState.OnTheBrink)
        {
            saveValue++;
            this.DeathSave();
        }
        else
        {
            currentHP -= damageTaken;
            if (currentHP <= 0)
            {
                currentHP = 0;
                GameObject brinkText = Instantiate(TextPopup, gameObject.transform);
                brinkText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("HOLDING ON!");
                unitState = UnitState.OnTheBrink;
            }
            HPBar.SetHealth(currentHP);
        }
    }

    public void heal(int healAmount, GameObject triggerUnit)
    {
        if (unitState != UnitState.Dead)
        {
            healAmount = HandleHealedStatus(healAmount, triggerUnit);
            GameObject healText = Instantiate(HealPopup, gameObject.transform);
            healText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(healAmount.ToString());
            currentHP += healAmount;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
            HPBar.SetHealth(currentHP);
            if (currentHP > 0 && unitState == UnitState.OnTheBrink)
            {
                unitState = UnitState.Active;
            }
        }
    }

    public GameObject getSpriteObj()
    {
        return spriteRenderer.gameObject;
    }

    public Vector3 getUnitPos()
    {
        Vector3 pos = transform.position;
        return pos;
    }

    public int getSlot()
    {
        return PartySlot;
    }

    public void DeathSave()
    {
        float RolledSave = Random.Range(1, 20);
        if (RolledSave < saveValue)
        {
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
    public bool isDead()
    {
        if (unitState == UnitState.Dead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isTurnDone()
    {
        if (isDone)
        {
            OnTurnEnd();
            isDone = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnTurnStart()
    {
        isDone = false;
        HandleTurnStartStatus();
        if (unitState == UnitState.OnTheBrink)
        {
            this.DeathSave();
        }
    }

    public void OnTurnEnd()
    {
        HandleTurnEndStatus();
    }

    public void handleSelectedAttack(AttackBase attack)
    {
        GetComponent<iUnitControl>().handleAttack(attack);
    }

    public void PlayAttack()
    {
        //play attack animation
    }

    public void PlayWindup()
    {
        //play windup animation
    }
    public void PlaySupport()
    {
        //play support animation
    }

    public void PlayDamageTaken()
    {
        //play damage taken animation
    }

    private void IdleAnimation()
    {
        if (!doingAnother)
        {
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

    public void ApplyStatus(GameObject status)
    {
        StatusBar.GetComponent<StatusManager>().AddStatus(status);
        string statusName = $"+ {status.GetComponent<StatusBase>().Name}";
        GameObject statusText = Instantiate(TextPopup, gameObject.transform);
        statusText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(statusName);
    }

    public void CheckStatusExpiration()
    {
        List<GameObject> activeStatuses = StatusBar.GetComponent<StatusManager>().ActiveStatuses;
        foreach (GameObject Status in activeStatuses)
        {
            GameObject StatObj = Status.GetComponent<StatusSpriteTemplate>().status;
            StatObj.GetComponent<StatusBase>().length = StatObj.GetComponent<StatusBase>().length - 1;
            if (StatObj.GetComponent<StatusBase>().length == 0)
            {
                string statusName = $"- {StatObj.GetComponent<StatusBase>().Name}";
                StatusBar.GetComponent<StatusManager>().RemoveStatus(Status);
                GameObject statusText = Instantiate(TextPopup, gameObject.transform);
                statusText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(statusName);
            }
        }
    }

    protected void HandleTurnStartStatus()
    {
        HandleData Data = new HandleData
        {
            handleValue = 0,
            baseUnit = this.gameObject,
            triggerUnit = null,
            skipTurn = false
        };

        List<GameObject> TurnStartStatusList = StatusBar.GetComponent<StatusManager>().GetStatusesByCat(StatusCat.StartOfTurn);
        foreach (GameObject status in TurnStartStatusList)
        {
            Data = status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().handleStatus(Data);
            if(Data.skipTurn) {
                //method to skip turn here
                break;
            }
        }

    }

    protected int HandleDamageTakenStatus(int dmgAmount, GameObject triggerUnit)
    {
        HandleData Data = new HandleData
        {
            handleValue = dmgAmount,
            baseUnit = this.gameObject,
            triggerUnit = triggerUnit,
            skipTurn = false
        };

        List<GameObject> DmgStatusList = StatusBar.GetComponent<StatusManager>().GetStatusesByCat(StatusCat.DamageTaken);
        foreach (GameObject status in DmgStatusList)
        {
            Data = status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().handleStatus(Data);
        }

        return Data.handleValue;
    }

    protected int HandleHealedStatus(int healAmount, GameObject triggerUnit)
    {
        HandleData Data = new HandleData
        {
            handleValue = healAmount,
            baseUnit = this.gameObject,
            triggerUnit = triggerUnit,
            skipTurn = false
        };

        List<GameObject> HealedStatusList = StatusBar.GetComponent<StatusManager>().GetStatusesByCat(StatusCat.Healed);
        foreach (GameObject status in HealedStatusList)
        {
            Data = status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().handleStatus(Data);
        }

        return Data.handleValue;
    }

    protected void HandleTurnEndStatus()
    {
        HandleData Data = new HandleData
        {
            handleValue = 0,
            baseUnit = this.gameObject,
            triggerUnit = null,
            skipTurn = false
        };

        List<GameObject> TurnEndStatusList = StatusBar.GetComponent<StatusManager>().GetStatusesByCat(StatusCat.EndOfTurn);
        foreach (GameObject status in TurnEndStatusList)
        {
            Data = status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().handleStatus(Data);
        }
    }

    public int HandleDamageDoneStatus(int dmgAmount)
    {
        HandleData Data = new HandleData
        {
            handleValue = dmgAmount,
            baseUnit = this.gameObject,
            triggerUnit = null,
            skipTurn = false
        };

        List<GameObject> DmgStatusList = StatusBar.GetComponent<StatusManager>().GetStatusesByCat(StatusCat.DamageDone);
        foreach (GameObject status in DmgStatusList)
        {
            Data = status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().handleStatus(Data);
        }

        return Data.handleValue;

    }

    public int HandleHealingDoneStatus(int healAmount)
    {
        HandleData Data = new HandleData
        {
            handleValue = healAmount,
            baseUnit = this.gameObject,
            triggerUnit = null,
            skipTurn = false
        };

        List<GameObject> HealDoneStatusList = StatusBar.GetComponent<StatusManager>().GetStatusesByCat(StatusCat.HealingDone);
        foreach (GameObject status in HealDoneStatusList)
        {
            Data = status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().handleStatus(Data);
        }

        return Data.handleValue;
        
    }

}