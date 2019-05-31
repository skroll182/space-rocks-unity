using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private GameObject AsteroidPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;

        

        for(int i = 0; i < 20; i++)
        {
            float[] X = new float[] {
                Random.Range(
                    cam.transform.position.x - cam.orthographicSize * cam.aspect,
                    cam.transform.position.x - cam.orthographicSize * cam.aspect + cam.orthographicSize * cam.aspect * 2.0f * 0.3f
                ),
                Random.Range(
                    cam.transform.position.x + cam.orthographicSize * cam.aspect - cam.orthographicSize * cam.aspect * 2.0f * 0.3f,
                    cam.transform.position.x + cam.orthographicSize * cam.aspect
                )
            };

            float[] Y = new float[] {
                Random.Range(
                    cam.transform.position.y - cam.orthographicSize,
                    cam.transform.position.y - cam.orthographicSize + cam.orthographicSize * 2.0f * 0.3f
                ),
                Random.Range(
                    cam.transform.position.y + cam.orthographicSize - cam.orthographicSize * 2.0f * 0.3f,
                    cam.transform.position.y + cam.orthographicSize
                )
            };

            float x = X[Random.Range(0, X.Length)];
            float y = Y[Random.Range(0, Y.Length)];
            float z = 1.0f;

            Vector3 pos = new Vector3(x, y, z);
            Instantiate(AsteroidPrefab, pos, new Quaternion(0,0,0,0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
