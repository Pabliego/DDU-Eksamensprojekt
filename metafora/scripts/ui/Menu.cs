using Godot;
using System;

public partial class Menu : CanvasLayer
{

	private bool Showing = false;

	private AudioStreamPlayer2D Ambiance;

	private CanvasLayer MenuItems;


    public override void _Ready()
    {
        MenuItems = GetNode<CanvasLayer>("CanvasLayer");
		Ambiance = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
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
