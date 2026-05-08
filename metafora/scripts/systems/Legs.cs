using Godot;
using System;

public partial class Legs : Node2D
{
	[Export] GetLegs LinkedLegAnimation;

	private Area2D Area2D;

	private bool Collected = false;

	public override void _Ready()
    {
		Area2D = GetNode<Area2D>("Area2D");
    }

	private void BodyEntered(Node2D body)
	{
		if (body.Name == "Unit555" && Collected == false)
		{
			LinkedLegAnimation.Visible = true;
			LinkedLegAnimation.StartGetLegs();
			Collected = true;
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Visible = false;
		}
	} 


}
