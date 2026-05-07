using Godot;
using System;

public partial class Boundary : Area2D
{
	public void OnBodyEntered(Node2D other)
	{
		GetTree().ReloadCurrentScene();
	}
}
