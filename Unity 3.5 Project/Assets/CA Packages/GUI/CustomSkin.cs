using UnityEngine;
using System.Collections;

public class CustomSkin : MonoBehaviour {

    public GUISkin Skin;

    // Use this for initialization
    void Start()
    {

    }
    
	// Update is called once per frame
    void Update()
    {

    }

	// Use this for initialization
	void OnGUI () 
    {
        GUI.skin = Skin;
        GUI.Label(new Rect(10, 10, 300, 20), "Build No : 0.00127ab");
	}
}
