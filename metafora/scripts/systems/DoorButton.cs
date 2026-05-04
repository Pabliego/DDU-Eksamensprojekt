using Godot;
using System;

public partial class DoorButton : Node2D
{
	
	[Export] public Door linkedDoor;

	[Export] public bool HasToBeHeld = false;

	private AnimatedSprite2D aniButton;
	private Area2D DetectBox;
	bool Activated = false;

	public override void _Ready()
    {
        aniButton = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		DetectBox = GetNode<Area2D>("Area2D");
    }
	
	private void ActivateLink()
	{
		if (Activated != true)
		{
			linkedDoor.Activate(true);
			Activated = true;
		} else
		{
			linkedDoor.Activate(false);
			Activated = false;
		}
		
	}

	private void BodyEntered(Node2D body)
	{
		if (body.Name == "Unit555Body" || body.IsInGroup("pushable"))
		{
			aniButton.Play("pressed");
			if (linkedDoor != null)
			{
				ActivateLink();
			} else {
				GD.Print("No Door Node linked!");

			}
		}
	} 

	private void BodyExited(Node2D body)
	{
		if (body.Name == "Unit555Body" || body.IsInGroup("pushable"))
		{
			if (Activated == true)
			{
				aniButton.Play("engaged");
			} else {
				aniButton.Play("idle");
			}
		}
		if (HasToBeHeld == true)
		{
			linkedDoor.Activate(false);
			Activated = false;
		}
	} 
}
