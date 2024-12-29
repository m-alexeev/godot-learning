using Godot;

namespace spacewar.scripts.player.states;

public partial class AttackStateMachine : StateMachine {
    [Signal]
    public delegate void AttackEventHandler();
    
    protected override void OnChildTransitioned(State state, State nextState) {
        if (state != _currentState) {
            return;
        }
        State newState = _states[nextState.Name.ToString().ToLower()];
        if (newState == null) {
            return;
        }
        if (_currentState != null) {
            _currentState.Exit();
        }
        newState.Enter();
        _currentState = newState;
    }
}