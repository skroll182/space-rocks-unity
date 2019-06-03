using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 3.0f;
    // Start is called befre the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * transform.right.x, speed * transform.right.y) * Time.deltaTime, Space.World);
        if (transform.position.x >= GameSceneController.rightBound || transform.position.x <= -GameSceneController.rightBound || transform.position.y >= GameSceneController.upperBound || transform.position.y <= -GameSceneController.upperBound)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        GameObject.Find("GameSceneController").GetComponent<GameSceneController>().AsteroidDestroyed(other, other.transform.position);
        Destroy(this.gameObject);
        Destroy(other);
        
    }
}
