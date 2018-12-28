using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scaleCharacter : MonoBehaviour {

    public Slider sliderVal;

	// Use this for initialization
	void Start () {
        sliderVal.value = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(sliderVal.value/2 , sliderVal.value/2 , sliderVal.value/2 );
	}
}
