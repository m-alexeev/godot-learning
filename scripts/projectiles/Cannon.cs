using Godot;
using System;

public partial class Cannon : Node2D {
    [Export] public int InitialVelocity;
    private Timer _timer;
    private Vector2 _dir;
    
    public override void _Ready() {
        _timer = GetNode<Timer>("Lifetime");
        _timer.Timeout += OnLifetimeEnd;
    }

    public void SetDirection(Vector2 dir) {
        _dir = dir;
    }
    
    public override void _Process(double delta) {
        Position += _dir * InitialVelocity * (float)delta;
    }

    private void OnLifetimeEnd() {
        QueueFree();
    }
}
