using Godot;
using spacewar.scripts.projectiles;

namespace spacewar.scripts.components;

public partial class HurtboxComponent : Area2D {

    [Export] public HealthComponent HealthComponent;
    
    [Signal]
    public delegate void OnDamageEventHandler(float damage);

    [Signal]
    public delegate void OnProjectileDamageEventHandler(float damage);

    public override void _Ready() {
        AreaEntered += OnAreaEntered;
    }

    private void DealDamage(DamageComponent damageComponent) {
        if (HealthComponent != null) {
            HealthComponent.TakeDamage(damageComponent.Damage * damageComponent.DamageMultiplier);    
        }
        EmitSignal(SignalName.OnDamage, damageComponent.Damage * damageComponent.DamageMultiplier);
    }

    private void OnAreaEntered(Area2D collisionArea) {
        if (collisionArea is BaseProjectile){
            // Get damage component
            DamageComponent damageComponent = collisionArea.GetNode<DamageComponent>("DamageComponent");
            if (damageComponent != null) {
                DealDamage(damageComponent);     
            }
        }

        if (collisionArea.GetNode<HitboxComponent>("HitboxComponent") != null) {
            DamageComponent damageComponent = collisionArea.GetNode<DamageComponent>("DamageComponent");
            if (damageComponent != null) {
                DealDamage(damageComponent);     
            }
        }
    }

}