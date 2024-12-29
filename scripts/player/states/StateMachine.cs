using System;
using Godot;
using Godot.Collections;

namespace durak.scripts.player.states;

public partial class StateMachine : Node2D {
    [Export] public State InitialState;

    private Dictionary<String, State> _states = new(); 
    private State _currentState;

    public override void _Ready() {
        foreach (Node child in GetChildren()) {
            if (child is State state) {
                _states[state.Name.ToString().ToLower()] = state;
                state.Transition += OnChildTransitioned;
            }
        }
        if (InitialState != null) {
            InitialState.Enter();
            _currentState = InitialState;
        }
    }

    public override void _Process(double delta) {
        if (_currentState != null) {
            _currentState.Update(delta);
        } 
    }

    public override void _PhysicsProcess(double delta) {
        if (_currentState != null) {
            _currentState.PhysicsUpdate(delta);
        }
    }


    private void OnChildTransitioned(State state, State nextState) {
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