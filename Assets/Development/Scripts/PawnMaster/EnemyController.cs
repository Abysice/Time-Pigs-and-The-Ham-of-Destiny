using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	#region Public Variables
	public bool firing = false; //whether or not the enemy is firing bullets
	public float move_speed = 0.05f;
	public int enemy_type;
	public int move_type = 5;
	public int lives = 1;
	#endregion
	
	#region Protected Variables
	#endregion
	
	#region Private Variable
	private CommandManager m_cmanager;
    private int m_bulletTimer;
    private BulletPool m_bulletPool;
	#endregion
	
	#region Accessors
	#endregion
	
	#region Unity Defaults
	void Start ()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
        m_bulletPool = Managers.GetInstance().GetBulletPoolManager().GetBulletPool(20, Managers.GetInstance().GetGameProperties().enemyBulletPrefab);
	}
	
	void Update ()
	{
		if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
		{
			return;
		}
		
		if (isAlive ())
		{
			moveCycle();
            if (!m_cmanager.GetTimeState())
            {
                m_bulletTimer++;

                if (m_bulletTimer == 10)
                {
                    m_bulletTimer = 0;
                    fireBullets();
                }
            }
            else
            {
                if (m_bulletTimer > 0)
                {
                    m_bulletTimer--;
                }
            }
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "p_bullet")
		{
			
			Destroy (other.gameObject);
			Destroy (this.gameObject, 0.01f);
		}
		
	}
	#endregion
	
	#region Public Methods
	public bool isAlive()
	{
		if (lives > 0)
			return true;
		else
			return false;
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
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, -0.2f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(-0.2f, -0.2f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0.2f, -0.2f));
			break;
		case 2: //o-type, fire eight streams of bullets, four in each cardinal direction and four in each diagonal
			break;
		case 3: //m-type, fire five streams of bullets, two on both downward diagonals and three downward
			break;
		}
	}
	
	/// <summary>
	/// Moves the enemy along the screen according to its movement type, which is tied to its enemy type.
	/// </summary>
	private void moveCycle()
	{
		Vector3 move = Vector3.zero;
		switch(move_type)
		{
		case 1: //v-type, move horizontally right to left across the screen
			move += Vector3.right * move_speed;
			break;
		case 2: //v-type, move horizontally left to right across the screen
			move += Vector3.left * move_speed;
			break;
		case 3: //o-type 1, move down the screen along the right diagonal
			move += (Vector3.down + Vector3.right) * move_speed;
			break;
		case 4: //o-type 1, move down the screen along the left diagonal
			move += (Vector3.down + Vector3.left) * move_speed;
			break;
		case 5: //o-type 1, move up the screen along the right diagonal
			move += (Vector3.up + Vector3.right) * move_speed;
			break;
		case 6: //o-type 1, move up the screen along the left diagonal
			move += (Vector3.up + Vector3.left) * move_speed;
			break;
		case 7: //m-type, move vertically down the screen
			move += Vector3.down * move_speed;
			break;
		}
		m_cmanager.AddMoveCommand(gameObject, gameObject.transform.position + move);
	}
	#endregion
}
