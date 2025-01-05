using System.Transactions;
using Godot;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;

public partial class SearchingState: State {
    [Export] public State IdleState;
    [Export] public TrackingState TrackingState;
    [Export] public float WanderRange;
    [Export] public AnimatedSprite2D AnimatedSprite2D;
    
    private Player _player;
    private Enemy _enemy;

    private Vector2 _nextLocation;
    private const float Tolerance = 10; 
    
    private ThrustMovement _thrustMovement;
    public override void Enter() {
        AnimatedSprite2D.SetAnimation("move");
        AnimatedSprite2D.Play();
        GD.Print("Searching");
        
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
        _enemy = GetParent().GetParent<Enemy>();
        _thrustMovement = _enemy.GetNode<ThrustMovement>("ThrustMovement");
        ChooseNextSearchLocation();
    }

    public override void PhysicsUpdate(double delta) {
        if (_enemy.Position.DistanceTo(_player.Position) <= TrackingState.TrackingRange) {
            _thrustMovement.ApplyThrust(0);
            EmitSignal(State.SignalName.Transition, this, TrackingState);
        }
        else if (_enemy.Position.DistanceTo(_nextLocation) < Tolerance) {
            EmitSignal(State.SignalName.Transition, this, IdleState);
        }
        else {
           _thrustMovement.RotateTowards(_nextLocation - _enemy.Position); 
           _thrustMovement.ApplyThrust(1);
        }
    }

    private void ChooseNextSearchLocation() {
        GD.Print("Next location");
        _nextLocation = _enemy.Position + new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1,1)).Normalized() * WanderRange;
        GD.Print(_nextLocation);
    }
}