using UnityEngine;
using System.Collections;

public class BossController : EnemyController
{
    #region Public Variables
    public bool firing = false; //whether or not the enemy is firing bullets
    public float move_speed = 0.05f;
    public int move_type = 5;
    public int lives = 1;
    public int health = 100;
    public int bullet_fire_rate = 20;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variable
    private CommandManager m_cmanager;
    private BulletPool m_bulletPool;
    private BulletPool m_sineBulletPool;
    private int bulletState = 0;
    private int m_bulletTimer = 0;
    private int m_bulletpattern = 0;
    private int m_enemyState = 0;
    private EnemyPool m_enemyManager;
    private float m_stateTimer = 0;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    public override void Start()
    {
        m_cmanager = Managers.GetInstance().GetCommandManager();
        m_bulletPool = Managers.GetInstance().GetBulletPoolManager().GetBulletPool(100, Managers.GetInstance().GetGameProperties().enemyBulletPrefab);
        m_sineBulletPool = Managers.GetInstance().GetBulletPoolManager().GetBulletPool(40, Managers.GetInstance().GetGameProperties().sineEnemyBulletPrefab);
    }

    public override void Update()
    {
        if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
        {
            return;
        }

        if (isAlive())
        {
            moveCycle();
            if (!m_cmanager.GetTimeState())
            {
                m_bulletTimer++;

                if (m_bulletTimer == bullet_fire_rate)
                {
                    m_bulletTimer = 0;
                    fireBullets();
                }

                m_stateTimer++;

                if (m_stateTimer == 100)
                {
                    m_stateTimer = 0;
                    //ChangeStates();
                }
            }
            else
            {
                if (m_bulletTimer > 0)
                {
                    m_bulletTimer--;
                }

                if (m_stateTimer > 0)
                {
                    m_stateTimer--;
                }
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "p_bullet")
        {
            health--;
            other.GetComponent<Bullet>().DestroyBulletCommand();

            if (health < 0)
                m_cmanager.AddDestroyEnemyCommand(gameObject, transform.position);
        }

    }
    #endregion

    #region Public Methods
    public override void ActivateEnemy()
    {
        gameObject.SetActive(true);
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    /// <summary>
    /// Fires bullets according to a pattern determined by it's enemy type.
    /// </summary>
    public override void fireBullets()
    {
        switch (m_bulletpattern)
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
                    m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2((bulletState - 10) / 100f, (20 - bulletState) / 100f));
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
    public override void moveCycle()
    {
        Vector3 move = Vector3.zero;
        switch (m_enemyState)
        {
            case 0: //first, the boss will move down
                move += Vector3.down * move_speed;
                break;
            case 1: //v-type, move horizontally left to right across the screen
                move += Vector3.left * move_speed;
                break;
            case 2: //o-type 1, move right
                move += -Vector3.left * move_speed;
                break;
        }
        m_cmanager.AddMoveCommand(gameObject, gameObject.transform.position + move);
    }
    #endregion
}
