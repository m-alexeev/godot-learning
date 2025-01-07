using System.Collections;
using System.Collections.Generic;
using spacewar.scripts.player.enums;
using Godot;
using Godot.Collections;
using spacewar.scripts.player.states;

namespace spacewar.scripts.player;

public partial class WeaponsComponent : Node2D {

    [Export] public Weapon InitialWeapon;
    private Weapon _currentWeapon;

    [Signal]
    public delegate void WeaponSwitchedEventHandler(Weapon selectedWeapon);

    [Signal]
    public delegate void WeaponFiredEventHandler();
    
    public override void _Ready() {
        _currentWeapon = InitialWeapon;
    }

    private void NextWeapon() {
        if (_currentWeapon == Weapon.LASER) {
            _currentWeapon = Weapon.CANNON;
        }
        else if (_currentWeapon == Weapon.CANNON) {
            _currentWeapon = Weapon.ROCKET;
        }
        else if (_currentWeapon == Weapon.ROCKET) {
            _currentWeapon = Weapon.LASER;
        }
        EmitSignal(SignalName.WeaponSwitched, WeaponUtils.ToVariant(_currentWeapon));
    }
    
    public void OnSwap(bool swap) {
        if (swap) {
            NextWeapon();
        } 
    }

    public void ShootWeapon() {
        EmitSignal(SignalName.WeaponFired);
    }
}