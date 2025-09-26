using Godot;
using System;

public partial class FirstMenu : Control
{
	private Button _registerButton;

	public override void _Ready()
	{
		_registerButton = GetNode<Button>("Panel/VBoxContainer3/Button2");
		_registerButton.Pressed += OnRegisterButtonPressed;
	}

	private void OnRegisterButtonPressed()
	{
		var registerScene = GD.Load<PackedScene>("res://main/interface/register.tscn");
		var registerMenu = registerScene.Instantiate();

		GetParent().AddChild(registerMenu);
		QueueFree();
	}
}
