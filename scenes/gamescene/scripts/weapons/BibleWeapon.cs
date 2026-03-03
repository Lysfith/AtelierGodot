using Godot;
using System;
using System.Collections.Generic;

public partial class BibleWeapon : Node2D
{
    [Export]
    public string WeaponName { get; set; }

    [Export]
    public int ProjectileCount { get; set; } = 1;

    [Export]
    public PackedScene ProjectilePrefab { get; set; }

    [Export]
    public float OrbitRadius { get; set; } = 100f;

    [Export]
    public float RotationSpeed { get; set; } = 2f;

    private List<Node2D> _projectiles = new List<Node2D>();
    private float _currentAngle = 0f;

    public override void _Ready()
    {
        for (int i = 0; i < ProjectileCount; i++)
        {
            SpawnProjectile(i);
        }
    }

    public override void _Process(double delta)
    {
        // Clean up projectiles that are no longer in the scene
        _projectiles.RemoveAll(projectile => !IsInstanceValid(projectile));

        // Rotate projectiles around the player
        _currentAngle += RotationSpeed * (float)delta;
        
        for (int i = 0; i < _projectiles.Count; i++)
        {
            if (IsInstanceValid(_projectiles[i]))
            {
                float angleOffset = (Mathf.Tau / ProjectileCount) * i;
                float angle = _currentAngle + angleOffset;
                
                Vector2 offset = new Vector2(
                    Mathf.Cos(angle) * OrbitRadius,
                    Mathf.Sin(angle) * OrbitRadius
                );
                
                _projectiles[i].GlobalPosition = GlobalPosition + offset;
            }
        }
    }

    private void SpawnProjectile(int index)
    {
        var projectileInstance = ProjectilePrefab.Instantiate() as Node2D;
        if (projectileInstance != null)
        {
            AddChild(projectileInstance);
            projectileInstance.GlobalPosition = GlobalPosition;
            _projectiles.Add(projectileInstance);
        }
    }
}
