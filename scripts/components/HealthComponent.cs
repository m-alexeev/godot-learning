using Godot;

namespace spacewar.scripts.components;

public partial class HealthComponent : Node {
    [Export] public float InitialHealth;
    [Export] public float MaxHealth;
    private float _currentHealth;

    [Signal]
    public delegate void OnHealthUpdateEventHandler(float newHealth);

    [Signal]
    public delegate void DeathEventHandler();

    public override void _Ready() {
        _currentHealth = InitialHealth;
    }

    public void TakeDamage(float damageAmount) {
        _currentHealth -= damageAmount;
        if (_currentHealth < 0) {
            _currentHealth = 0;
        }
        EmitSignal(SignalName.OnHealthUpdate,_currentHealth);
        if (Mathf.Floor(_currentHealth) <= 0) {
            EmitSignal(SignalName.Death);
        }
    }

    public void IncreaseHealth(float increaseAmount) {
        _currentHealth += increaseAmount;
        if (_currentHealth > MaxHealth) {
            _currentHealth = MaxHealth;
        }
        EmitSignal(SignalName.OnHealthUpdate, _currentHealth);
    }

    public float CurrentHealth => _currentHealth;
}