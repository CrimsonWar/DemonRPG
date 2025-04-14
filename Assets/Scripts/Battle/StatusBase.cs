using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatusCat
{
    Damage,
    Heal,
    StartOfTurn,
    EndOfTurn
}
public enum StatusType
{
    Buff,
    Debuff
}

public abstract class StatusBase : MonoBehaviour
{
    public StatusCat Category;
    public StatusType Type;
    public Sprite sprite;
    public int length;
    public string Name;
    public string Description;

    public abstract void handleStatus();

}
