using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	#region Public Variables
	public bool firing = false; //whether or not the enemy is firing bullets
	public float move_speed = 0.05f;
	public int enemy_type;
	public int move_type = 5;
	public int lives = 1;
    public int bullet_fire_rate = 20;
	#endregion
	
	#region Protected Variables
	#endregion
	
	#region Private Variable
	protected CommandManager m_cmanager;
    protected BulletPool m_bulletPool;
    protected BulletPool m_sineBulletPool;
    protected int bulletState = 0;
    protected int m_bulletTimer = 0;
    protected EnemyPool m_enemyManager;
    protected float m_TTLTimer = 400;
	#endregion
	
	#region Accessors
	#endregion
	
	#region Unity Defaults
	public virtual void Start ()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
        m_bulletPool = Managers.GetInstance().GetBulletPoolManager().GetBulletPool(100, Managers.GetInstance().GetGameProperties().enemyBulletPrefab);
        m_sineBulletPool = Managers.GetInstance().GetBulletPoolManager().GetBulletPool(40, Managers.GetInstance().GetGameProperties().sineEnemyBulletPrefab);
	}
	
	public virtual void Update ()
	{
		if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
		{
			return;
		}
		
		if (isAlive ())
		{
			moveCycle();
            if (!m_cmanager.GetTimeState() && !Managers.GetInstance().GetLoadManager().playerObject.GetComponent<PlayerController>().m_stillDead)
            {
                m_bulletTimer++;

                if (m_bulletTimer == bullet_fire_rate)
                {
                    m_bulletTimer = 0;
                    fireBullets();
                }

                if (m_TTLTimer > 0 )
                {
                    m_TTLTimer--;

                    if (m_TTLTimer <= 0)
                    {
                        m_cmanager.AddDestroyEnemyCommand(gameObject, transform.position);
                    }
                }
            }
            else
            {
                if (m_bulletTimer > 0)
                {
                    m_bulletTimer--;
                }

                m_TTLTimer++;
            }
		}
	}
	
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "p_bullet")
		{
            other.GetComponent<Bullet>().DestroyBulletCommand();
            m_cmanager.AddDestroyEnemyCommand(gameObject, transform.position);
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

    public void SetEnemyManager(EnemyPool p_enemyManager)
    {
        m_enemyManager = p_enemyManager;
    }

    public virtual void ActivateEnemy()
    {
        gameObject.SetActive(true);
    }

    public void DestroyEnemy()
    {
        m_enemyManager.ReturnEnemyToPool(gameObject);
    }

    public void BringBackEnemy(Vector2 p_position)
    {
        m_enemyManager.ReactivateEnemy(gameObject);
        transform.position = p_position;
    }
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	/// <summary>
	/// Fires bullets according to a pattern determined by it's enemy type.
	/// </summary>
	public virtual void fireBullets()
	{
		switch(enemy_type)
		{
		case 1: //v-type, fire three streams of bullets, two on both downward diagonals and one straight down
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, -0.1f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(-0.1f, -0.1f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0.1f, -0.1f));
			break;
		case 2: //o-type, fire eight streams of bullets, four in each cardinal direction and four in each diagonal
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0.1f, 0));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(-0.1f, 0));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, 0.1f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, -0.1f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0.1f, 0.1f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(-0.1f, -0.1f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(-0.1f, 0.1f));
            m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0.1f, -0.1f));
			break;
		case 3: //m-type, fire five streams of bullets, two on both downward diagonals and three downward
            if (bulletState == 0)
            {
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(-0.1f, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0.1f, -0.1f));
                bulletState = 1;
            }
            else if (bulletState == 1)
            {
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, -0.1f));
                bulletState = 2;
            }
            else if (bulletState == 2)
            {
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, -0.1f));
                bulletState = 0;
            }
			break;
        case 4: //fire sine pattern of bullets
            m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, transform.position, new Vector2(0, -0.1f));
            break;
        case 5:
            if (bulletState < 40)
                bulletState++;
            else
                bulletState = 0;

            if (bulletState < 10)
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2((bulletState - 10) / 100f, bulletState / 100f));
            else if (bulletState < 20)
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2((bulletState - 10) / 100f, (20 - bulletState)  / 100f));
            else if (bulletState < 30)
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2((30 - bulletState) / 100f, (20 - bulletState) / 100f));
            else
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2((30 - bulletState) / 100f, (bulletState - 40) / 100f));
            break;
		}
	}
	
	/// <summary>
	/// Moves the enemy along the screen according to its movement type, which is tied to its enemy type.
	/// </summary>
	public virtual void moveCycle()
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
