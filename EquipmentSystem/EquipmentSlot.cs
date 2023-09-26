using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentType Type;

    public bool IsEmpty()
    {
        int result = transform.childCount;
        return result == 0;
    }
}
