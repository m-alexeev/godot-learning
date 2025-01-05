using Godot;

namespace spacewar.scripts.player.states.actions;

public partial class ActionLockOn: State {
    [Export] public State ActionTracking;
}