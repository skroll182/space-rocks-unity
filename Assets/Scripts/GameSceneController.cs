﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private GameObject[] Asteroids = new GameObject[3];
    [SerializeField] private GameObject debris;
    public static float upperBound;
    public static float rightBound;
    private static int _score = 0;
    private static int _lives = 999;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        upperBound = cam.transform.position.y + cam.orthographicSize;
        rightBound = cam.transform.position.x + cam.orthographicSize * cam.aspect;


        for (int i = 0; i < 14; i++)
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
            GameObject asteroid = Asteroids[Random.Range(0, Asteroids.Length)];
            Instantiate(asteroid, pos, new Quaternion());
            
        }
        StartCoroutine("SpawnNewAsteroid");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnGUI()
    {
        GameObject.Find("TextScore").GetComponent<Text>().text = "SCORE: " + _score;
        GameObject.Find("TextLives").GetComponent<Text>().text = "LIVES: " + _lives;
    }

    public void AsteroidDestroyed(GameObject original, Vector3 pos)
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(debris, pos, new Quaternion());
        }
        switch (original.GetComponent<SpriteRenderer>().sprite.name)
        {
            case "spr_asteroid_huge":
                _score += 10;
                for (int i = 0; i < 2; i++)
                {
                    
                    GameObject new_asteroid = Instantiate(Asteroids[1], pos, new Quaternion());
                }
                Debug.Log("Huge Destroyed");
                break;
            case "spr_asteroid_med":
                _score += 15;
                for (int i = 0; i < 2; i++)
                {
                    GameObject new_asteroid = Instantiate(Asteroids[2], pos, new Quaternion());
                }
                Debug.Log("Medium Destroyed");
                break;
            default:
                _score += 20;
                break;
        }
    }

    public void ShipDestroyed(Vector3 pos)
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(debris, pos, new Quaternion());
        }
        StartCoroutine("ShipDestroyedCoroutine");
    }

    private IEnumerator ShipDestroyedCoroutine()
    {
        _lives--;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnNewAsteroid()
    {
        for(; ; )
        {
            GameObject asteroid = Asteroids[Random.Range(0, Asteroids.Length - 1)];
            Vector3 pos;
            Camera cam = Camera.main;
            bool[] values = new bool[] { true, false };
            if (values[Random.Range(0, values.Length)])
            {
                float x = Random.Range(-rightBound, rightBound);
                float[] Y = new float[] {upperBound - asteroid.GetComponent<Asteroid>().SpriteHeight*2, -upperBound + asteroid.GetComponent<Asteroid>().SpriteHeight*2 };
                float y = Y[Random.Range(0, Y.Length)];
                pos = new Vector3(x, y, 1.0f);
            }
            else
            {
                float y = Random.Range(-upperBound, upperBound);
                float[] X = new float[] {rightBound - asteroid.GetComponent<Asteroid>().SpriteWidth*2, -rightBound + asteroid.GetComponent<Asteroid>().SpriteWidth*2 };
                float x = X[Random.Range(0, X.Length)];
                pos = new Vector3(x, y, 1.0f);
            }
            Instantiate(asteroid, pos, new Quaternion());
            Debug.Log("New asteroid spawned");
            yield return new WaitForSeconds(5.0f);
        }
        
    }
}
