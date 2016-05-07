using UnityEngine;
using System.Collections;

public abstract class Bullet : MonoBehaviour
{
    private BulletPool m_bulletManager;
    protected float m_TTLTimer = 50;
    protected CommandManager m_cmanager;

    //Unity default
    public virtual void Start()
    {
        m_cmanager = Managers.GetInstance().GetCommandManager();
    }

    public virtual void Update()
    {
        if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
        {
            if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
            {
                return;
            }

            MoveBullet();
        }
    }

    public virtual void MoveBullet()
    {
        if (!m_cmanager.GetTimeState())
        {
            if (m_TTLTimer > 0)
            {
                m_TTLTimer--;

                if (m_TTLTimer <= 0)
                {
                    m_cmanager.AddDestroyBulletCommand(gameObject, transform.position);
                }
            }
        }
        else
        {
            m_TTLTimer++;
        }
    }

    public void DestroyBullet()
    {
        m_TTLTimer = 50;
        m_bulletManager.ReturnBulletToPool(gameObject);
    }

    public void BringBackBullet(Vector2 p_position)
    {
        m_TTLTimer = 1;
        m_bulletManager.ReactivateBullet(gameObject);
        transform.position = p_position;
    }

    public void SetBulletManager(BulletPool p_bulletManager)
    {
        m_bulletManager = p_bulletManager;
    }

    public virtual void ActivateBullet()
    {
        m_TTLTimer = 50;
        gameObject.SetActive(true);
    }

    public virtual void ActivateBullet(Vector2 p_direction)
    {
        m_TTLTimer = 50;
        gameObject.SetActive(true);
    }
}
