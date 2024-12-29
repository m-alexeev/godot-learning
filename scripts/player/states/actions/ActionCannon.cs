using Godot;

namespace durak.scripts.player.states.actions;

public partial class ActionCannon: State {

    [Export] public State IdleState;
    [Export] public SpriteFrames AttackAnimation;
    
    [Export] public ulong AttackCooldown;
    [Export] public PackedScene CannonProjectile; 
    [Export] public Marker2D Cannon1Spawn;
    [Export] public Marker2D Cannon2Spawn;
    
    private AnimatedSprite2D _animatedSprite2D;
    private Node2D _parent;
    private Node2D _gameNode;
    private ulong _lastAttack = 0;
    
    public override void Enter() {
        _parent = (Node2D)GetParent().GetParent();
        _gameNode= (Node2D)GetTree().GetRoot().GetNode("Game"); 
        _animatedSprite2D = new AnimatedSprite2D();
        _animatedSprite2D.AnimationFinished += OnAnimationComplete;
    }
    
    public override void PhysicsUpdate(double delta) {
        if (Time.GetTicksMsec() - _lastAttack > AttackCooldown) {
            StartAttack();            
        }
    }

    private void StartAttack() {
        AddChild(_animatedSprite2D);
        _animatedSprite2D.SetSpriteFrames(AttackAnimation);
        _animatedSprite2D.Play();
        _lastAttack = Time.GetTicksMsec();
    }

    private void InstantiateLaser(Marker2D spawnPoint) {
        // Get Parent forward 
        Vector2 forwardDirection = new Vector2(1, 0).Rotated(_parent.Rotation);
       // Create Laser
       Cannon cannon = (Cannon)CannonProjectile.Instantiate();
       cannon.Position = spawnPoint.GlobalPosition;
       cannon.Rotation = _parent.Rotation;
       cannon.SetDirection(forwardDirection);
       _gameNode.AddChild(cannon);
    }
    
    private void LaunchProjectile() {
        InstantiateLaser(Cannon1Spawn);
        InstantiateLaser(Cannon2Spawn);
    }

    private void OnAnimationComplete() {
        LaunchProjectile();
        _animatedSprite2D.QueueFree();
        // Go back to idle state
        EmitSignal(State.SignalName.Transition, this, IdleState);
    }
}