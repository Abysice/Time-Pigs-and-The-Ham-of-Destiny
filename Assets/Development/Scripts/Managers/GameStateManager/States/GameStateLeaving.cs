using UnityEngine;
using System.Collections;

public class GameStateLeaving : GameStateBase
{
	public GameStateLeaving(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{

	}

	public override void UpdateState()
	{

	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}