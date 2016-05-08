// Script description goes here.
//
// Written By: Adam Bysice

using UnityEngine;
using System.Collections;

public class DisableComponentCommand : CommandBase {

	private SpriteRenderer m_comp;
	private GameObject m_explosion;
	public DisableComponentCommand()
	{
		m_creationTime = Time.fixedTime;
	}

	public DisableComponentCommand(SpriteRenderer p_comp, GameObject p_explosion)
	{
		m_comp = p_comp;
		m_explosion = p_explosion;

		m_creationTime = Time.fixedTime;
	}

	public override void Execute()
	{
		m_comp.gameObject.GetComponent<PlayerController>().m_isAlive = false;
		m_comp.enabled = false;
		m_explosion.SetActive(true);
	}

	public override void Undo()
	{
		m_comp.gameObject.GetComponent<PlayerController>().m_isAlive = true;
		m_comp.enabled = true;
		m_explosion.SetActive(false);
	}
}


public class ToggleAnimation : CommandBase
{

	private Animator m_comp;
	private GameObject m_explosion;
	public ToggleAnimation()
	{
		m_creationTime = Time.fixedTime;
	}

	public ToggleAnimation(Animator p_comp, GameObject p_explosion)
	{
		m_comp = p_comp;
		m_explosion = p_explosion;

		m_creationTime = Time.fixedTime;
	}

	public override void Execute()
	{

	}

	public override void Undo()
	{
		m_comp.Play("Reverse"); //play backwards
	}
}
