using Godot;
using spacewar.scripts.player;

public partial class PlayerBoost : State {
    [Export]
    public AnimatedSprite2D AnimatedSprite;

    [Export] public State ThrustState;
    [Export] public State IdleState;
    
    private ShipMovement _movement;
    
    public override void Enter() {
        Node player = GetParent().GetParent();
        _movement = player.GetNode<ShipMovement>("ShipMovement");
        AnimatedSprite.SetAnimation("boost");
        AnimatedSprite.Play();
    }

    public override void PhysicsUpdate(double delta) {
        if (!_movement.IsBoosting && _movement.ApplyingThrust) {
            EmitSignal(State.SignalName.Transition, this, ThrustState);
        }

        if (!_movement.IsBoosting && !_movement.ApplyingThrust) {
            EmitSignal(State.SignalName.Transition, this, IdleState);
        }
    }
}

