using Godot;
using System;

public interface IPlayerStateMachine
{
    void TransitionToState(PlayerBaseStateMachine state);
    void Update();
}
