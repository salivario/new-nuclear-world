using Godot;

public partial class UserSession : Node
{
	private static UserSession _instance;

	public static UserSession Instance => _instance;

	public int UserId { get; private set; }
	public string Username { get; private set; }

	public override void _Ready()
	{
		if (_instance == null)
		{
			_instance = this;
			this.Owner = null;
		}
		else
		{
			QueueFree();
		}
	}

	public void SetSession(int id, string username)
	{
		UserId = id;
		Username = username;
	}

	public void ClearSession()
	{
		UserId = 0;
		Username = "";
	}
}
