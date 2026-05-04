using Godot;
using System;

public partial class Unit555 : CharacterBody2D
{
    [Export] public float MaxSpeed = 150f;
    [Export] public float Acceleration = 350f;
    [Export] public float Deceleration = 250f;

    [Export] public float PushForce = 100f;

    private AnimatedSprite2D Unit555ani;
    private float currentSpeed = 0f;
    private bool Turning = false;
    private int FacingDirection = 1; // 1 = right, -1 = left

    public override void _Ready()
    {
        Unit555ani = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        int input = 0;

        if (Input.IsActionPressed("ui_right"))
        {
            input = 1;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            input = -1;
        }


        if (input != 0 && input != FacingDirection && !Turning)
        {
            FacingDirection = input;
            Turning = true;
        }

        if (Turning)
        {
            currentSpeed = Mathf.MoveToward(currentSpeed, 0f, Deceleration * (float)delta);

            if (Mathf.Abs(currentSpeed) < 1f)
            {
                Unit555ani.FlipH = FacingDirection != 1;
                Turning = false;
            }
        }
        else
        {
            if (input != 0)
                currentSpeed = Mathf.MoveToward(currentSpeed, input * MaxSpeed, Acceleration * (float)delta);
            else
                currentSpeed = Mathf.MoveToward(currentSpeed, 0f, Deceleration * (float)delta);
        }

        Velocity = new Vector2(currentSpeed, Velocity.Y);
        if (!IsOnFloor())
            Velocity += GetGravity() * (float)delta;

        MoveAndSlide();
        UpdateAnimation();

        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);

            if (collision.GetCollider() is RigidBody2D pushable)
            {
                pushable.ApplyForce(-collision.GetNormal() * PushForce);
            }
        }
    }

    private void UpdateAnimation()
    {
        if (Turning)
        {
            if (Unit555ani.Animation != "turn")
                Unit555ani.Play("turn");
        }
        else if (Mathf.Abs(currentSpeed) > 5f)
            Unit555ani.Play("move");
        else
            Unit555ani.Play("idle");
    }
}