using Godot;
using System;

public partial class SceneFadeTransition : Control
{
	
	AnimationPlayer ani;

	public override void _Ready()
	{
		ani = GetNode<AnimationPlayer>("AnimationPlayer");
		PlayTransition("FadeIn");
	}

	public void PlayTransition(string FadeInOrOut)
	{
		switch (FadeInOrOut)
		{
			case "FadeIn" :
				ani.Play("FadeIn");
				break;

			case "FadeOut" :
				ani.Play("FadeOut");
				break;

			default:
				GD.Print("Animation not included in swtichcase");
				break;
		}
	}

	public async void PlayTransitionAndChangeScene(string ScenePath)
	{
		ani.Play("FadeOut");
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile(ScenePath);
	}

}
