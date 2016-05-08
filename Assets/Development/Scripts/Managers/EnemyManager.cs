using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyManager : MonoBehaviour {

	#region Public Variables
	public GameObject enemyObject;
	#endregion
	
	#region Protected Variables
	protected float m_TTWTimer = 100;
	#endregion
	
	#region Private Variable
	private int current_wave = 0; //begins at wave 0
	private CommandManager m_cmanager;
	#endregion
	
	#region Accessors
	#endregion
	
	#region Unity Defaults
	void Start ()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
	}
	
	public void Update ()
	{
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
			{
				return;
			}	
			waveCount();
		}
	}
	#endregion
	
	#region Public Methods
	public void waveCount()
	{
		if (!m_cmanager.GetTimeState())
		{
			if (m_TTWTimer > 0)
			{
				m_TTWTimer--;
				Debug.Log (m_TTWTimer);
				
				if (m_TTWTimer <= 0)
				{
					m_TTWTimer = 500;
					executeCurrentWave();
				}
			}
		}
		else
		{
			m_TTWTimer++;
		}
		Debug.Log (m_TTWTimer);
	}
	
	public void SpawnEnemy(int type, Vector3 pos, string wave_num)
	{
        m_cmanager.AddSpawnEnemyCommand(Managers.GetInstance().GetEnemyPoolManager().GetEnemyPool(type - 1), pos);

		//enemyObject.tag = wave_num;
	}
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	/// <summary>
	/// Instantiates the enemies for the current wave and sets the timer for the next wave to begin.
	/// </summary>
	private void executeCurrentWave()
	{
		string cur;
		GameObject[] wave;
		
		if (current_wave > 0) //if at least one wave has happened prior, perform cleanup
		{
			cur = (current_wave - 1).ToString();
			wave = GameObject.FindGameObjectsWithTag(cur);
			foreach (GameObject thing in wave)
			{
				thing.gameObject.SetActive(false);
			}
		}
		
		cur = current_wave.ToString();
		wave = GameObject.FindGameObjectsWithTag(cur);
		foreach (GameObject spawner in wave)
		{
			SpawnController cont = spawner.GetComponent<SpawnController>();
			cont.allowSpawn();
		}
		if (current_wave < 9)
			current_wave++;
	}
	#endregion
}
