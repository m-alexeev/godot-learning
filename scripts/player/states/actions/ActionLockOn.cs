using Godot;
using spacewar.scripts.components;
using spacewar.scripts.enemies;

namespace spacewar.scripts.player.states.actions;

public partial class ActionLockOn : State {
    private Sprite2D _lockOnSprite;
    private Enemy _trackedEnemy;


    private Node2D _tracker;
    [Export] public State ActionSeeking;
    [Export] public State ActionShootRocket;
    [Export] public Texture2D LockOnImage;
    [Export] public TrackingComponent TrackingComponent;

    public override void Enter() {
        GD.Print("Locking State");
        _tracker = GetOwner<Node2D>();
        if (_lockOnSprite == null) {
            _lockOnSprite = new Sprite2D();
            _lockOnSprite.SetTexture(LockOnImage);
            _lockOnSprite.SetName("LockOn");
            _lockOnSprite.SetAsTopLevel(true);
            _tracker.AddChild(_lockOnSprite);
        }
        else {
            _lockOnSprite.Show();
        }

        TrackingComponent.TrackingTarget += TrackingComponentOnTrackingTarget;
        _trackedEnemy = (Enemy)TrackingComponent.TrackedTarget;
    }

    private void TrackingComponentOnTrackingTarget(Node2D target) {
        if (target == null)
            // Go back to seeking state if no tracking target exists
            EmitSignal(State.SignalName.Transition, this, ActionSeeking);
    }

    public override void PhysicsUpdate(double delta) {
        if (_trackedEnemy != null) _lockOnSprite.Position = _trackedEnemy.Position;
    }

    public override void Exit() {
        _lockOnSprite.Hide();
    }
}