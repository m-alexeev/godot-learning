using Godot;
using System;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;
public partial class TrackingState : State {
    [Export] public State IdleState;
    [Export] public State SearchingState;
    [Export] public TargetingState TargetingState; 
    [Export] public float TrackingRange;

    private Player _player;
    public override void Enter() {
        GD.Print("Tracking");
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
    }
     
    public override void PhysicsUpdate(double delta) {
        
    } 
}
