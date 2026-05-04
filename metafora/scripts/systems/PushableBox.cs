using Godot;
using System;

public partial class PushableBox : RigidBody2D
{
	private AnimatedSprite2D aniBox;

	[Export(PropertyHint.Enum, "red, blue, green")] private string ColorSelect;

    public override void _Ready()
    {
        aniBox = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		aniBox.Play(ColorSelect);
    }




}
