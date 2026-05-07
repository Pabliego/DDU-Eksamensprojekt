using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class Door : Node2D
{
	private AnimatedSprite2D aniDoor;
	private Area2D AreaDetect;

	private CollisionShape2D Collision;

	private AudioStreamPlayer2D DoorSound;

	private AudioStreamPlayer2D DoorSoundClose;

	[Export(PropertyHint.Enum, "red, blue")] private string ColorSelect = "blue";

	[Export] PackedScene selectedScene {get; set;}

	[Export] public Godot.Collections.Array<DoorButton> LinkedButtons = new();
	private bool DoorOpen = false;


		public override void _Ready()
    {
		aniDoor = GetNode<AnimatedSprite2D>(ColorSelect);
		aniDoor.Visible = true;
		
		AreaDetect = GetNode<Area2D>("Area2D");
		Collision = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");

		DoorSound = GetNode<AudioStreamPlayer2D>("DoorAudio");
		DoorSoundClose = GetNode<AudioStreamPlayer2D>("DoorAudioClose");

    }

	public void UpdateButtonState(DoorButton button, bool activated)
	{
		CheckAllButtons();
	}


	private void CheckAllButtons()
	{
		if (LinkedButtons.Count == 0)
		{
			return;
		}

		bool AllLinkedPressed = true;

		for (int i = 0; i < LinkedButtons.Count; i++)
		{
			DoorButton button = LinkedButtons[i] as DoorButton;
			if (button == null || button.Activated == false)
			{
				AllLinkedPressed = false;
				break;
			}
		}


		if (AllLinkedPressed && DoorOpen == false)
		{
			Activate(true);
			DoorOpen = true;
		}
		else if (AllLinkedPressed == false && DoorOpen == true)
		{
			bool shouldClose = false;
			for (int i = 0; i < LinkedButtons.Count; i++)
			{
				DoorButton button = LinkedButtons[i] as DoorButton;
				if (button != null && button.ActivateOnce == false && button.Activated == false)
				{
					shouldClose = true;
					break;
				}
			}

			if (shouldClose)
			{
				Activate(false);
				DoorOpen = false;
			}
		}


	}


    public override void _Process(double delta)
    {
        if (AreaDetect.Monitoring == true)
		{
			Collision.Visible = true;
		} else
		{
			Collision.Visible = false;
		}
    }


	public void Activate(bool open)
	{
		if (open)
		{
			aniDoor.Play("open");	
			AreaDetect.Monitoring = true;
			DoorSound.Play();
		}
		else
		{
			aniDoor.Play("close");
			AreaDetect.Monitoring = false;
			DoorSoundClose.Play();
		}
	}

	public async void BodyEntered(Node2D body)
	{
		if (body.Name == "Unit555")
		{
			if (selectedScene != null)
			{
				GetTree().CurrentScene.GetNode<SceneFadeTransition>("SceneFadeTransition").PlayTransitionAndChangeScene(selectedScene.ResourcePath);	
			}		
		}

	}
}
