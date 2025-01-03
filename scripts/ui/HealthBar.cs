using Godot;
using spacewar.scripts.components;

namespace spacewar.scripts.ui;

public partial class HealthBar : ProgressBar{
    [Export] public HealthComponent HealthComponent;
    [Export] public Vector2 Offset;
    [Export] public float RotationOffset;

    private StyleBoxFlat _fgStyleBox = new();
    private Node2D _parent;
    
    public override void _Ready() {
        _parent = GetParent<Node2D>();
        // Unhook from parent rotation  
        SetAsTopLevel(true);
        SetHealthBarPercentage(HealthComponent.CurrentHealth, HealthComponent.MaxHealth);
        SetHealthBarColor(HealthComponent.CurrentHealth / HealthComponent.MaxHealth);
        HealthComponent.OnHealthUpdate += HealthComponentOnOnHealthUpdate;
    
        // Add color 
        AddThemeStyleboxOverride("fill",_fgStyleBox);
    }

    public override void _Process(double delta) {
        float targetVerticalOffset = Offset.Y - Mathf.Abs(Mathf.Sin(_parent.Rotation) * RotationOffset);
        Position = new Vector2(_parent.Position.X + Offset.X, _parent.Position.Y + targetVerticalOffset) ;
    }

    private void HealthComponentOnOnHealthUpdate(float newHealth) {
        SetHealthBarPercentage(newHealth, HealthComponent.MaxHealth);
        SetHealthBarColor(newHealth / HealthComponent.MaxHealth);
    }

    private void SetHealthBarColor(float currentPercentage) {
        float r = Mathf.Abs(currentPercentage - 1);
        float g = currentPercentage;
        _fgStyleBox.SetBgColor(new Color(r, g, 0));
    }
        

    private void SetHealthBarPercentage(float currentHealth, float maxHealth) {
        SetValue((currentHealth / maxHealth) * 100);
    }
}