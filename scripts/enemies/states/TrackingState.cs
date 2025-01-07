using Godot;
using System;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;
public partial class TrackingState : State {
    [Export] public State IdleState;
    [Export] public State SearchingState;
    [Export] public TargetingState TargetingState; 
    [Export] public float TrackingRange;
    [Export] public AnimatedSprite2D AnimatedSprite2D;

    private Player _player;
    private Enemy _enemy;
    private ThrustMovement _thrustMovement;
    public override void Enter() {
        AnimatedSprite2D.SetAnimation("move");
        AnimatedSprite2D.Play();
        
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
        _enemy = GetParent().GetParent<Enemy>();
        _thrustMovement = _enemy.GetNode<ThrustMovement>("ThrustMovement");
    }
     
    public override void PhysicsUpdate(double delta) {
        if (_enemy.Position.DistanceTo(_player.Position) < TargetingState.TargetingRange) {
            // Change to targeting
            EmitSignal(State.SignalName.Transition, this, TargetingState);
        }
        else {
           // Track player 
           _thrustMovement.ApplyThrust(1);
           _thrustMovement.RotateTowards(_player.Position - _enemy.Position);
        }    
    } 
}
