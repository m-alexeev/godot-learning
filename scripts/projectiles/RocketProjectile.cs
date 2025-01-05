using Godot;
using System;
using spacewar.scripts.player;

public partial class RocketProjectile : Node2D {
    private Timer _timer;
    [Export] private long _lifetime;
    [Export] private HitboxComponent _hitboxComponent;
    [Export] private ThrustMovement _movement;
    [Export] private Node2D _target;
    public override void _Ready() {
        _movement.ApplyThrust(1);
        _timer = GetNode<Timer>("FuelTimer");
        _timer.SetWaitTime(_lifetime);
        _timer.Start();
        _timer.Timeout += TimerOnTimeout;
        _hitboxComponent.AreaEntered += HitboxComponentOnAreaEntered;
    }

    public void SetTarget(Node2D target) {
        _target = target;
    }

    public override void _PhysicsProcess(double delta) {
        if (_target != null) {
            // Aim at target 
            _movement.RotateTowards(_target.Position - Position);
        }
    }
    
    private void HitboxComponentOnAreaEntered(Area2D otherArea) {
        DestroyRocket();
    }
   
    private void TimerOnTimeout() {
        DestroyRocket();
    }

    private void DestroyRocket() {
        
        // Kill rocket for now 
        QueueFree();
    }

}
