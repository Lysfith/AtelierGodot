using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Weapon : Resource
{
    [Export]
    public string Name { get; set; }

    [Export]
    public Texture2D Texture { get; set; }

    [Export]
    public float Damage { get; set; }

    [Export]
    public float Cooldown { get; set; }

    [Export]
    public float Speed { get; set; }

    [Export]
    public PackedScene ProjectileNode { get; set; }

    public virtual void Activate(CharacterBody2D owner, CharacterBody2D target, SceneTree tree)
    {

    }

    public Weapon()
    {

    }
}
