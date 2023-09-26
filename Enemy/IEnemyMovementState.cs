using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovementState 
{
    public void OnEnter();
    public void OnLogic();
    public void OnExit();
}
