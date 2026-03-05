using Godot;
using System;
using System.Threading.Tasks;

public partial class Damage : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ShowDamage();
	}


	public void ShowDamage()
	{
		var tween = GetTree().CreateTween().BindNode(this);
		tween.TweenProperty(this, "position", Position + new Vector2(0, -20), 0.2f);
		tween.TweenProperty(this, "scale", new Vector2(2, 2), 0.1f);
		tween.Chain().TweenProperty(this, "scale", new Vector2(1, 1), 0.1f);
		tween.TweenCallback(Callable.From(QueueFree));

	}
}
