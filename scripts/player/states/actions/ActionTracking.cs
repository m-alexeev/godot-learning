using Godot;
using Godot.Collections;
using spacewar.scripts.enemies;

namespace spacewar.scripts.player.states.actions;

public partial class ActionTracking: State {
   [Export] public State ActionRocket;
   [Export] public float LockOnAngle;
   [Export] public Area2D TrackingZone;

   [Export] public SpriteFrames LockOnSprite; 
   
   private Node2D _parent;
   private Area2D _trackingArea;
   private Array<Enemy> _enemies = new(); 

   public override void Enter() {
      GD.Print("Player Tracking");
      _trackingArea = GetNode<Area2D>("LockZone");
      _trackingArea.AreaEntered += OnAreaEntered;
      _trackingArea.AreaExited += OnAreaExited;
   }


   private void OnAreaEntered(Area2D otherArea) {
      if (otherArea.GetParent().Name == "Enemy") {
         GD.Print("Enemy entered");
      }
   }

   private void OnAreaExited(Area2D otherArea) {
      if (otherArea.GetParent().Name == "Enemy") {
         GD.Print("Enemy left");
      }
   }
}