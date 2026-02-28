using Godot;
using System;

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

	private int _time;

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
		enemy.Position = position;
		enemy.SetTarget(_player);

		GetTree().CurrentScene.AddChild(enemy);
    }

	private void OnTimerTimeout()
    {
        _time += 1;
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
