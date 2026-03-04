using Godot;
using System;

public partial class Slot : PanelContainer
{
	[Export]
	private TextureRect _textureRect;
	[Export]
	private Timer _cooldown;

	[Export]	
	private Weapon _weapon;

	public override void _Ready()
	{
		_cooldown.Timeout += OnCooldownTimeout;

		if(_weapon.Texture != null)
		{
			_textureRect.Texture =  _weapon.Texture;
		}
		_cooldown.WaitTime = _weapon.Cooldown;
	}

    private void OnCooldownTimeout()
    {
        _cooldown.WaitTime = _weapon.Cooldown;
		_weapon.Activate((Player)Owner, ((Player)Owner).NearestEnemy, GetTree());
    }

}
