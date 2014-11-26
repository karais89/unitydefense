using UnityEngine;
using System.Collections;

public class PerformenceTest : MonoBehaviour {
    GameObject spherePrefab;
    private System.Random rand = new System.Random();
    void Awake()
    {
        spherePrefab = (GameObject)Resources.Load("Prefabs/Sphere", typeof(GameObject));

        StartCoroutine(CreateSphere(0.1f));
    }

    IEnumerator CreateSphere( float time)
    {
        while ( true )
        {
            yield return new WaitForSeconds(time);

            GameObject sphere = (GameObject) Instantiate(spherePrefab, new Vector3(0, 4, 0), Quaternion.identity);
            
            Color newColor = Color.white;
            int randomR = rand.Next(0, 9);
            int randomG = rand.Next(0, 9);
            int randomB = rand.Next(0, 9);
            newColor.r = randomR * 0.1f;
            newColor.g = randomG * 0.1f;
            newColor.b = randomB * 0.1f;
            sphere.renderer.material.color = newColor;
            
            //sphere.renderer.material.color = Color.red;
        }
        

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
