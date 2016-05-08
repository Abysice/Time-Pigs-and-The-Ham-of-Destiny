// Keeps the character moving when it should
//
//

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	#region Public Variables
	private GameObject m_camera;
	public bool m_isAlive;
	private const float MOVE_SPEED = 0.075f;
	private GameObject m_explosion;

	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private InputManager m_inp;
	private CommandManager m_cmanager;
    private BulletPool m_bulletPool;
	private float m_moveTimer;
	private SpriteRenderer m_sprite;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_inp = Managers.GetInstance().GetInputManager();
		m_cmanager = Managers.GetInstance().GetCommandManager();
        m_bulletPool = Managers.GetInstance().GetBulletPoolManager().GetBulletPool(20, Managers.GetInstance().GetGameProperties().playerBulletPrefab);
		m_moveTimer = m_cmanager.GetTimer();
		m_isAlive = true;
		m_explosion = transform.GetChild(0).gameObject;
		m_sprite = GetComponent<SpriteRenderer>();

	}
	//runs every frame
	public void Update()
	{
		if (m_camera == null)
			m_camera = Managers.GetInstance().GetLoadManager().cameraObject;

		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			if (m_isAlive == false)
			{
				m_explosion.GetComponent<Animator>().speed = 1;
				if (m_cmanager.GetTimeState())
				{
					m_explosion.GetComponent<Animator>().Play("Reverse"); //play backwards
				}
					
				return;
			}
			if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
			{
				return;
			}

			//PlaceHolder Code
			Vector3 m_inputVec = Vector3.zero;
			if (Input.GetKey(KeyCode.W))
			{
				m_inputVec += (Vector3.up * MOVE_SPEED);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				m_inputVec += (Vector3.down * MOVE_SPEED);
			}
			if (Input.GetKey(KeyCode.D))
			{
				m_inputVec += (Vector3.right * MOVE_SPEED);
			}
			if (Input.GetKey(KeyCode.A))
			{
				m_inputVec += (Vector3.left * MOVE_SPEED);
			}
			

			//CONTROLLER CONTROLS
			//Vector3 m_inputVec = Vector3.zero;
			//m_inputVec.x = m_inp.p1_LStickX;
			//m_inputVec.y = m_inp.p1_LStickY;

			//m_inputVec *= MOVE_SPEED;

			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
			if (hit)
			{
				float distance = Mathf.Abs(hit.point.y - transform.position.y);
				if (distance <= 0.1f && m_inputVec.y <= 0)
					m_inputVec.y = 0;
			}
			hit = Physics2D.Raycast(transform.position, Vector2.up);
			if (hit)
			{
				float distance = Mathf.Abs(hit.point.y - transform.position.y);
				if (distance <= 0.1f && m_inputVec.y >= 0)
					m_inputVec.y = 0;
			}
			hit = Physics2D.Raycast(transform.position, Vector2.left);
			if (hit)
			{
				float distance = Mathf.Abs(hit.point.x - transform.position.x);
				if (distance <= 0.1f && m_inputVec.x <= 0)
					m_inputVec.x = 0;
			}
			hit = Physics2D.Raycast(transform.position, Vector2.right);
			if (hit)
			{
				float distance = Mathf.Abs(hit.point.x - transform.position.x);
				if (distance <= 0.1f && m_inputVec.x >= 0)
					m_inputVec.x = 0;
			} 
	
			m_cmanager.AddMoveCommand(gameObject, gameObject.transform.localPosition + m_inputVec);

			//Shoot();
			ShootKeyBoard();

		
		}
	}

	public void OnCollisionStay2D(Collision2D collisionInfo)
	{
		
		if (m_isAlive == false || collisionInfo.gameObject.CompareTag("p_bullet")) //if I'm already dead
			return;

		Debug.Log("HIT");
		
		m_cmanager.AddPlayerDeathCommand(m_sprite, m_explosion);
		

	}


	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	public void Shoot()
	{
		if (m_moveTimer > 0.0f) // do nothing this frame
		{
			m_moveTimer -= Time.deltaTime;
			return;
		}
		else if (m_moveTimer <= 0.0f)
		{
			m_moveTimer = 0.1f;
		}

		if (m_inp.p1_ButtonA == 0)
		{
			m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, 0.2f));
		}
	}

	public void ShootKeyBoard()
	{
		if (m_moveTimer > 0.0f) // do nothing this frame
		{
			m_moveTimer -= Time.deltaTime;
			return;
		}
		else if (m_moveTimer <= 0.0f)
		{
			m_moveTimer = 0.1f;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			m_cmanager.AddSpawnBulletCommand(m_bulletPool, transform.position, new Vector2(0, 0.2f));
		}
	}
	#endregion
}
