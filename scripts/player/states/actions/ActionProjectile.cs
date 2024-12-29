using Godot;
using Godot.Collections;
using spacewar.scripts.projectiles;

namespace spacewar.scripts.player.states.actions;

public partial class ActionProjectile: State {

    [Export] public State IdleState;
    [Export] public SpriteFrames AttackAnimation;
    
    [Export] public ulong AttackCooldown;
    [Export] public PackedScene Projectile;
    [Export] public Array<Marker2D> ProjectileSpawns;
    
    private AnimatedSprite2D _animatedSprite2D;
    private Node2D _parent;
    private Node2D _gameNode;
    private ulong _lastAttack;
    
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

    private void InstantiateProjectile(Marker2D spawnPoint) {
       BaseProjectile projectile = (BaseProjectile)Projectile.Instantiate();
       projectile.Position = spawnPoint.GlobalPosition;
       projectile.Rotation = _parent.Rotation;
       _gameNode.AddChild(projectile);
    }
    
    private void LaunchProjectile() {
        foreach (var spawn in ProjectileSpawns) {
            InstantiateProjectile(spawn);
        }
    }

    private void OnAnimationComplete() {
        LaunchProjectile();
        _animatedSprite2D.QueueFree();
        // Go back to idle state
        EmitSignal(State.SignalName.Transition, this, IdleState);
    }
}