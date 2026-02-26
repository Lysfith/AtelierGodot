using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private AnimatedSprite2D _animatedSprite2D;

	private Direction _direction = Direction.Down;

	[Export]
	private float _speed = 30;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var directionH = Input.GetAxis("move_left", "move_right");
		var directionV = Input.GetAxis("move_up", "move_down");

		bool isMoving = directionH != 0 || directionV != 0;
		if(Mathf.Abs(directionH) > Mathf.Abs(directionV))
		{
			if(directionH > 0)
			{
				_direction = Direction.Right;
			}
			else if(directionH < 0)
			{
				_direction = Direction.Left;
			}
		}
		else if(Mathf.Abs(directionH) < Mathf.Abs(directionV))
		{
			if(directionV > 0)
			{
				_direction = Direction.Down;
			}
			else if(directionV < 0)
			{
				_direction = Direction.Up;
			}
		}

		Vector2 velocity = Vector2.Zero;

		var animation = "idle";
		if(isMoving)
		{
			animation = "walk";
		}

		switch (_direction)
		{
			case Direction.Up:
				_animatedSprite2D.Play($"{animation}_up");
				velocity.Y = -1;
				break;
			case Direction.Down:
				_animatedSprite2D.Play($"{animation}_down");
				velocity.Y = 1;
				break;
			case Direction.Left:
				_animatedSprite2D.Play($"{animation}_left");
				velocity.X = -1;
				break;
			case Direction.Right:
				_animatedSprite2D.Play($"{animation}_right");
				velocity.X = 1;
				break;
		}

		if(isMoving)
		{
			Position += velocity * _speed * (float)delta;
		}
	}
}
