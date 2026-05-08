using Godot;
using System;
using System.Threading.Tasks;

public partial class FinalScene : CanvasLayer
{
	private AnimatedSprite2D ani;

	private AudioStreamPlayer2D doorsound;
	private AudioStreamPlayer2D naturesound;

	public override void _Ready()
	{
		ani = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		doorsound = GetNode<AudioStreamPlayer2D>("DoorSound");
		naturesound = GetNode<AudioStreamPlayer2D>("Nature");

		Play();
	}


	private async void Play()
	{
		await Task.Delay(2000); 
		doorsound.Play();
		ani.Play("default");
		naturesound.Play();
		await Task.Delay(1500); 
	}
}
