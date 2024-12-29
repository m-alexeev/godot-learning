using Godot;
using System;

namespace  spacewar.scripts.player;


public partial class ShipMovement : Node2D {
	[Export] public int EnginePower{ get; set; } = 5000;
	[Export] public int BoostEnginePower { get; set; } = 6000;
	[Export] public int Mass { get; set; } = 10;
	[Export] public int Speed { get; set; } = 500;
	[Export] public int BoostSpeed { get; set; } = 800;
	[Export] public int VelocityDampening { get; set; } = 20;
	[Export] public float RotateSpeed { get; set; } = 0.2f; 

	private Node2D _parent;

	private int _currentMaxSpeed;
	// Thrust 
	private Vector2 _position;
	private double _velocity ;
	private float _acceleration;
	private float _thrust;
	public bool ApplyingThrust => Mathf.Abs(_thrust) > 0;
	public bool IsBoosting => _currentMaxSpeed == BoostSpeed;
	
	
	// Rotation
	private Vector2 _rotation;
		
	public override void _Ready() {
		_parent = (Node2D)(GetParent());
		_acceleration = (float) EnginePower / Mass;
		_position = ((Node2D)GetParent()).Position;
		_currentMaxSpeed = Speed;
	}

	public override void _Process(double delta) {
		// Calculate new velocity based on acceleration
		// Only accelerate if the current velocity is less than the max
		if (Mathf.Abs(_velocity) < _currentMaxSpeed) {
			_velocity += _acceleration * _thrust * delta;
		}

		if (!ApplyingThrust) {
			// Apply deceleration to velocity
			double prevV = _velocity;
			_velocity -= (Mathf.Sign(_velocity) * VelocityDampening * delta);
			// Clamp velocity to 0 if there is a change of direction
			if (Mathf.Sign(prevV) != Mathf.Sign(_velocity)) {
				_velocity = 0;
			}
		}
		
		Vector2 movementDirection = new Vector2(Mathf.Cos(_parent.Rotation), Mathf.Sin(_parent.Rotation)).Normalized();
		_position += movementDirection * (float)_velocity * (float)delta;
		
		_parent.Position = _position;
	
		float currentRotation = _parent.Rotation;
		float targetRotation = Mathf.Atan2(_rotation.Y, _rotation.X);

		// Interpolate the rotation angle
		_parent.Rotation = (float)Mathf.LerpAngle(currentRotation, targetRotation, RotateSpeed* delta);
	}

	// Refactor, this should be only called once not once per frame 
	// update of speed should happen in Process
	public void ApplyThrust(float direction) {
		if (!IsBoosting) {
			_thrust = direction;
		}
	}

	public void RotateTowards(Vector2 mousePosition) {
		_rotation = (mousePosition ).Normalized();
	}

	public void ApplyBoost(bool isBoosting) {
		_currentMaxSpeed = isBoosting ? BoostSpeed : Speed;
		_acceleration = isBoosting ? (float)BoostEnginePower / Mass : (float)EnginePower / Mass;
		_thrust = isBoosting ? 1 : 0;
	}
	

}
