using Godot;
using spacewar.scripts.components;

namespace spacewar.scripts.enemies;

public partial class Enemy : Node2D {
    private HealthComponent _healthComponent;
    [Export]
    private HurtboxComponent _hurtboxComponent;
    [Export]
    private AnimatedSprite2D _animatedSprite2d;
    

    public override void _Ready() {
        _healthComponent = GetNode<HealthComponent>("HealthComponent");
        _healthComponent.Death += OnDie;
        _hurtboxComponent.OnDamage += HurtboxComponentOnOnDamage;
    }

    private void HurtboxComponentOnOnDamage(float damage) {
        GD.Print("taking damage");
        _animatedSprite2d.Play("damage");
    }

    private void OnDie() {
        GD.Print("Dying");
        QueueFree();
    } 
}