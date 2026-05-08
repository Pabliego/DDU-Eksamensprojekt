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

	private AudioStreamPlayer2D ButtonSound;

	[Export(PropertyHint.Enum, "red,blue,default,green")] private string ColorSelect = "default";

	public override void _Ready()
    {
		GetNode<AnimatedSprite2D>("default").Visible = false;
		GetNode<AnimatedSprite2D>("blue").Visible = false;
		GetNode<AnimatedSprite2D>("green").Visible = false;
		GetNode<AnimatedSprite2D>("red").Visible = false;


        aniButton = GetNode<AnimatedSprite2D>(ColorSelect);
		aniButton.Visible = true;
		aniButton.Play("idle");
		DetectBox = GetNode<Area2D>("Area2D");
		ButtonSound = GetNode<AudioStreamPlayer2D>("ButtonSound");
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
		if (body.Name == "Unit555" || body.IsInGroup("pushable"))
		{
			aniButton.Play("pressed");
			ButtonSound.Play();
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
		if (body.Name == "Unit555" || body.IsInGroup("pushable"))
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
