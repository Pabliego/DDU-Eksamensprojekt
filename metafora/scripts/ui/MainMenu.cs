using Godot;
using System;
using System.Threading.Tasks;

public partial class MainMenu : Control
{
    LineEdit InputField;
    RichTextLabel OutputText;
    private bool CursorVisible = true;
    private Timer CursorTimer;

    private AudioStreamPlayer2D alrt; 
    private AudioStreamPlayer2D line; 
    
    public override void _Ready()
    {
        InputField = GetNode<LineEdit>("InputField");
        OutputText = GetNode<RichTextLabel>("OutputText");
        alrt = GetNode<AudioStreamPlayer2D>("AlertSound");
        line = GetNode<AudioStreamPlayer2D>("LineSound");

        InputField.TextSubmitted += InputSubmit;

        _ = BootScreen();
    }


    private async Task WriteToTerminal(string text, float typedelay, bool newline)
    {
        if (typedelay <= 0f)
        {
            OutputText.Text += text;
        }else{
        foreach (char character in text)
        {
            OutputText.Text += character;
            await ToSignal(GetTree().CreateTimer(typedelay), SceneTreeTimer.SignalName.Timeout);
        };
        }
        if (newline == true)
        {
            OutputText.Text += "\n";   
        }
    }


    public async Task BootScreen()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        
        InputField.Editable = false;

        string[] MetaforaLogo =
        {
            "                                                   ,d8888b                           ",
            "                                d8P                88P'                              ",
            "                            d888888P           d888888P                             ",
            "        88bd8b,d88b   d8888b  ?88'   d888b8b     ?88'   d8888b    88bd88b  d888b8b  ",
            "       88P'`?8P'?8b d8b_,dP  88P   d8P' ?88     88P   d8P' ?88   88P'   `d8P' ?88  ",
            "      d88  d88  88  P88b      88b   88b  ,88b   d88    88b  d88  d88      88b  ,88b ",
            "     d88' d88'  88b `?888P'  `?8b  `?88P'`88b d88'    `?8888P' d88'      `?88P'`88b",
            "                                                                         corp.",
            "                                                                         All Rights Reserved.",
            ""                                                               
        };

        foreach (string line in MetaforaLogo)
        {
            await WriteToTerminal(line, 0f, true);
            await ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout);
        }

        await ToSignal(GetTree().CreateTimer(0.4f), SceneTreeTimer.SignalName.Timeout);

        //await WriteToTerminal("REMOTE CONNECTION SYSTEM BOOT v7.3.6 ................. [OK]", 0.001f, false);
        await WriteToTerminal("REMOTE CONNECTION SYSTEM BOOT v7.3.6 ", 0.001f, false);
        await WriteToTerminal(".................", 0.05f, false);
        await WriteToTerminal(" [OK]", 0f, true);
        line.Play();
        //await WriteToTerminal("Loading user interface ...................... [FAIL]", 0.005f);
        await WriteToTerminal("Loading user interface ", 0.001f, false);
        await WriteToTerminal("......................", 0.08f, false);
        await WriteToTerminal(" [FAIL]", 0f, true);
        line.Play();
        await WriteToTerminal("Initializing connection hardware ", 0.008f, false);
        await WriteToTerminal("..................", 0.005f, false);
        await WriteToTerminal(" [OK]", 0f, true);
        line.Play();
        await WriteToTerminal("Attempting connection ..................", 0.008f, false);
        await WriteToTerminal(" [FAIL]", 0f, true);
        line.Play();
        await WriteToTerminal("Attempting connection ..................", 0.006f, false);
        await WriteToTerminal(" [FAIL]", 0f, true);
        line.Play();
        await WriteToTerminal("Attempting connection ..................", 0.003f, false);
        await WriteToTerminal(" [FAIL]", 0f, true);
        line.Play();

        await ToSignal(GetTree().CreateTimer(0.8f), SceneTreeTimer.SignalName.Timeout);

        //await WriteToTerminal("Attempting connection .................. [OK]", 0.002f);
        await WriteToTerminal("Attempting connection ", 0.002f, false);
        await WriteToTerminal("..................", 0.10f, false);
        await WriteToTerminal(" [OK]", 0f, true);
        line.Play();

        await WriteToTerminal("\nType 'help' for available commands.", 0.001f, true);
        await WriteToTerminal("> ", 0.01f, true);
        line.Play();

        InputField.Editable = true;
        InputField.GrabFocus();
    }

    bool quitstatus = false;
    public async void InputSubmit(string text)
    {
        string input = text.Trim().ToLower();
        InputField.Clear();

        alrt.Play();
        

        switch (input)
        {
            case "start":
                _ = StartGameSequence();
                break;
            case "help":
                await WriteToTerminal("> Available commands: start, quit, options", 0.01f, true);
                break;
            case "quit":
                await WriteToTerminal("Are you sure? y/n", 0f, true);
                quitstatus = true;
                break;

            case "y":
                if (quitstatus == true)
                {
                    GetTree().Quit();
                } else {
                    InputField.Editable = false;
                    await WriteToTerminal($"> Unknown command: {input}", 0.01f, true);
                    InputField.Editable = true;
                    InputField.GrabFocus();
                    quitstatus = false;
                }
                break;
            case "n":
                quitstatus = false;
                break;


            default:
                await WriteToTerminal($"> Unknown command: {input}", 0.01f, true);
                quitstatus = false;
                break;
        }

        InputField.GrabFocus();

    }


    private async Task StartGameSequence()
    {
        GetTree().CurrentScene.GetNode<SceneFadeTransition>("SceneFadeTransition").PlayTransition("FadeOut");
        await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
        GetTree().ChangeSceneToFile("res://scenes/levels/Level 1.tscn");
    } 


}