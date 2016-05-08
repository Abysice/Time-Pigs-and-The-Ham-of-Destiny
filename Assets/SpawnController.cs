using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	//public GameObject spawned_object;
	public int spawned_object_type;
	private bool spawn = false;
	private CommandManager m_cmanager;
	private EnemyManager m_enemymanager;

	// Use this for initialization
	void Start ()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
		m_enemymanager = Managers.GetInstance().GetEnemyManager();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
		{
			return;
		}
		
		if (spawn)
		{
			m_enemymanager.SpawnEnemy(spawned_object_type, new Vector2(transform.position.x, transform.position.y), gameObject.tag);
			spawn = false;
		}
	}
	
	public void allowSpawn()
	{
		spawn = true;
	}
}
