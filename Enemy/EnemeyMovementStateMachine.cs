using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyMovementStateMachine 
{
    public IEnemyMovementState CurrentState;

    public EnemeyMovementStateMachine(IEnemyMovementState firstState)
    {
        CurrentState = firstState;
    }
    public void ChangeState(IEnemyMovementState nextState)
    {
        CurrentState.OnExit();
        CurrentState = nextState;
        CurrentState.OnEnter();
    }
}
