﻿using UnityEngine;
using System.Collections;

public class EnemySin_Bullet : Bullet {

    private Vector2 m_bulletDirection = new Vector2(0f, 0.1f);

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveBullet()
    {
        base.MoveBullet();

        m_cmanager.AddMoveCommand(gameObject, new Vector2(transform.position.x, transform.position.y) + m_bulletDirection + new Vector2(1f, 0f) * Mathf.Sin(m_TTLTimer * 0.1f) * 0.05f);
    }

    /*
    void OnTriggerEnter(Collider p_collider)
    {
        if (p_collider.tag == "Player")
        {
            m_cmanager.AddDestroyBulletCommand(gameObject, transform.position);
        }
    }
     */

    public override void ActivateBullet()
    {
        base.ActivateBullet();
    }

    public override void ActivateBullet(Vector2 p_direction)
    {
        base.ActivateBullet();
        m_bulletDirection = p_direction;
    }
}
