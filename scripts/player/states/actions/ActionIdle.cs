using durak.scripts.player.enums;
using Godot;

namespace durak.scripts.player.states.actions;

public partial class ActionIdle : State {
    [Export] public State ActionLaser;
    [Export] public State ActionCannon;

    private WeaponsHandler _weaponsHandler;
    
    public override void Enter() {
        Node parent = GetParent().GetParent();
        _weaponsHandler = parent.GetNode<WeaponsHandler>("WeaponsHandler");
    }
    
    
    public override void PhysicsUpdate(double delta) {
        // Change to different action states
        if (_weaponsHandler.IsShooting) {
            if (_weaponsHandler.CurrentWeapon == Weapon.LASER) {
                EmitSignal(State.SignalName.Transition, this, ActionLaser);
            } 
            if (_weaponsHandler.CurrentWeapon == Weapon.CANNON) {
                EmitSignal(State.SignalName.Transition, this, ActionCannon);
            } 
        } 
    }
}