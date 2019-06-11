using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroids = new GameObject[3];
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            
            float x = Random.Range(GlobalControl.Instance.leftBound, GlobalControl.Instance.rightBound);
            float y = Random.Range(GlobalControl.Instance.lowerBound, GlobalControl.Instance.upperBound);
            float z = 200.0f;

            Vector3 pos = new Vector3(x, y, z);
            GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
            Instantiate(asteroid, pos, new Quaternion());
            
        }
    }
    

    public void GoToGameScene()
    {
        GlobalControl.Instance.score = 0;
        GlobalControl.Instance.lives = 3;
        SceneManager.LoadScene("GameScene");
        
    }

    public void Exit()
    {
        Application.Quit();
        
    }
}
