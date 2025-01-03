using Godot;

namespace spacewar.scripts.player.states.actions;

public partial class ActionTracking: State {
   [Export] public State ActionRocket;
   [Export] public float TrackingAngle;
   [Export] public Area2D TrackingZone;
   
   
   
}