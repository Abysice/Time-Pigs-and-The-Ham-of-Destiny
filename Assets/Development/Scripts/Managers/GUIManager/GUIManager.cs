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
    private Scrollbar m_scrollbar;

    private bool m_init = false;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    public void Awake()
    {

    }

    //initialization
    public void Start()
    {
        
    }
    //runs every frame
    public void Update()
    {
        if (!m_init)
            return;


    }
    #endregion

    #region Public Methods
    public void LoadGameGUI()
    {
        m_canvasObject = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().slidebarPrefab);
        m_scrollbarObject = m_canvasObject.transform.GetChild(0).gameObject;
        m_scrollbar = m_scrollbarObject.GetComponent<Scrollbar>();
        m_init = true;
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
