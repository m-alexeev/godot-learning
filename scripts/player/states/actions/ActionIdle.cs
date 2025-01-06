using System;
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
        _weaponsHandler.WeaponSwitched += WeaponsHandlerOnWeaponSwitched;
    }

    private void WeaponsHandlerOnWeaponSwitched(Weapon selectedweapon) {
        switch (selectedweapon) {
            case Weapon.LASER:
                EmitSignal(State.SignalName.Transition, this, ActionLaser);
                break;
            case Weapon.CANNON:
                EmitSignal(State.SignalName.Transition, this, ActionCannon);
                break;
            case Weapon.ROCKET:
                EmitSignal(State.SignalName.Transition, this, ActionTracking);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(selectedweapon), selectedweapon, null);
        }
    }


    public override void Update(double delta) {
        // Change to different action states
        //TODO: Refactor this
    }
}