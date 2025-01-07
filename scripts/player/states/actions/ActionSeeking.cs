using Godot;
using spacewar.scripts.components;
using spacewar.scripts.enemies;
using spacewar.scripts.player.enums;

namespace spacewar.scripts.player.states.actions;

public partial class ActionSeeking: State {
   [Export] public State ActionIdle;
   [Export] public TrackingComponent TrackingComponent;
   [Export] public WeaponsComponent WeaponsComponent;
   [Export] public State ActionLockOn;

   public override void Enter() {
      TrackingComponent.TrackingTarget += TrackingComponentOnTrackingTarget; 
      WeaponsComponent.WeaponSwitched += WeaponsComponentOnWeaponSwitched;
      
   }

   private void WeaponsComponentOnWeaponSwitched(Weapon selectedweapon) {
      EmitSignal(State.SignalName.Transition, this, ActionIdle);
   }


   private void TrackingComponentOnTrackingTarget(Node2D target) {
      if (target != null) {
         EmitSignal(State.SignalName.Transition, this, ActionLockOn);
      } 
   }
}