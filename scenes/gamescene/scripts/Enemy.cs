using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export]
	private CharacterBody2D _target;
	[Export]
	private AnimatedSprite2D _animatedSprite2D;
	[Export]
	private Area2D _hitbox;
	[Export]
	private ProgressBar _healthBar;

	[Export]
	private EnemyResource _type;

	private Direction _direction = Direction.Down;

	[Export]
	private float _speed = 20;

	public float Damage => _type.AttackPower;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var animation = "idle";

		if(_target == null)
        {
			_animatedSprite2D.Play($"{animation}_down");
            return;
        }

		var direction = (_target.Position - Position).Normalized();

		bool isMoving = direction != Vector2.Zero;

		direction = direction.Normalized();
		
		
		if(isMoving)
		{
			animation = "walk";
		}

		_animatedSprite2D.Play($"{animation}_down");

		if(isMoving)
		{
			Position += direction * _speed * (float)delta;
		}
	}

	public void SetTarget(CharacterBody2D target)
    {
    	_target = target;   
    }

    public void SetResource(EnemyResource type)
    {
        _type = type;
        _speed = type.Speed;
		_animatedSprite2D.SpriteFrames = ResourceLoader.Load<SpriteFrames>(type.AnimationPath);
    }

}
