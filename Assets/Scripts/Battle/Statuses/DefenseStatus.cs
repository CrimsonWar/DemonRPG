using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStatus : StatusBase
{
    public int DamageDivider;

    public override HandleData handleStatus(HandleData handleData)
    {
        HandleData newData = handleData;
        newData.handleValue = handleData.handleValue / DamageDivider;
        return newData;
    }
}
