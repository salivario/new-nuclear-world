using Godot;
using System;
using Npgsql;

public partial class FirstMenu : Control
{

	private LineEdit _usernameField;
	private LineEdit _passwordField;
	private Button _loginButton;
	private Button _registerButton;

	public override void _Ready()
	{
		_usernameField = GetNode<LineEdit>("Panel/VBoxContainer4/LoginField");
		_passwordField = GetNode<LineEdit>("Panel/VBoxContainer/PasswordField");
		_loginButton = GetNode<Button>("Panel/VBoxContainer3/EnterButton");
		_registerButton = GetNode<Button>("Panel/VBoxContainer3/ButtonReg");

		_loginButton.Pressed += OnLoginPressed;
		_registerButton.Pressed += OnRegisterPressed;
	}

	private void OnLoginPressed()
	{
		string username = _usernameField.Text;
		string passwordHash = _passwordField.Text;

		if (TryLogin(username, passwordHash))
		{
			GD.Print($"✅ Успешный вход: {username}");
			// Загружаем основную сцену игры
			var mainScene = GD.Load<PackedScene>("res://main/game/main_scene.tscn");
			GetTree().ChangeSceneToPacked(mainScene);
		}
		else
		{
			GD.Print("Incorrect data");
		}
	}

	private void OnRegisterPressed()
	{
		var registerScene = GD.Load<PackedScene>("res://main/interface/register.tscn");
		var registerMenu = registerScene.Instantiate();
		GetParent().AddChild(registerMenu);
		QueueFree();
	}

	private bool TryLogin(string username, string passwordHash)
	{
		try
		{
			using var conn = new NpgsqlConnection("Host=127.0.0.1;Username=testuser1;Password=12345;Database=newnuclearwinter");
			conn.Open();

			using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM players WHERE username=@u AND password_hash=@p", conn);
			cmd.Parameters.AddWithValue("u", username);
			cmd.Parameters.AddWithValue("p", passwordHash);

			long count = (long)cmd.ExecuteScalar();
			return count > 0;
		}
		catch (Exception e)
		{
			GD.PrintErr($"Ошибка БД: {e.Message}");
			return false;
		}
	}
}
