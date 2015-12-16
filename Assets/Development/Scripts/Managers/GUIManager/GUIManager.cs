// Script Comments go here
//
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {


	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private GameObject m_canvasObject;
	private GameObject m_scrollbarObject;
	private Slider m_scrollbar;
	private Image m_fill;
	private CommandManager m_cmanager;
	private bool m_lerping; //are we lerping?
	private float m_lerpTarget; //target lerping towards
	private bool m_init = false;
	#endregion

	#region Accessors
	public float ScrollBarValue()
	{
		return m_scrollbar.value;
	}
	#endregion

	#region Unity Defaults
	public void Awake()
	{

	}

	//initialization
	public void Start()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
		m_lerping = false;
	}
	//runs every frame
	public void Update()
	{
		if (!m_init)
			return;

		if(m_lerping)
		{
			//lerp the slider to lerp target
			m_cmanager.MovetoAction(m_fill.fillAmount);
			if (Mathf.Abs(m_scrollbar.value - m_fill.fillAmount) < 0.05f)
			{
				m_lerping = false;
			}
		}
	}
	#endregion

	#region Public Methods
	//Spawn the ingame GUI
	public void LoadGameGUI()
	{
		m_canvasObject = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().slidebarPrefab);
		m_scrollbarObject = m_canvasObject.transform.GetChild(0).gameObject;
		m_scrollbar = m_scrollbarObject.GetComponent<Slider>();
		m_fill = m_scrollbar.transform.GetChild(3).GetComponent<Image>();
		m_init = true;
	}

	//Reset the scrollbar to 1 (the far right)
	public void ResetScrollBar()
	{
		m_scrollbar.value = 1.0f;
		m_lerping = false;
	}

	//move slider towards the new position
	public void LerpSlider()
	{
		m_lerping = true;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
