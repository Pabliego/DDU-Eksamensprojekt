using Godot;
using System;

public partial class DoorButton : Node2D
{
	
	[Export] public Door linkedDoor;

	[Export] public bool HasToBeHeld = false;

	[Export] public bool ActivateOnce = false;


	private AnimatedSprite2D aniButton;
	private Area2D DetectBox;
	public bool Activated = false;

	public override void _Ready()
    {
        aniButton = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		DetectBox = GetNode<Area2D>("Area2D");
    }
	
	private void ActivateLink()
	{
		if (Activated != true)
		{
			Activated = true;

			if (linkedDoor != null)
			{
				linkedDoor.UpdateButtonState(this, true);
			}

		} else
		{
			if (ActivateOnce != true)
			{
				Activated = false;	
				if(linkedDoor != null)
				{
					linkedDoor.UpdateButtonState(this, false);
				}
			}
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
			if (HasToBeHeld ==true || ActivateOnce == false)
			{
				Activated = false;
				aniButton.Play("idle");
				if (linkedDoor != null)
				{
					linkedDoor.UpdateButtonState(this, false);
				}
			}
			else
			{
				aniButton.Play("engaged");
			}
		}
	} 
}
