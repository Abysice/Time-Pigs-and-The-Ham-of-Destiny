using UnityEngine;
using System.Collections;

public class Player_Bullet : Bullet {

    private Vector2 m_bulletDirection = new Vector2(0f, 0.1f);

	// Use this for initialization
	void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();

        m_cmanager.AddMoveCommand(gameObject, new Vector2(transform.position.x, transform.position.y) + m_bulletDirection);
	}

    void OnTriggerEnter(Collider p_collider)
    {
        if (p_collider.tag == "Enemy")
        {
            m_cmanager.AddDestroyBulletCommand(gameObject, transform.position);
        }
    }

    public override void ActivateBullet()
    {
        base.ActivateBullet();
    }
}
