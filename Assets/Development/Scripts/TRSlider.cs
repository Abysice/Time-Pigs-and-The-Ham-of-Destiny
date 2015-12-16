using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TRSlider : MonoBehaviour {

    public Slider TRScroll;
    private Image img;
    private const float LERP_MULTIPLIER = 0.3f;

	// Use this for initialization
	void Start () {
        img = gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        
        img.fillAmount = Mathf.Lerp(img.fillAmount, TRScroll.value, LERP_MULTIPLIER); 
	}
}
