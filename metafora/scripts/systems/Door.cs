using Godot;
using System;

public partial class Door : Node2D
{
	private AnimatedSprite2D aniDoor;
	private Area2D AreaDetect;

	private CollisionShape2D Collision;

	[Export(PropertyHint.Enum, "red, blue")] private string ColorSelect = "blue";

	[Export] PackedScene selectedScene {get; set;}

		public override void _Ready()
    {
		aniDoor = GetNode<AnimatedSprite2D>(ColorSelect);
		aniDoor.Visible = true;
		
		AreaDetect = GetNode<Area2D>("Area2D");
		Collision = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
    }

    public override void _Process(double delta)
    {
        if (AreaDetect.Monitoring == true)
		{

		} else
		{
			
		}
    }


	public void Activate(bool open)
	{
		if (open)
		{
			aniDoor.Play("open");	
			AreaDetect.Monitoring = true;

		}
		else
		{
			aniDoor.Play("close");
			AreaDetect.Monitoring = false;
		}
	}

	public void BodyEntered(Node2D body)
	{
		if (body.Name == "Unit555Body")
		{
			if (selectedScene != null)
			{
				GetTree().ChangeSceneToFile(selectedScene.ResourcePath);	
			}		
		}

	}
}
