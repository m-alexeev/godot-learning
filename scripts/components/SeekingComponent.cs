using Godot;
using Godot.Collections;
using spacewar.scripts.enemies;

namespace spacewar.scripts.components;

public partial class SeekingComponent : Node2D {
    [Export] public float SeekingRadius;

    private Array<Enemy> _trackedTargets;
    public Array<Enemy> TrackedTargets => _trackedTargets;


}