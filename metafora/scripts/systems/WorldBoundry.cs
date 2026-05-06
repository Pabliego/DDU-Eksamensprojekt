using Godot;
using System;

public partial class Area2d : Area2D
{
	public void OnBodyEntered(Node2D other)
	{
		GetTree().ReloadCurrentScene();
	}
}
