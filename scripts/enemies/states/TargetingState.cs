using Godot;
using System;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;
public partial class TargetingState: State {
    [Export] public State IdleState;
    [Export] public State SearchingState;
    [Export] public float TrackingRange;

    private Player _player;
    private Enemy _enemy;
    private ShipMovement _shipMovement;
    public override void Enter() {
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
        _enemy = GetParent().GetParent<Enemy>();
        _shipMovement = _enemy.GetNode<ShipMovement>("ShipMovement");
    }
     
    public override void PhysicsUpdate(double delta) {
    } 
}
