using Godot;
using System;

public partial class GetLegs : CanvasLayer
{
	
	private AnimationPlayer player;
	private AnimatedSprite2D sprite;

	[Export] PackedScene selectedScene {get; set;}

	public override void _Ready()
	{
		player = GetNode<AnimationPlayer>("AnimationPlayer");
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public void StartGetLegs()
	{
		player.Play("GetThemLegs");
		sprite.Play("GetLegs");
	}

	public void SpriteFinished()
	{
		if (selectedScene != null)
		{
			GetTree().CurrentScene.GetNode<SceneFadeTransition>("SceneFadeTransition").PlayTransitionAndChangeScene(selectedScene.ResourcePath);	
		}
	}


}
