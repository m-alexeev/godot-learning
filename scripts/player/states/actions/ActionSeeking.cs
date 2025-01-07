using Godot;
using spacewar.scripts.components;
using spacewar.scripts.enemies;

namespace spacewar.scripts.player.states.actions;

public partial class ActionSeeking: State {
   [Export] public TrackingComponent TrackingComponent;
   [Export] public State ActionLockOn;

   public override void Enter() {
      GD.Print("Seeking State");
      TrackingComponent.TrackingTarget += TrackingComponentOnTrackingTarget; 
   }


   private void TrackingComponentOnTrackingTarget(Node2D target) {
      if (target != null) {
         EmitSignal(State.SignalName.Transition, this, ActionLockOn);
      } 
   }
}