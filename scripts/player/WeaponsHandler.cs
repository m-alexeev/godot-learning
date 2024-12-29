using durak.scripts.player.enums;
using Godot;

namespace durak.scripts.player;

public partial class WeaponsHandler : Node2D {

    [Export] public Weapon InitialWeapon;
    private bool _shooting;
    private Weapon _currentWeapon;

    public bool IsShooting => _shooting;
    public Weapon CurrentWeapon => _currentWeapon;
    
    public override void _Ready() {
        _currentWeapon = InitialWeapon;
    }

    private void NextWeapon() {
        if (_currentWeapon == Weapon.LASER) {
            _currentWeapon = Weapon.CANNON;
        }
        else if (_currentWeapon == Weapon.CANNON) {
            _currentWeapon = Weapon.LASER;
        }
    }
    
    public void OnSwap(bool swap) {
        if (swap) {
            NextWeapon();
        } 
    }

    public void OnShoot(bool shoot) {
        _shooting = shoot;
    }
}