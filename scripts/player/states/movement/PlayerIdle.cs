using Godot;

namespace spacewar.scripts.player.states.movement;

public partial class PlayerIdle : State {
    [Export]
    public AnimatedSprite2D AnimatedSprite;

    [Export] public SpriteFrames IdleSprite;
    [Export] public State ThrustState;
    [Export] public State BoostState;

    private ShipMovement _movement;
    
    public override void Enter() {
        Node player = GetParent().GetParent();
        _movement = player.GetNode<ShipMovement>("ShipMovement");
        AnimatedSprite.SetSpriteFrames(IdleSprite);
    }

    public override void PhysicsUpdate(double delta) {
        // Get velocity
        if (_movement.ApplyingThrust) {
            EmitSignal(State.SignalName.Transition, this, ThrustState);
        } 
        if (_movement.IsBoosting) {
            EmitSignal(State.SignalName.Transition, this, BoostState);
        }
    }
}