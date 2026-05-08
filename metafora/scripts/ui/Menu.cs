using Godot;
using System;
using System.Data.Common;

public partial class Menu : CanvasLayer
{

	private bool Showing = false;

	private AudioStreamPlayer2D Ambiance;

	private CanvasLayer MenuItems;

	private AnimationPlayer helpani;

	private bool helphidden = true;


    public override void _Ready()
    {
        MenuItems = GetNode<CanvasLayer>("CanvasLayer");
		Ambiance = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		helpani = GetNode<AnimationPlayer>("IconHelp/AnimationPlayer");
		Ambiance.Play();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
		{
			if (Showing == false)
			{
				Showing = true;
				MenuItems.Visible = true;
			} else
			{
				Showing = false;
				MenuItems.Visible = false;
			}
		}

		if (Input.IsActionJustPressed("h"))
		{
			if (helphidden == true)
			{
				helpani.Play("Open");
				helphidden = false;
			} 
			else if (helphidden == false)
			{
				helpani.Play("Close");
				helphidden = true;
			}
		}


    }



	private void ButtonPressed()
	{
		GetTree().ReloadCurrentScene();
	}

		private void ButtonPressedHome()
	{
		GetTree().ChangeSceneToFile("res://scenes/ui/MainMenu.tscn");
	}
}
