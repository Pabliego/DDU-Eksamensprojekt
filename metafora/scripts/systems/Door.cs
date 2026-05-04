using Godot;
using System;

public partial class Door : Node2D
{
	private AnimatedSprite2D aniDoor;
	private CollisionShape2D CollisionDetect;

	[Export(PropertyHint.Enum, "red, blue")] private string ColorSelect = "blue";

	[Export] PackedScene selectedScene {get; set;}

		public override void _Ready()
    {
		aniDoor = GetNode<AnimatedSprite2D>(ColorSelect);
		aniDoor.Visible = true;
		CollisionDetect = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
    }

	public void Activate(bool open)
	{
		if (open)
		{
			aniDoor.Play("open");	
			CollisionDetect.Disabled = false;
		}
		else
		{
			aniDoor.Play("close");
			CollisionDetect.Disabled = true;
		}
	}

	public void BodyEntered()
	{
		if (selectedScene != null)
		GetTree().ChangeSceneToFile(selectedScene.ResourcePath);	
	}
}
