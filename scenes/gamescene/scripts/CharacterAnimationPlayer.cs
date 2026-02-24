using Godot;
using System;

public partial class CharacterAnimationPlayer : AnimationPlayer
{
	[Export]
	private AnimationPlayer _animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_animationPlayer == null)
		{
			return;
		}
		if (Input.IsActionPressed("move_right"))
		{
			_animationPlayer.Play("idle_right");
		}
		else if (Input.IsActionPressed("move_left"))
		{
			_animationPlayer.Play("idle_left");
		}
		else if (Input.IsActionPressed("move_up"))
		{
			_animationPlayer.Play("idle_up");
		}
		else if (Input.IsActionPressed("move_down"))
		{
			_animationPlayer.Play("idle_down");
		}
		else
		{
			_animationPlayer.Stop();
		}
	}
}
