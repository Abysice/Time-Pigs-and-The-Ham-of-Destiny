using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        gameObject.AddComponent<Managers>();
        DontDestroyOnLoad(this);
	}
	
}
