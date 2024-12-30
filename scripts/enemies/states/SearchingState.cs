using Godot;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;

public partial class SearchingState: State {
    [Export] public State IdleState;
    [Export] public TrackingState TrackingState;
    
    private Player _player;
    private Enemy _enemy;

    private ShipMovement _shipMovement;
    public override void Enter() {
        GD.Print("Searching");
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
        _enemy = GetParent().GetParent<Enemy>();
        _shipMovement = _enemy.GetNode<ShipMovement>("ShipMovement");
    }
     
    public override void PhysicsUpdate(double delta) {
        if (_enemy.Position.DistanceTo(_player.Position) <= TrackingState.TrackingRange) {
            EmitSignal(State.SignalName.Transition, this, TrackingState);
        }
    }   
}