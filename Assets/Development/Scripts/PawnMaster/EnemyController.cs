using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	#region Public Variables
	public bool firing; //whether or not the enemy is firing bullets
	public bool move_speed = 0.2f;
	#endregion
	
	#region Protected Variables
	#endregion
	
	#region Private Variable
	private float health;
	private string enemy_type;
	#endregion
	
	#region Accessors
	#endregion
	
	#region Unity Defaults
	void Start ()
	{

	}
	
	void Update ()
	{

	}
	#endregion
	
	#region Public Methods
	public bool isAlive()
	{
		return true;
	}
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	/// <summary>
	/// Fires bullets according to a pattern determined by it's enemy type.
	/// </summary>
	private void fireBullets()
	{
		switch(enemy_type)
		{
		case 1: //v-type, fire three streams of bullets, two on both downward diagonals and one straight down
			break;
		case 2: //o-type, fire eight streams of bullets, four in each cardinal direction and four in each diagonal
			break;
		case 3: //m-type, fire five streams of bullets, two on both downward diagonals and three downward
			break;
		}
	}
	
	private void moveCycle()
	{
		switch(enemy_type)
		{
		case 1: //v-type, move horizontally across the screen
			break;
		case 2: //o-type, move diagonally down the screen
			break; 
		case 3: //m-type, move vertically down the screen
			break; 
		}	
	}
	#endregion
}
