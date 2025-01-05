using Godot;
using Godot.Collections;
using spacewar.scripts.enemies;

namespace spacewar.scripts.components;

public partial class TrackingComponent : Node2D {
    [Export] public Area2D TrackingArea;
    [Export] public float MaxTrackingAngle;
    [Export] public string NameOfTargets;
    [Signal] public delegate void TrackingTargetEventHandler(Node2D target);
    
    private Node2D _trackedTarget;
    private Node2D _tracker;
    private Array<Node2D> _targetsInRange = new() ;
    
    public Node2D TrackedTarget => _trackedTarget;

    public override void _Ready() {
        _tracker = GetOwner<Node2D>();
        TrackingArea.AreaEntered += TrackingAreaOnAreaEntered;
        TrackingArea.AreaExited += TrackingAreaOnAreaExited;
    }

    public override void _PhysicsProcess(double delta) {
        float minAngle = MaxTrackingAngle;
        Node2D prevTarget = _trackedTarget;
        Node2D newTarget = null;
        // Find closest target to tracker based on forward vector
        foreach (var target in _targetsInRange) {
            float angleToTarget = _tracker.GetAngleTo(target.Position);
            if (Mathf.Abs(angleToTarget) < Mathf.DegToRad(MaxTrackingAngle) && angleToTarget < minAngle) {
                newTarget = target;
                minAngle = angleToTarget;
            }
        }
        
        if (prevTarget != newTarget) {
            // Signal that new target is being tracked
            EmitSignal(SignalName.TrackingTarget, newTarget);
            _trackedTarget = newTarget;
        }
    }
    


    private void TrackingAreaOnAreaEntered(Area2D otherArea) {
        if (otherArea.GetOwner().Name.Equals(NameOfTargets)) {
           _targetsInRange.Add(otherArea.GetOwner<Node2D>()); 
        }
    }

    private void TrackingAreaOnAreaExited(Area2D otherArea) {
        if (otherArea.GetOwner().Name.Equals(NameOfTargets)) {
           _targetsInRange.Remove(otherArea.GetOwner<Node2D>()); 
        }
    }
}