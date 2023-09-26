using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    public bool IsEquipped { get; set; }
    public int ItemIndex { get;}
    public EquipmentType Type { get; set; }
    public void OnEquip();
    public void OnUnEquip();
}
