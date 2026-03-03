using Godot;
using System;
using System.Collections.Generic;

public partial class Projectile : Node2D
{
    [Export]
    public int Damage { get; set; }

    [Export]
    public Area2D Hitbox { get; set; }

    [Export]
    public bool DestroyOnHit { get; set; }

    public override void _Ready()
    {
        Hitbox.AreaEntered += OnHitboxAreaEntered;
    }

    private void OnHitboxAreaEntered(Area2D area)
    {
        if(area.GetParent() is Enemy enemy)
		{
			enemy.TakeDamage(Damage);
			if (DestroyOnHit)
			{
				QueueFree();
			}
		}
    }
}
