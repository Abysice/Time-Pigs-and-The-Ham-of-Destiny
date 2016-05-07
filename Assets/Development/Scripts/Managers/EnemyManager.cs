using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyManager : MonoBehaviour {

	#region Public Variables
	#endregion
	
	#region Protected Variables
	private int MAX_WAVES = 3; //wave 0 = start, last wave is the boss battle
	#endregion
	
	#region Private Variable
	private int wave_status = 0; //0 = not active, 1 = active, 2 = complete
	private int current_wave = 0; //begins at wave 0
	#endregion
	
	#region Accessors
	#endregion
	
	#region Unity Defaults
	void Start ()
	{
		GameObject[] wave0, wave1, wave2, wave3;
		//instantiate all enemies
		//instantiate all the enemy bullets
		//instantiate the boss
		
		//disable all enemies
		wave0 = GameObject.FindGameObjectsWithTag("0");
		foreach (GameObject enemy in wave0)
		{
			enemy.SetActive(false);
		}
		wave1 = GameObject.FindGameObjectsWithTag("1");
		foreach (GameObject enemy in wave1)
		{
			enemy.SetActive(false);
		}
		wave2 = GameObject.FindGameObjectsWithTag("2");
		foreach (GameObject enemy in wave2)
		{
			enemy.SetActive(false);
		}
		//disable all enemy bullets
		//disable the boss
		wave3 = GameObject.FindGameObjectsWithTag("3");
		foreach (GameObject enemy in wave3)
		{
			enemy.SetActive(false);
		}
	}
	
	void Update ()
	{
		if (checkWaveComplete())
			updateWave ();
	}
	#endregion
	
	#region Public Methods
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	/// <summary>
	/// Increments the wave count (current_wave) if the current wave's status is complete. If it increments the wave count, it will handle the set-up of the next wave of enemies and clean up of the last wave.
	/// </summary>
	void updateWave()
	{
		if (wave_status == 2 && current_wave > MAX_WAVES) //if the current wave is complete and there are no waves left
		{
			string current;
			GameObject[] wave_enemies;
			//sends message to CommandManager to suspend player time travel
			//shows animation for WAVE X, where X is the current wave's number. Shows an animation for BOSS, if the next wave is the last one
			
			//disable all active enemies
			current = current_wave.ToString();
			wave_enemies = GameObject.FindGameObjectsWithTag(current);
			foreach (GameObject enemy in wave_enemies)
			{
				enemy.SetActive(false);
			}
			//disable all active enemy bullets
			
			current_wave++;
			wave_status = 0; //disable the current wave so enemies and enemy bullets don't act (their actions are dependent on the wave's status)
			//enable the enemies tagged with the number of the current wave
			current = current_wave.ToString();
			wave_enemies = GameObject.FindGameObjectsWithTag(current);
			foreach (GameObject enemy in wave_enemies)
			{
				enemy.SetActive(true);
			}
			//enable 5 * (# of enemies enabled above) enemy bullets
			wave_status = 1; //enable the current wave so that the enemies and bullets begin to act
		} 
		else if (wave_status == 2) //if the current wave is complete and there are no more waves, i.e. the game is complete
		{
			//the game is over, play the end game animation/ splash screen
		}
	}
	
	/// <summary>
	/// Runs through all the active enemies in the scene (i.e. the enemies tagged with the number of the current wave) and checks their alive value. If all enemies are dead, it labels the current wave's status (wave_status) as complete (2).
	/// </summary>
	bool checkWaveComplete()
	{
		string current;
		GameObject[] wave_enemies;
		bool complete = false;
		
		current = current_wave.ToString();
		wave_enemies = GameObject.FindGameObjectsWithTag(current);
		foreach (GameObject enemy in wave_enemies)
		{
			EnemyController state = enemy.GetComponent<EnemyController>();
			if (state.isAlive())
				break;
			complete = true;
		}
		return complete;
	}
	#endregion
}
