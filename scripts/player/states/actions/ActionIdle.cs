using spacewar.scripts.player.enums;
using Godot;

namespace spacewar.scripts.player.states.actions;

public partial class ActionIdle : State {
    [Export] public State ActionLaser;
    [Export] public State ActionCannon;
    [Export] public State ActionTracking;

    private WeaponsHandler _weaponsHandler;
    
    public override void Enter() {
        Node parent = GetParent().GetParent();
        _weaponsHandler = parent.GetNode<WeaponsHandler>("WeaponsHandler");
    }
    
    
    public override void Update(double delta) {
        // Change to different action states
        if (_weaponsHandler.IsShooting) {
            if (_weaponsHandler.CurrentWeapon == Weapon.LASER) {
                EmitSignal(State.SignalName.Transition, this, ActionLaser);
            } 
            if (_weaponsHandler.CurrentWeapon == Weapon.CANNON) {
                EmitSignal(State.SignalName.Transition, this, ActionCannon);
            }
        } 
        if (_weaponsHandler.CurrentWeapon == Weapon.ROCKET) {
            EmitSignal(State.SignalName.Transition, this, ActionTracking);
        }
    }
}