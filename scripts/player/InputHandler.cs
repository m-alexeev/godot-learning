using Godot;
using System;

namespace spacewar.scripts.player;
public partial class InputHandler : Node2D {
	[Signal]
	public delegate void ThrustEventHandler(float input);
	[Signal]
	public delegate void BoostEventHandler(bool input);
	[Signal]
	public delegate void ShootEventHandler();
	[Signal]
	public delegate void SwapEventHandler(bool input);
	[Signal]
	public delegate void MouseRotateEventHandler(Vector2 input);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventKey) {
			HandleActionInput(@event);
		}
		if (@event is InputEventMouseMotion eventMouseMotion) {
			Vector2 screenDimensions = GetViewport().GetVisibleRect().Size;
			Vector2 screenCenter = screenDimensions / 2;
			EmitSignal(SignalName.MouseRotate, eventMouseMotion.Position - screenCenter);
		}
	}

	private void HandleActionInput(InputEvent @event) {
		// Movement 
		if (@event.IsActionPressed("boost")) {
			EmitSignal(SignalName.Boost, true);
		}
		if (@event.IsActionReleased("boost")) {
			EmitSignal(SignalName.Boost, false);
		}
		if (@event.IsActionPressed("up") || @event.IsActionPressed("down")) {
			float value = @event.GetActionStrength("up") - @event.GetActionStrength("down");
			EmitSignal(SignalName.Thrust, value);
		}
		if (@event.IsActionReleased("up") || @event.IsActionReleased("down")) {
			EmitSignal(SignalName.Thrust, 0);
		}
		
		// Actions
		if (@event.IsActionPressed("shoot")) {
			EmitSignal(SignalName.Shoot);
		}
		if (@event.IsActionReleased("shoot")) {
			EmitSignal(SignalName.Shoot, false);
		}

		if (@event.IsAction("swap")) {
			EmitSignal(SignalName.Swap, @event.IsActionPressed("swap"));
		}
	}
}
