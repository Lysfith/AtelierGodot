using Godot;
using System;
using System.Collections;

public partial class Enemy : CharacterBody2D
{
	[Export]
	private Player _target;
	[Export]
	private AnimatedSprite2D _animatedSprite2D;
	[Export]
	private Area2D _hitbox;
	[Export]
	private ProgressBar _healthBar;

	[Export]
	private EnemyResource _type;

	[Export]
	private float _health = 100;
	private float _healthMax;

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

	public override void _PhysicsProcess(double delta)
	{
		 CheckSeparation();
	}

	public void SetTarget(Player target)
    {
    	_target = target;   
    }

    public void SetResource(EnemyResource type)
    {
        _type = type;
        _speed = type.Speed;
		_health = type.Health;
		_healthMax = _health;
		_animatedSprite2D.SpriteFrames = ResourceLoader.Load<SpriteFrames>(type.AnimationPath);
    }

	public void TakeDamage(float damage)
	{
		GD.Print($"Enemy took {damage} damage, remaining health: {_health - damage}");
		_health -= damage;
		_healthBar.Value = _health / _healthMax * 100f;
		if(_health <= 0)
		{
			//Give xp to player
			QueueFree();
		}
	}

	private void CheckSeparation()
	{
		if(_target == null) return;

		var distance = Position.DistanceTo(_target.Position);
		if(distance > 500)
		{
			QueueFree();
		}

		if(distance < _target.NearestEnemyDistance)
		{
			_target.SetNearestEnemy(this, distance);
		}
	}

}
