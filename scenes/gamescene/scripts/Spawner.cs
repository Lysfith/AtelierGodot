using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node2D
{
	[Export]
	private CharacterBody2D _player;
	[Export]
	private PackedScene _enemy;
	[Export]
	private Label _minutes;
	[Export]
	private Label _seconds;
	[Export]
	private Timer _timer;
	[Export]
	private Timer _pattern;
	[Export]
	private Timer _elite;

	[Export]
	private EnemyResource[] _enemyResources;

	private int _time;
	private int _enemyResourceIndex = 0;

	private float _spanwDistance = 400;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        _timer.Timeout += OnTimerTimeout;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void UpdateTimerLabel()
    {
        _minutes.Text = ((int)(_time / 60)).ToString();
        _seconds.Text = (_time % 60).ToString("00");
    }

	private void SpawnEnemy(Vector2 position)
    {
        var enemy = _enemy.Instantiate() as Enemy;
		var enemyResource = _enemyResources[_enemyResourceIndex];
		enemy.SetTarget(_player);
		enemy.SetResource(enemyResource);
		enemy.Position = position;

		GetTree().CurrentScene.AddChild(enemy);
    }

	private void OnTimerTimeout()
    {
        _time += 1;
		if(_time % 10 == 0)
		{
			_enemyResourceIndex = (_enemyResourceIndex + 1) % _enemyResources.Length;
		}
		UpdateTimerLabel(); 
		SpawnMultipleEnemies(_time % 10);
    }

	private Vector2 GetRandomPosition()
    {
        return _player.Position + _spanwDistance * Vector2.Right.Rotated((float)Random.Shared.NextDouble() * Mathf.Pi*2);
    }

	private void SpawnMultipleEnemies(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
			var position = GetRandomPosition();
            SpawnEnemy(position);
        }
    }
}
