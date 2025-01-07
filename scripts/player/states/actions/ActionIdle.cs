using System;
using spacewar.scripts.player.enums;
using Godot;

namespace spacewar.scripts.player.states.actions;

public partial class ActionIdle : State {
    [Export] public State ActionLaser;
    [Export] public State ActionCannon;
    [Export] public State ActionTracking;

    [Export] public WeaponsComponent WeaponsComponent;
    private Weapon _selectedWeapon;
    
    public override void Enter() {
        WeaponsComponent.WeaponSwitched += WeaponsComponentOnWeaponSwitched;
        WeaponsComponent.WeaponFired += WeaponsComponentOnWeaponFired;
    }

    private void WeaponsComponentOnWeaponFired() {
        // Shoot weapons
        switch (_selectedWeapon) {
            case Weapon.LASER:
                EmitSignal(State.SignalName.Transition, this, ActionLaser);
                break;
            case Weapon.CANNON:
                EmitSignal(State.SignalName.Transition, this, ActionCannon);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void WeaponsComponentOnWeaponSwitched(Weapon selectedWeapon) {
        // Switch weapons
        switch (selectedWeapon) {
            case Weapon.LASER:
                _selectedWeapon = Weapon.LASER;
                break;
            case Weapon.CANNON:
                _selectedWeapon = Weapon.CANNON;
                break;
            case Weapon.ROCKET:
                EmitSignal(State.SignalName.Transition, this, ActionTracking);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(selectedWeapon), selectedWeapon, null);
        }
    }
}