using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    private const string gameSceneName = "GameScene";
    [SerializeField] private GameObject[] Asteroids = new GameObject[3];
    public static float upperBound;
    public static float rightBound;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        upperBound = cam.transform.position.y + cam.orthographicSize;
        rightBound = cam.transform.position.x + cam.orthographicSize * cam.aspect;


        for (int i = 0; i < 25; i++)
        {
            
            float x = Random.Range(cam.transform.position.x - cam.orthographicSize * cam.aspect,
                cam.transform.position.x + cam.orthographicSize * cam.aspect);
            float y = Random.Range(cam.transform.position.y - cam.orthographicSize,
                cam.transform.position.y + cam.orthographicSize);
            float z = 200.0f;

            Vector3 pos = new Vector3(x, y, z);
            GameObject asteroid = Asteroids[Random.Range(0, Asteroids.Length)];
            Instantiate(asteroid, pos, new Quaternion());
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGameScene()
    {
        
        SceneManager.LoadScene(gameSceneName);
        
    }

    public void Exit()
    {
        Application.Quit();
        
    }
}
