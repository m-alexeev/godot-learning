using Godot;
using System;
using spacewar.scripts.player;
using spacewar.scripts.player.enums;

public partial class UIComponent : Control {
    [Export] public Player Player;

    private Label _weaponLabel;
    
    private WeaponsComponent _weaponsComponent;

    public override void _Ready() {
        _weaponsComponent = Player.GetNode<WeaponsComponent>("WeaponComponent");
        _weaponLabel = GetNode<Label>("Weapon");
        _weaponsComponent.WeaponSwitched += SetWeaponLabel;
        
        SetWeaponLabel(_weaponsComponent.InitialWeapon);
    }


    private void SetWeaponLabel(Weapon selectedweapon) {
        _weaponLabel.SetText(selectedweapon.ToString());
    }
}
