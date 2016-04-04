/**
 * @file PerformenceTest.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-11-26
 */

using System.Collections;
using UnityEngine;

namespace Test
{
    public class PerformenceTest : MonoBehaviour
    {
        private GameObject spherePrefab;
        private System.Random rand = new System.Random();

        private void Awake()
        {
            spherePrefab = (GameObject) Resources.Load( "Prefabs/Sphere", typeof( GameObject ) );

            StartCoroutine( CreateSphere( 0.1f ) );
        }

        private IEnumerator CreateSphere( float time )
        {
            while ( true )
            {
                yield return new WaitForSeconds( time );

                GameObject sphere = (GameObject) Instantiate( spherePrefab, new Vector3( 0, 4, 0 ), Quaternion.identity );

                Color newColor = Color.white;
                int randomR = rand.Next( 0, 9 );
                int randomG = rand.Next( 0, 9 );
                int randomB = rand.Next( 0, 9 );
                newColor.r = randomR * 0.1f;
                newColor.g = randomG * 0.1f;
                newColor.b = randomB * 0.1f;
                sphere.GetComponent<Renderer>().material.color = newColor;

                //sphere.renderer.material.color = Color.red;
            }
        }
    }
}


