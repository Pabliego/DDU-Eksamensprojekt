using Godot;
using System;
using System.Data;
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

	[Export] public Godot.Collections.Array<Elevatorbutton> LinkedButtons = new();

	public Vector2 TargetPosition;
	
	private bool Activated = false;



	
    public override void _Ready()
    {
        platform = GetNode<AnimatableBody2D>("AnimatableBody2D");
		ani = GetNode<AnimatedSprite2D>("AnimatableBody2D/AnimatedSprite2D");

		ani.Play(ColorSelect);
		platform.GlobalPosition = Point1.GlobalPosition;

		if (AwaitActivaion)
		{
			
		} else
		{
			MovePlatform();
		}
    }

	public void UpdateElevatorState(Elevatorbutton button, bool activated)
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
			Elevatorbutton elv = LinkedButtons[i] as Elevatorbutton;
			if (elv == null || elv.Activated == false)
			{
				AllLinkedPressed = false;
				break;
			}
		}

		if (AllLinkedPressed == true)
		{
			Activate();
		}
	}


	Tween tween;
    private void MovePlatform()
    {
        if (tween != null)
		{
			tween.Kill();
		}

		tween = CreateTween().SetLoops();
        
        tween.TweenProperty(platform, "global_position", Point2.GlobalPosition, MovementDuration).SetTrans(Tween.TransitionType.Sine);
		tween.TweenInterval(PauseDuration);
        tween.TweenProperty(platform, "global_position", Point1.GlobalPosition, MovementDuration).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
		tween.TweenInterval(PauseDuration);
    }

	public void Deactivate()
	{
		if (tween != null)
		{
			tween.Kill();
			tween = null;
			Activated = false;
		}
	}

	public void Activate()
	{
		if (tween != null && tween.IsRunning())
		{
			return;	
		}
	
		MovePlatform();
		Activated = true;

	}

}
