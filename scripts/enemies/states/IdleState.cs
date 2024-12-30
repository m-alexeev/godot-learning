using Godot;
using System;
using spacewar.scripts.enemies;
using spacewar.scripts.player;

namespace spacewar.scripts.enemies.states;
public partial class IdleState : State {
    [Export] public State SearchingState;
    [Export] public TrackingState TrackingState;
    

    [Export] public double SearchDelay;

    private Timer _timer;
    private Player _player;
    private Enemy _enemy;
    public override void Enter() {
        GD.Print("Idling");
        _enemy = GetParent().GetParent<Enemy>();
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
        
        // Setup timer
        _timer = GetNode<Timer>("Timer");
        _timer.SetWaitTime(SearchDelay);
        _timer.Timeout += OnIdleTimerEnd;
        _timer.Start();
    }
     
    public override void PhysicsUpdate(double delta) {
        // Check if player in range again 
        if (_enemy.Position.DistanceTo(_player.Position) <= TrackingState.TrackingRange) {
            GD.Print("stop idling");
            _timer.Stop();
            EmitSignal(State.SignalName.Transition, this, TrackingState);
        }
    }

    private void OnIdleTimerEnd() {
        EmitSignal(State.SignalName.Transition, this, SearchingState);
    }
}
