using Godot;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

public partial class Dialogue : CanvasLayer
{
	[Export(PropertyHint.MultilineText)] public string DialogueText = "Placeholder Text bleep bloop";
	[Export] public float TextSpeed = 0.05f;
    private RichTextLabel textLabel;
    private AnimatedSprite2D siren;
	private AnimationPlayer loadani;
	private AudioStreamPlayer2D sound;
	[Export]private int StayOnScreenTime = 5000;

    public override void _Ready()
    {
        textLabel = GetNode<RichTextLabel>("Panel/RichTextLabel");
        siren = GetNode<AnimatedSprite2D>("Panel/AnimatedSprite2D");
		loadani = GetNode<AnimationPlayer>("AnimationPlayer");
		sound = GetNode<AudioStreamPlayer2D>("DialogueSound");

		loadani.Play("LoadIn");
    }

    public async void DisplayDialogue(string text, int delay)
    {
        textLabel.Text = "";
        siren.Play("talk");

        for (int i = 0; i < text.Length; i++)
        {
            textLabel.Text += text[i];;
			sound.Play();
            await Task.Delay((int)(TextSpeed * 1000));
        }

        siren.Play("idle");

		await Task.Delay(delay);
		loadani.Play("LoadOut");
    }


	public async void AnimationFinished(StringName AnimName)
	{
		if (AnimName == "LoadIn")
		{
			DisplayDialogue(DialogueText, StayOnScreenTime);
		}
	}
}
