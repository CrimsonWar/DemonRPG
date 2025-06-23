using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatusCat
{
    DamageDone,
    Healed,
    DamageTaken,
    HealingDone,
    StartOfTurn,
    EndOfTurn
}
public enum StatusType
{
    Buff,
    Debuff
}

public enum StatusReapplyType
{
    RankUp, //increase Rank of Status(stack to a max Value)
    Immune, //only one of this status can be applied
    RefreshLength, //update length to new value
    Multiple //multiple Instances Possible
}

public struct HandleData
{
    public int handleValue;
    public GameObject baseUnit;
    public GameObject triggerUnit;
    public bool skipTurn;
}

public abstract class StatusBase : MonoBehaviour
{
    public StatusCat Category;
    public StatusType Type;
    public StatusReapplyType reapplyType;
    public Sprite sprite;
    public int length;
    public int rank;
    public string Name;
    public string Description;

    public abstract HandleData handleStatus(HandleData handleData);

    public StatusReapplyType GetReapplyType()
    {
        return reapplyType;
    }

}
