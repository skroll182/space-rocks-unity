using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroids = new GameObject[3];
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

    
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}

