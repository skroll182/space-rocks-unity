using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 3.0f;

    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * transform.right.x, speed * transform.right.y) * Time.deltaTime, Space.World);
        if (transform.position.x >= GlobalControl.Instance.rightBound || transform.position.x <= GlobalControl.Instance.leftBound || transform.position.y >= GlobalControl.Instance.upperBound || transform.position.y <= GlobalControl.Instance.lowerBound)
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
