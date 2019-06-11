using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public int score;

    public int lives;

    public float upperBound;

    public float lowerBound;

    public float rightBound;

    public float leftBound;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Camera cam = Camera.main;
            upperBound = cam.transform.position.y + cam.orthographicSize;
            rightBound = cam.transform.position.x + cam.orthographicSize * cam.aspect;
            lowerBound = cam.transform.position.y - cam.orthographicSize;
            leftBound = cam.transform.position.x - cam.orthographicSize * cam.aspect;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
