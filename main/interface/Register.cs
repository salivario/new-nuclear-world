using Godot;
using System;

public partial class Register : Control
{
	private LineEdit _loginField;
	private LineEdit _emailField;
	private LineEdit _passwordField;
	private LineEdit _repeatPasswordField;
	private Button _createButton;
	private Button _autoButton;

	public override void _Ready()
	{
		_loginField = GetNode<LineEdit>("Panel/VBoxContainer2/LoginField");
		_emailField = GetNode<LineEdit>("Panel/VBoxContainer2/EmailField");
		_passwordField = GetNode<LineEdit>("Panel/VBoxContainer4/PasswordField");
		_repeatPasswordField = GetNode<LineEdit>("Panel/VBoxContainer4/RepeatPasswordField");

		_createButton = GetNode<Button>("Panel/VBoxContainer3/Create");
		_autoButton = GetNode<Button>("Panel/VBoxContainer3/ButtonAuto");

		_createButton.Pressed += OnCreatePressed;
		_autoButton.Pressed += OnAutoButtonPressed;
	}

	private void OnCreatePressed()
	{
		string username = _loginField.Text;
		string email = _emailField.Text;
		string password = _passwordField.Text;
		string repeatPassword = _repeatPasswordField.Text;

		if (password != repeatPassword)
		{
			GD.PrintErr("Пароли не совпадают!");
			return;
		}

		var db = GetNode<Database>("/root/Database");

		bool success = db.RegisterUser(username, password, email);

		if (success)
		{
			GD.Print("100");
			var firstMenuScene = GD.Load<PackedScene>("res://main/interface/first_menu.tscn");
			var firstMenu = firstMenuScene.Instantiate();
			GetParent().AddChild(firstMenu);
			QueueFree();
		}
		else
		{
			GD.PrintErr("200");
		}
	}

	private void OnAutoButtonPressed()
	{
		var firstMenuScene = GD.Load<PackedScene>("res://main/interface/first_menu.tscn");
		var firstMenu = firstMenuScene.Instantiate();

		GetParent().AddChild(firstMenu);
		QueueFree();
	}
}
