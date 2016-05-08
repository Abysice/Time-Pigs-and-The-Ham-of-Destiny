using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossController : EnemyController
{
    #region Public Variables
    public int health = 100;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variable
    private int m_bulletpattern = 0;
    private int m_enemyState = 0;
    private int m_enemyMovement = 0;
    private float m_stateTimer = 100;
    private Image m_healthBar;
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

                m_stateTimer--;

                if (m_stateTimer == 0)
                {
                    m_enemyState++;
                    ChangeStates();
                    ChangeBulletPattern();
                }
            }
            else
            {
                if (m_bulletTimer > 0)
                {
                    m_bulletTimer--;
                }

                m_stateTimer++;

                if (m_enemyState == 1)
                {
                    if (m_stateTimer == 50)
                    {
                        m_enemyState--;
                        ChangeReverseStates();
                        ChangeBulletPattern();
                        m_stateTimer = 0;
                    }
                        
                }
                else 
                {
                    if (m_stateTimer == 100)
                    {
                        if (m_enemyState != 0)
                        {
                            m_enemyState--;
                            ChangeReverseStates();
                            ChangeBulletPattern();
                            m_stateTimer = 0;
                        }
                    }
                }
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "p_bullet")
        {
            m_cmanager.AddBossTakeDamage(gameObject);
            other.GetComponent<Bullet>().DestroyBulletCommand();

            if (health < 0)
			{
				m_cmanager.AddDestroyEnemyCommand(gameObject, transform.position);
				Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_01_MENU);
			}
                
        }

    }
    #endregion

    #region Public Methods
    public void IncreaseHealth()
    {
        health++;
        m_healthBar.fillAmount = (health / 100f);
    }

    public void DecreaseHealth()
    {
        health--;
        m_healthBar.fillAmount = (health / 100f);
    }

    public override void ActivateEnemy()
    {
        gameObject.SetActive(true);
        m_healthBar.transform.parent.gameObject.SetActive(true);
    }

    public override void DestroyEnemy()
    {
        m_enemyManager.ReturnEnemyToPool(gameObject);
        m_healthBar.transform.parent.gameObject.SetActive(false);
    }

    public override void BringBackEnemy(Vector2 p_position)
    {
        m_enemyManager.ReactivateEnemy(gameObject);
        transform.position = p_position;
        m_healthBar.transform.parent.gameObject.SetActive(true);
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
        Debug.Log("Fire Bullets");
        switch (m_bulletpattern)
        {
            case 0:
                break;
            case 1: //v-type, fire three streams of bullets, two on both downward diagonals and one straight down
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, -0.1f));

                 m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, -0.1f));

                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0.1f));

                 m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0.1f));
                break;
            case 2: //o-type, fire eight streams of bullets, four in each cardinal direction and four in each diagonal
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, -0.1f));

                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_bulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, -0.1f));
                break;
            case 3: //m-type, fire five streams of bullets, two on both downward diagonals and three downward
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, -0.1f));

                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, -0.1f));
                break;
            case 4: //fire sine pattern of bullets
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, -0.1f));

                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0.1f, 0f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(-0.1f, 0f));

                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x + 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
                m_cmanager.AddSpawnBulletCommand(m_sineBulletPool, new Vector3(transform.position.x - 0.7f, transform.position.y - 1.1f, transform.position.z), new Vector2(0, 0.1f));
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

    private void ChangeStates()
    {
        if (m_enemyState == 1)
        {
            m_stateTimer = 50;
            m_enemyMovement = 1;
        }
        else if (m_enemyState % 2 == 0)
        {
            m_stateTimer = 100;
            m_enemyMovement = 2;
        }
        else
        {
            m_stateTimer = 100;
            m_enemyMovement = 1;
        }
    }

    private void ChangeReverseStates()
    {
        if (m_enemyState == 0)
        {
            m_enemyMovement = 0;
        }
        else if (m_enemyState % 2 == 0)
        {
            m_enemyMovement = 2;
        }
        else
        {
            m_enemyMovement = 1;
        }
    }

    private void ChangeBulletPattern()
    {
        if (m_enemyState <= 1)
        {
            m_bulletpattern = 0;
        }
        else if (m_enemyState % 4 == 0)
        {
            m_bulletpattern = 1;
        }
        else if (m_enemyState % 3 == 0)
        {
            m_bulletpattern = 2;
        }
        else if (m_enemyState % 2 == 0)
        {
            m_bulletpattern = 3;
        }
        else
        {
            m_bulletpattern = 4;
        }
    }

    /// <summary>
    /// Moves the enemy along the screen according to its movement type, which is tied to its enemy type.
    /// </summary>
    public override void moveCycle()
    {
        Vector3 move = Vector3.zero;
        move += Vector3.up * 0.03f;
        switch (m_enemyMovement)
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
