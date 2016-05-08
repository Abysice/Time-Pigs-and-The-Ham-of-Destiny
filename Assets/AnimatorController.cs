// Script description goes here.
//
// Written By: Adam Bysice

using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	private CommandManager m_cmanager;
	private Animator m_animator;
	#endregion

	#region Private Variables
	private bool InMyState = false;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	public void Start()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
		m_animator = gameObject.GetComponent<Animator>();
	}

	public void Update()
	{
		if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Explosion"))
		{
			// Avoid any reload.
			InMyState = true;
		}
		else if (this.InMyState)
		{
			this.InMyState = false;
			m_cmanager.AddFinishedExplosion(m_animator, this.gameObject);
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
