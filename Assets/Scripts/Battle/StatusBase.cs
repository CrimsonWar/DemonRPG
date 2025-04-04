using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatusCat
{
    Damage,
    Heal,
    Bonus
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

    public abstract void handleStatus();

}
