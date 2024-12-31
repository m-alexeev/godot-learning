using Godot;
using System;

public partial class State : Node2D {
    [Signal]
    public delegate void TransitionEventHandler(State state, State nextState);
    public virtual void Enter(){}

    public virtual void Exit(){}

    public virtual void Update(double delta){}

    public virtual void PhysicsUpdate(double delta){}
}
