using System.Transactions;
using Godot;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;

public partial class SearchingState: State {
    [Export] public State IdleState;
    [Export] public TrackingState TrackingState;
    [Export] public float WanderRange;
    
    private Player _player;
    private Enemy _enemy;

    private Vector2 _nextLocation;
    private const float Tolerance = 10; 
    
    private ShipMovement _shipMovement;
    public override void Enter() {
        GD.Print("Searching");
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
        _enemy = GetParent().GetParent<Enemy>();
        _shipMovement = _enemy.GetNode<ShipMovement>("ShipMovement");
        ChooseNextSearchLocation();
    }

    public override void PhysicsUpdate(double delta) {
        if (_enemy.Position.DistanceTo(_player.Position) <= TrackingState.TrackingRange) {
            EmitSignal(State.SignalName.Transition, this, TrackingState);
        }
        if (_enemy.Position.DistanceTo(_nextLocation) < Tolerance) {
            EmitSignal(State.SignalName.Transition, this, IdleState);
        }
        else {
            // Look at _nextLocation and move towards it 
           _shipMovement.RotateTowards(_nextLocation - _enemy.Position); 
           _shipMovement.ApplyThrust(1);
        }
    }

    private void ChooseNextSearchLocation() {
        GD.Print("Next location");
        _nextLocation = _enemy.Position + new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1,1)).Normalized() * WanderRange;
        GD.Print(_nextLocation);
    }
}