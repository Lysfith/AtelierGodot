using Godot;
using System;

[GlobalClass]
public partial class EnemyResource : Resource
{
    [Export]
    public int Health { get; set; }

    [Export]
    public float Speed { get; set; }

    [Export]
    public float AttackPower { get; set; }

    [Export]
	public string AnimationPath { get; set; }

    public EnemyResource()
    {
        
    }
}
