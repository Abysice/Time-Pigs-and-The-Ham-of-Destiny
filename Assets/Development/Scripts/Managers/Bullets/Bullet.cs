using UnityEngine;
using System.Collections;

public abstract class Bullet : MonoBehaviour
{
    private BulletPool m_bulletManager;
    protected float m_TTLTimer = 50;

    //Unity default
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (m_TTLTimer > 0)
        {
            m_TTLTimer--;

            if (m_TTLTimer <= 0)
            {
                DestroyBullet();
            }
        }
    }

    protected void DestroyBullet()
    {
        m_TTLTimer = 5f;
        m_bulletManager.DestroyObjectPool(gameObject);
    }

    public void SetBulletManager(BulletPool p_bulletManager)
    {
        m_bulletManager = p_bulletManager;
    }

    public virtual void ActivateBullet()
    {
        gameObject.SetActive(true);
    }
}
