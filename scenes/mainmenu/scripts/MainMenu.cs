using Godot;
using System;

public partial class MainMenu : Node
{
	[Export]
	private Button _playButton;
	[Export]
	private Button _quitButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//_playButton = GetNode<Button>("VBoxContainer/PlayButton");

		_playButton.Pressed += OnPlayButtonPressed;
		_quitButton.Pressed += OnQuitButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnPlayButtonPressed()
	{
		GD.Print("Bouton Jouer pressé - Chargement de l'écran de connexion");
		GetTree().ChangeSceneToFile("res://scenes/gamescene/game_scene.tscn");
	}

	private void OnQuitButtonPressed()
	{
		GD.Print("Bouton Quitter pressé");
		GetTree().Quit();
	}
}
