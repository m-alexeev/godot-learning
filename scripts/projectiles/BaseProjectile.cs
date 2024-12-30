using Godot;

namespace spacewar.scripts.projectiles;

public partial class BaseProjectile : Area2D{
    [Export] public int InitialVelocity = 500;
    [Export] public Vector2 InitialDirection = new (1,0);
    [Export] public double Lifetime = 5;

    private Timer _timer;
    private Vector2 _direction;
    
    public override void _Ready() {
        _timer = GetNode<Timer>("Lifetime");
        _timer.SetWaitTime(Lifetime);
        _timer.Timeout += OnTimeout;
        _timer.Start();
        _direction = InitialDirection;
    }

    public override void _PhysicsProcess(double delta) {
        Vector2 forwardDirection = InitialDirection.Rotated(Rotation);
        Position += InitialVelocity * forwardDirection* (float)delta;
    }

    private void OnTimeout() {
        // Delete projectile after timer runs out 
        QueueFree();
    }
}
