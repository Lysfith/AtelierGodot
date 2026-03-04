using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class SingleShot : Weapon
{
    public void Shoot(CharacterBody2D owner, CharacterBody2D target, SceneTree tree)
    {
        if(target == null || !IsInstanceValid(target))
        {
            return;
        }

        var projectile = ProjectileNode.Instantiate<Projectile>();
        projectile.Position = owner.Position;
        projectile.Direction = (target.Position - owner.Position).Normalized();
        projectile.Damage = Damage;
        projectile.Speed = Speed;
        tree.CurrentScene.AddChild(projectile);
    }

    public override void Activate(CharacterBody2D owner, CharacterBody2D target, SceneTree tree)
    {
        Shoot(owner, target, tree);
    }

    public SingleShot()
    {
        
    }
}
