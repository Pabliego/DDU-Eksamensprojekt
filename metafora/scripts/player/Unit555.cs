using Godot;
using System;

public partial class Unit555 : CharacterBody2D
{
    [Export] public float MaxSpeed = 150f;
    [Export] public float Acceleration = 350f;
    [Export] public float Deceleration = 250f;

    [Export] public float JumpVelocity = -400f;

    [Export] public float PushForce = 100f;

    [Export] public bool HasLegs = false;

    private AnimatedSprite2D Unit555ani;
    private AudioStreamPlayer2D sound;
    private AudioStreamPlaybackInteractive interactiveSound;
    private float currentSpeed = 0f;
    private bool Turning = false;
    private bool Jumping = false;
    
    private int FacingDirection = 1; // 1 = right, -1 = left

    public override void _Ready()
    {
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Visible = false;
        GetNode<AnimatedSprite2D>("AnimatedSprite2D2").Visible = false;

        if (!HasLegs)
        {
            Unit555ani = GetNode<AnimatedSprite2D>("AnimatedSprite2D");   
        }
        else
        {
            Unit555ani = GetNode<AnimatedSprite2D>("AnimatedSprite2D2"); 
        }
        Unit555ani.Visible = true;
        
        sound = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        sound.Play();
        interactiveSound = sound.GetStreamPlayback() as AudioStreamPlaybackInteractive;
    }

    string CurrentSetClip;
    private void SetSound(string SetClip)
    {
        if (CurrentSetClip == SetClip) return;
        CurrentSetClip = SetClip;
        interactiveSound.SwitchToClipByName(SetClip);
    }

    public override void _PhysicsProcess(double delta)
    {
        string ExecutedSound = "IdleSound";
        
        int input = 0;

        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, JumpVelocity);
            Jumping = true;
        }
        else if (IsOnFloor() && Jumping)
        {
            Jumping = false;
        }


        if (Input.IsActionPressed("ui_right"))
        {
            input = 1;
            ExecutedSound = "WorkingSound";
        }
        if (Input.IsActionPressed("ui_left"))
        {
            input = -1;
            ExecutedSound = "WorkingSound";
        }

        SetSound(ExecutedSound);


        if (input != 0 && input != FacingDirection && !Turning)
        {
            FacingDirection = input;
            if (IsOnFloor() && !Turning)
            {
               Turning = true;
            }
            else if (!IsOnFloor())
            {
                Unit555ani.FlipH = FacingDirection != 1;
            }
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
        if (!HasLegs)
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
        else
        {
            if (Jumping)
            {
                string jumpani;

                if (FacingDirection == 1)
                {
                    jumpani = "jumpleft";
                }
                else
                {
                    jumpani = "jump";
                }
                if (Unit555ani.Animation != jumpani)
                Unit555ani.Play(jumpani);
            }
            else if (Turning)
            {
                string turnani;

                if (FacingDirection == 1)
                {
                    turnani = "turnleft";
                }
                else
                {
                    turnani = "turn";
                }
                
                Unit555ani.Play(turnani); 
            }
            else if (Mathf.Abs(currentSpeed) > 5f)
            {
                string moveani;

                if (FacingDirection == 1)
                {
                    moveani = "move";
                }
                else
                {
                    moveani = "moveleft";
                }
                
                Unit555ani.Play(moveani);   
            }
            else
            {
                string idleani;

                if (FacingDirection == 1)
                {
                    idleani = "idle";
                }
                else
                {
                    idleani = "idleleft";
                }
                
                Unit555ani.Play(idleani);
            }
        }
    }
}