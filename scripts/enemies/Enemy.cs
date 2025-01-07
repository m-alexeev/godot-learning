using Godot;
using spacewar.scripts.components;

namespace spacewar.scripts.enemies;

public partial class Enemy : Node2D {
    private HealthComponent _healthComponent;
    

    public override void _Ready() {
        _healthComponent = GetNode<HealthComponent>("HealthComponent");
        _healthComponent.Death += OnDie;
    }


    private void OnDie() {
        QueueFree();
    } 
}