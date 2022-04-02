using Godot;
using System;

public interface IEnemyStateMachine
{
    void TransitionToState(EnemyBaseStateMachine state);
    void Update();
}
