using Godot;

namespace spacewar.scripts.player.states.movement;

public partial class PlayerThrust: State {
    [Export]
    public AnimatedSprite2D AnimatedSprite;

    [Export] public State IdleState;
    [Export] public State BoostState;
    
    private ThrustMovement _movement;
    
    public override void Enter() {
        Node player = GetParent().GetParent();
        _movement = player.GetNode<ThrustMovement>("ThrustMovement");
        AnimatedSprite.SetAnimation("move");
        AnimatedSprite.Play();
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