using UnityEngine;
using System.Collections;

public class Performance : MonoBehaviour {

    private LightShadows shadowType = LightShadows.None;

    void Awake()
    {
        shadowType = LightShadows.Hard;

        GameObject.Find("Directional light").GetComponent<Light>().shadows = shadowType;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
