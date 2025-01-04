using Godot;
using Godot.Collections;
using spacewar.scripts.enemies;

namespace spacewar.scripts.player.states.actions;

public partial class ActionTracking: State {
   [Export] public State ActionRocket;
   [Export] public float LockOnAngle;
   [Export] public Area2D TrackingZone;

   [Export] public Texture2D LockOnImage;


   private Sprite2D _lockOnSprite = new();
   private Node2D _parent;
   private Area2D _trackingArea;
   private Array<Enemy> _enemies = new();
   private Enemy _trackedEnemy;

   public override void Enter() {
      _parent = GetParent<Node2D>();
      _trackingArea = GetNode<Area2D>("LockZone");
      _trackingArea.AreaEntered += OnAreaEntered;
      _trackingArea.AreaExited += OnAreaExited;
      _lockOnSprite.SetTexture(LockOnImage);
      _lockOnSprite.SetName("LockOn");
   }

   public override void PhysicsUpdate(double delta) {
      float minAngle = LockOnAngle;
      // Get the closest enemy to player 
      foreach (var enemy in _enemies) {
         float angleToEnemy = _parent.GetAngleTo(enemy.Position);
         if (Mathf.Abs(angleToEnemy) < Mathf.DegToRad(LockOnAngle)) {
            if (angleToEnemy < minAngle) {
               _trackedEnemy = enemy;
               minAngle = angleToEnemy;
            }
         }
      }
      
      // Draw target on that enemy
      if (_trackedEnemy != null && !_trackedEnemy.HasNode("LockOn")) {
         _trackedEnemy.AddChild(_lockOnSprite);
      }
   }

   private void OnAreaEntered(Area2D otherArea) {
      if (otherArea.GetParent().Name.Equals("Enemy")) {
         _enemies.Add(otherArea.GetParent<Enemy>());
      }
   }

   private void OnAreaExited(Area2D otherArea) {
      if (otherArea.GetParent().Name.Equals( "Enemy")) {
         _enemies.Remove(otherArea.GetParent<Enemy>());
      }
   }

}