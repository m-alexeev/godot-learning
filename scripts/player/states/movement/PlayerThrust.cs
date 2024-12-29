using Godot;
using System;
using System.Linq;
using durak.scripts.player;

public partial class PlayerThrust: State {
    [Export]
    public AnimatedSprite2D AnimatedSprite;

    [Export] public SpriteFrames ThrustSprite;

    [Export] public State IdleState;
    [Export] public State BoostState;
    
    private ShipMovement _movement;
    
    public override void Enter() {
        Node player = GetParent().GetParent();
        _movement = player.GetNode<ShipMovement>("ShipMovement");
        AnimatedSprite.SetSpriteFrames(ThrustSprite);
    }

    public override void PhysicsUpdate(double delta) {
        if (!_movement.ApplyingThrust) {
            EmitSignal(State.SignalName.Transition, this, IdleState);
        }

        if (_movement.IsBoosting) {
            EmitSignal(State.SignalName.Transition, this, BoostState);
        }
    }
}

