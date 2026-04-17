using Godot;
using System;
using System.Net.Http;

public partial class Unit555 : CharacterBody2D
{
	[Export] public float MaxSpeed;
    [Export] public float Acceleration;
    [Export] public float Deceleration;

	private AnimatedSprite2D Unit555ani;
	private float currentSpeed = 0f;
    private bool LookingRight = true;
    private bool Turning = false;

    
	public override void _Ready()
    {
        Unit555ani = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
	

	public override void _PhysicsProcess(double delta)
	{
        float input = 0f;

        if (Input.IsActionPressed("ui_right"))
        {
            input =  1f;  
        } 
        if (Input.IsActionPressed("ui_left"))
        {
            input = -1f;
        }


		bool detectTurn = false;
		if (input > 0f && !LookingRight || input < 0f && LookingRight)
		{
			detectTurn = true;
		}
		if (detectTurn && !Turning)
		{
			Turning = true; 
		}


		if (Turning)
        {
            currentSpeed = Mathf.MoveToward(currentSpeed, 0f, Deceleration * (float)delta);

            // Once fully stopped, flip and exit turning state
            if (Mathf.Abs(currentSpeed) < 1f)
            {
                LookingRight = !LookingRight;
                Unit555ani.FlipH = !LookingRight;
                Turning = false;
            }
        }
        else
        {
            if (input != 0f)
			{
                currentSpeed = Mathf.MoveToward(currentSpeed, input * MaxSpeed, Acceleration * (float)delta);
			}
			else
			{
                currentSpeed = Mathf.MoveToward(currentSpeed, 0f, Deceleration * (float)delta);
			}
        }

		Velocity = new Vector2(currentSpeed, Velocity.Y);
		if (!IsOnFloor())
		{
			Velocity += GetGravity() * (float)delta;
		}


		MoveAndSlide();
		UpdateAnimation();
	}

	private void UpdateAnimation()
    {
        if (Turning)
            Unit555ani.Play("turn");
        else if (Mathf.Abs(currentSpeed) > 5f)
            Unit555ani.Play("move");
        else
            Unit555ani.Play("idle");
    }

}
