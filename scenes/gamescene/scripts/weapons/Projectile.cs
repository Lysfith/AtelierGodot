using Godot;
using System;
using System.Collections.Generic;

public partial class Projectile : Node2D
{

    [Export]
    private Area2D _area;
    [Export]
    private VisibleOnScreenNotifier2D _screenNotifier;

    [Export]
	public Vector2 Direction = Vector2.Right;
	[Export]
	public float Speed = 200;
	[Export]
	public float Damage = 1;
    public override void _Ready()
    {
        _area.BodyEntered += OnAreaBodyEntered;
        _screenNotifier.ScreenExited += OnScreenExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction.Normalized() * Speed * (float)delta;
    }

    private void OnScreenExited()
    {
        QueueFree();
    }


    private void OnAreaBodyEntered(Node2D body)
    {
        if(body is Enemy enemy)
        {
            enemy.TakeDamage(Damage);
            QueueFree();
        }
    }
}
