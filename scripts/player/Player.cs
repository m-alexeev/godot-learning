using Godot;

namespace spacewar.scripts.player;

public partial class Player : Node2D {

	[Export]
	private ThrustMovement _thrustMovement;
	[Export]
	private InputHandler _inputHandler;

	[Export] private WeaponsHandler _weaponsHandler;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// Connect signals
		_inputHandler.Thrust+= _thrustMovement.ApplyThrust;
		_inputHandler.Boost += _thrustMovement.ApplyBoost;
		_inputHandler.MouseRotate += _thrustMovement.RotateTowards;
		_inputHandler.Shoot += _weaponsHandler.OnShoot;
		_inputHandler.Swap += _weaponsHandler.OnSwap;
	}
}