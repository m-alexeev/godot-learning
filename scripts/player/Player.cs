using Godot;

namespace durak.scripts.player;

public partial class Player : Node2D {

	[Export]
	private ShipMovement _shipMovement;
	[Export]
	private InputHandler _inputHandler;

	[Export] private WeaponsHandler _weaponsHandler;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// Connect signals
		_inputHandler.Thrust+= _shipMovement.ApplyThrust;
		_inputHandler.Boost += _shipMovement.ApplyBoost;
		_inputHandler.MouseRotate += _shipMovement.RotateTowards;
		_inputHandler.Shoot += _weaponsHandler.OnShoot;
		_inputHandler.Swap += _weaponsHandler.OnSwap;
	}
}