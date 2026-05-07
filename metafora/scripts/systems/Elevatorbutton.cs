using Godot;
using System;

public partial class Elevatorbutton : Node2D
{
	[Export] public Elevator linkedElevator;

	[Export] public bool HasToBeHeld = false;

	[Export] public bool ActivateOnce = false;

	private AnimatedSprite2D aniButton;
	private Area2D DetectBox;
	public bool Activated = false;

	private AudioStreamPlayer2D ButtonSound;

	public override void _Ready()
    {
        aniButton = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		DetectBox = GetNode<Area2D>("Area2D");
		ButtonSound = GetNode<AudioStreamPlayer2D>("ButtonSound");
    }
	
	private void ActivateLink()
	{
		if (Activated != true)
		{
			Activated = true;

			if (linkedElevator != null)
			{
				linkedElevator.UpdateElevatorState(this, true);
			}

		} else
		{
			if (ActivateOnce != true)
			{
				Activated = false;	
				if(linkedElevator != null)
				{
					linkedElevator.UpdateElevatorState(this, false);
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
			if (linkedElevator != null)
			{
				ActivateLink();
			} else {
				GD.Print("No Door Node linked!");
				return;
			}

		if (HasToBeHeld)
        {
            Activated = true;
            linkedElevator.Activate();
        }
        else
        {
            ActivateLink();
        }
		}
	} 

	private void BodyExited(Node2D body)
	{
		if (body.Name == "Unit555" || body.IsInGroup("pushable"))
		{
			if (HasToBeHeld && linkedElevator != null)
			{
				Activated = false;
				linkedElevator.Deactivate();
				aniButton.Play("idle");
			}
			else
			{
				
				string playani;

				if (Activated == true)
				{
					playani = "engaged";
				}
				else
				{
					playani = "idle";
				}
				
				aniButton.Play(playani);
			}
		}
	} 
}
