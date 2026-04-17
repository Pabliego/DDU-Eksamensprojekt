using Godot;
using System;

public partial class Door : Node2D
{
	private AnimatedSprite2D aniDoor;
	private CollisionShape2D CollisionDetect;

		public override void _Ready()
    {
        aniDoor = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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
}
