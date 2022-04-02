using Godot;
using System;

public abstract class PlayerBaseStateMachine
{
    public abstract void OnStateEnter(IPlayerStateMachine stateMachine, ObjPlayer owner);
    public abstract void OnStateUpdate(IPlayerStateMachine stateMachine, ObjPlayer owner);
    public abstract void OnStateExit(IPlayerStateMachine stateMachine, ObjPlayer owner);
}
