using Godot;
using Godot.Collections;
using spacewar.scripts.components;
using spacewar.scripts.enemies;

namespace spacewar.scripts.player.states.actions;

public partial class ActionTracking: State {
   [Export] public State ActionRocket;
   [Export] public Texture2D LockOnImage;
   [Export] public TrackingComponent TrackingComponent;

   private Node2D _parent;
   private Sprite2D _lockOnSprite = new();
   private Enemy _trackedEnemy;

   public override void Enter() {
      _parent = GetOwner<Node2D>();
      _lockOnSprite.SetTexture(LockOnImage);
      _lockOnSprite.SetName("LockOn");
      _lockOnSprite.SetAsTopLevel(true);
      _lockOnSprite.Hide();
      TrackingComponent.TrackingTarget += TrackingComponentOnTrackingTarget; 
      _parent.AddChild(_lockOnSprite);
   }

   private void TrackingComponentOnTrackingTarget(Node2D target) {
      _trackedEnemy = (Enemy)target;
      if (_trackedEnemy != null) {
         _lockOnSprite.Show();
      }
      else {
         _lockOnSprite.Hide();
      } 
   }


   public override void PhysicsUpdate(double delta) {
      if (_trackedEnemy != null) {
         _lockOnSprite.Position = _trackedEnemy.Position;
      }
   }

}