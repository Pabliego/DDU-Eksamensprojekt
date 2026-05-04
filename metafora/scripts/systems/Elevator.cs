using Godot;
using System;
using System.Threading;

public partial class Elevator : Node2D
{
	public AnimatedSprite2D ani;
	public AnimatableBody2D platform;

	[Export] private float MovementDuration = 2f;
	[Export] private float PauseDuration = 2f;

	[Export] private Node2D Point1 {get; set;}
	[Export] private Node2D Point2 {get; set;}

	[Export] private bool AwaitActivaion = false;

	[Export(PropertyHint.Enum, "red, blue, yellow, green")] private string ColorSelect;

	public Vector2 TargetPosition;
	




	
    public override void _Ready()
    {
        platform = GetNode<AnimatableBody2D>("AnimatableBody2D");
		ani = GetNode<AnimatedSprite2D>("AnimatableBody2D/AnimatedSprite2D");

		ani.Play(ColorSelect);

		if (AwaitActivaion)
		{
			
		} else
		{
			MovePlatform();
		}
    }

	
    private void MovePlatform()
    {
        platform.GlobalPosition = Point1.GlobalPosition;

        Tween tween = CreateTween().SetLoops();
        
        tween.TweenProperty(platform, "global_position", Point2.GlobalPosition, MovementDuration).SetTrans(Tween.TransitionType.Sine);
		tween.TweenInterval(PauseDuration);
        tween.TweenProperty(platform, "global_position", Point1.GlobalPosition, MovementDuration).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
		tween.TweenInterval(PauseDuration);
    }

	public void Activate()
	{
		MovePlatform();
	}

}
