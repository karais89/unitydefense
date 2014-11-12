using UnityEngine;
using System.Collections;
using Common;

public class Spawn : MonoBehaviour {

    public Point point;

    void Awake()
    {
        Color spawnColor = Color.blue;
        spawnColor.a = 0.5f;
        renderer.material.color = spawnColor;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
