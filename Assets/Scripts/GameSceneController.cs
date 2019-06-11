using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroids = new GameObject[3];
    [SerializeField] private GameObject debris;
    private AudioClip asteroidDestroySound;
    private AudioClip shipDestroySound;
    
    // Start is called before the first frame update
    void Start()
    {
        asteroidDestroySound = Resources.Load<AudioClip>("Audio/snd_hurt");
        shipDestroySound = Resources.Load<AudioClip>("Audio/snd_die");
        Camera cam = Camera.main;
        
        for (int i = 0; i < 14; i++)
        {
            // Generate new asteroid x position in range of 30% of screen width (both left and right sides)
            float[] X = new float[] {
                Random.Range(
                    GlobalControl.Instance.leftBound,
                    GlobalControl.Instance.leftBound + cam.orthographicSize * cam.aspect * 2.0f * 0.3f
                ),
                Random.Range(
                    GlobalControl.Instance.rightBound - cam.orthographicSize * cam.aspect * 2.0f * 0.3f,
                    GlobalControl.Instance.rightBound
                )
            };
            
            // Generate new asteroid y position in range of 30% of screen height (both upper and lower sides)
            float[] Y = new float[] {
                Random.Range(
                    GlobalControl.Instance.lowerBound,
                    GlobalControl.Instance.lowerBound + cam.orthographicSize * 2.0f * 0.3f
                ),
                Random.Range(
                    GlobalControl.Instance.upperBound - cam.orthographicSize * 2.0f * 0.3f,
                    GlobalControl.Instance.upperBound
                )
            };

            float x = X[Random.Range(0, X.Length)];
            float y = Y[Random.Range(0, Y.Length)];
            float z = 1.0f;

            Vector3 pos = new Vector3(x, y, z);
            GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
            Instantiate(asteroid, pos, new Quaternion());
            
        }
        // Start spawning asteroids every 5 seconds
        StartCoroutine("SpawnNewAsteroid");
    }

    

    private void OnGUI()
    {
        GameObject.Find("TextScore").GetComponent<Text>().text = "SCORE: " + GlobalControl.Instance.score;
        GameObject.Find("TextLives").GetComponent<Text>().text = "LIVES: " + GlobalControl.Instance.lives;
    }

    public void AsteroidDestroyed(GameObject original, Vector3 pos)
    {
        GetComponent<AudioSource>().PlayOneShot(asteroidDestroySound);
        for (int i = 0; i < 20; i++)
        {
            Instantiate(debris, pos, new Quaternion());
        }
        switch (original.GetComponent<SpriteRenderer>().sprite.name)
        {
            case "spr_asteroid_huge":
                GlobalControl.Instance.score += 10;
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(asteroids[1], pos, new Quaternion());
                }
                break;
            case "spr_asteroid_med":
                GlobalControl.Instance.score += 15;
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(asteroids[2], pos, new Quaternion());
                }
                break;
            default:
                GlobalControl.Instance.score += 20;
                break;
        }

        GlobalControl.Instance.score = Mathf.Clamp(GlobalControl.Instance.score, 0, 1000);
        
        if (GlobalControl.Instance.score >= 1000)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    public void ShipDestroyed(Vector3 pos)
    {
        GetComponent<AudioSource>().PlayOneShot(shipDestroySound);
        for (int i = 0; i < 20; i++)
        {
            Instantiate(debris, pos, new Quaternion());
        }
        StartCoroutine("ShipDestroyedCoroutine");
    }

    private IEnumerator ShipDestroyedCoroutine()
    {
        GlobalControl.Instance.lives--;
        yield return new WaitForSeconds(2.0f);
        if (GlobalControl.Instance.lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    /*
     *    Coroutine for spawning new asteroids
     */
    IEnumerator SpawnNewAsteroid()
    {
        for(; ; )
        {
            GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
            Vector3 pos;
            
            bool[] values = new bool[] { true, false };
            if (values[Random.Range(0, values.Length)])
            {
                float x = Random.Range(GlobalControl.Instance.leftBound, GlobalControl.Instance.rightBound);
                float[] Y = new float[] {GlobalControl.Instance.lowerBound, GlobalControl.Instance.upperBound};
                float y = Y[Random.Range(0, Y.Length)];
                pos = new Vector3(x, y, 1.0f);
            }
            else
            {
                float y = Random.Range(GlobalControl.Instance.lowerBound, GlobalControl.Instance.upperBound);
                float[] X = new float[] {GlobalControl.Instance.leftBound, GlobalControl.Instance.rightBound};
                float x = X[Random.Range(0, X.Length)];
                pos = new Vector3(x, y, 1.0f);
            }

            Instantiate(asteroid, pos, new Quaternion());
            yield return new WaitForSeconds(5.0f);
        }
        
    }
}
