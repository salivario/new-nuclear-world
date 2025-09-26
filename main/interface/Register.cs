using Godot;
using System;

public partial class Register : Control
{
		private Button _autoButton;

	public override void _Ready()
	{
		_autoButton = GetNode<Button>("Panel/VBoxContainer3/ButtonAuto");
		_autoButton.Pressed += OnAutoButtonPressed;
	}

	private void OnAutoButtonPressed()
	{
		var firstMenuScene = GD.Load<PackedScene>("res://main/interface/first_menu.tscn");
		var firstMenu = firstMenuScene.Instantiate();

		GetParent().AddChild(firstMenu);
		QueueFree();
	}
}
