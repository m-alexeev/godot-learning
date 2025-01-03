using Godot;
using Godot.Collections;

namespace spacewar.scripts.player.states.actions;

public partial class ActionRocket: State {
   [Export] public State IdleState;
   [Export] public State TrackingState;
   [Export] public SpriteFrames LaunchAnimation;

   [Export] public ulong LaunchCooldown;
   [Export] public PackedScene RocketProjectile;
   [Export] public Array<Marker2D> RocketSpawns;

   private AnimatedSprite2D _animatedSprite2D;
   private Node2D _parent;
   private Node2D _gameNode;
   private ulong _lastAttackTime;

   public override void Enter() {
      _parent = (Node2D)GetParent().GetParent();
      _gameNode = (Node2D)GetTree().GetRoot().GetNode("Game");
      _animatedSprite2D = new AnimatedSprite2D();
      _animatedSprite2D.AnimationFinished += AnimatedSprite2DOnAnimationFinished;
   }

   public override void PhysicsUpdate(double delta) {
     if (Time.GetTicksMsec() - _lastAttackTime > LaunchCooldown) {
         StartAttack();            
     }
   }

   public override void Update(double delta) {
   }

   private void StartAttack() {
      AddChild(_animatedSprite2D);
      _animatedSprite2D.SetSpriteFrames(LaunchAnimation);
      _animatedSprite2D.Play();
      _lastAttackTime = Time.GetTicksMsec();
   }

   private void LaunchProjectile() {
      foreach (var spawn in RocketSpawns) {
         InstantiateProjectile(spawn);    
      } 
   }

   private void InstantiateProjectile(Marker2D spawnPoint) {
      
   }
   
   private void AnimatedSprite2DOnAnimationFinished() {
      LaunchProjectile();
      _animatedSprite2D.QueueFree();

      EmitSignal(State.SignalName.Transition, this, TrackingState);
   }
   
   
}