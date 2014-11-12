using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {
    private static ResourceManager resourceManager;

    void Awake()
    {
        resourceManager = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject Load( string prefabName )
    {
        GameObject obj = null;

        obj = (GameObject) Resources.Load("Prefabs/"+prefabName);

        return obj;
    }
}
