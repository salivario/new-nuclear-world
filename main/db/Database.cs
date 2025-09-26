using Godot;
using System;
using Npgsql;

public partial class Database : Node
{
	private string _connString = "Host=127.0.0.1;Username=testuser1;Password=12345;Database=newnuclearwinter";

	public override void _Ready()
	{
		GD.Print("Database node loaded!");
	}

	public bool RegisterUser(string username, string passwordHash, string email)
	{
		try
		{
			using var conn = new NpgsqlConnection(_connString);
			conn.Open();

			var query = "INSERT INTO players (username, password_hash, email) VALUES (@u, @p, @e)";
			using var cmd = new NpgsqlCommand(query, conn);
			cmd.Parameters.AddWithValue("u", username);
			cmd.Parameters.AddWithValue("p", passwordHash);
			cmd.Parameters.AddWithValue("e", email);

			cmd.ExecuteNonQuery();
			return true;
		}
		catch (Exception ex)
		{
			GD.PrintErr("Error: " + ex.Message);
			return false;
		}
	}
}
