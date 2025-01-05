using Godot;
using System;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;
public partial class TargetingState: State {
    [Export] public State TrackingState;
    [Export] public float TargetingRange;
    [Export] public AnimatedSprite2D AnimatedSprite2D;

    private Player _player;
    private Enemy _enemy;
    private ThrustMovement _thrustMovement;
    public override void Enter() {
        AnimatedSprite2D.SetAnimation("idle");
        AnimatedSprite2D.Play();
        
        GD.Print("Targeting");
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
        _enemy = GetParent().GetParent<Enemy>();
        _thrustMovement = _enemy.GetNode<ThrustMovement>("ThrustMovement");
    }
     
    public override void PhysicsUpdate(double delta) {
        if (_enemy.Position.DistanceTo(_player.Position) > TargetingRange) {
            EmitSignal(State.SignalName.Transition, this, TrackingState);
        }
        else {
            _thrustMovement.RotateTowards(_player.Position - _enemy.Position);
        }
    } 
}
