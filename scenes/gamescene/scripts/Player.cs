using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
	[Export]
	private AnimatedSprite2D _animatedSprite2D;

	[Export]
	private Area2D _hitbox;
	[Export]
	private CollisionShape2D _hitboxShape;
	[Export]
	private ProgressBar _healthBar;
	[Export]
	private ProgressBar _xpBar;
	[Export]
	private Timer _hitboxTimer;

	private Direction _direction = Direction.Down;

	[Export]
	private float _speed = 30;

	[Export]
	private float _health = 100;

	[Export]
	private float _xp = 0;

	[Export]
	private float _level = 0;

	private float _healthMax;

	private CharacterBody2D _nearestEnemy;
	private float _nearestEnemyDistance = float.MaxValue;
	public float NearestEnemyDistance => _nearestEnemyDistance;
	public CharacterBody2D NearestEnemy => _nearestEnemy;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_healthMax = _health;
		_hitbox.AreaEntered += OnHitboxAreaEntered;
		_hitboxTimer.Timeout += OnHitboxTimerTimeout;
	}

    private void OnHitboxTimerTimeout()
    {
        _hitboxShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        _hitboxShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
    }


    private void OnHitboxAreaEntered(Area2D area)
    {
		if(area.GetParent() is Enemy enemy)
		{
			TakeDamage(enemy.Damage);
		}
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		bool isMoving = direction != Vector2.Zero;

		direction = direction.Normalized();

		var animation = "idle";
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
		if(_nearestEnemy != null && IsInstanceValid(_nearestEnemy))
		{
			_nearestEnemyDistance = Position.DistanceTo(_nearestEnemy.Position);
		}
		else
		{
			_nearestEnemyDistance = float.MaxValue;
		}
	}

	public void SetNearestEnemy(CharacterBody2D enemy, float distance)
	{
		_nearestEnemy = enemy;
		_nearestEnemyDistance = distance;
	}

	public void TakeDamage(float damage)
	{
		_health -= damage;
		_healthBar.Value = _health / _healthMax * 100f;
		if(_health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		GetTree().ChangeSceneToFile("res://scenes/mainmenu/mainmenu.tscn");
	}
}
