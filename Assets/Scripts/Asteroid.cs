using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float rotationSpeed = 50.0f;
 
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _spriteWidth;
    private float _spriteHeight;

    public float SpriteWidth
    {
        get { return _spriteWidth; }
    }

    public float SpriteHeight
    {
        get { return _spriteHeight; }
    }
    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
        _velocity = new Vector3(speed*_moveDirection.x, speed*_moveDirection.y, 0);
        _spriteWidth = GetComponent<SpriteRenderer>().sprite.texture.width / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        _spriteHeight = GetComponent<SpriteRenderer>().sprite.texture.height / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= GameSceneController.rightBound + _spriteWidth / 4)
        {
            transform.position = new Vector3(-GameSceneController.rightBound - _spriteWidth / 4, transform.position.y);
        }
        if (transform.position.x <= -GameSceneController.rightBound - _spriteWidth / 4)
        {
            transform.position = new Vector3(GameSceneController.rightBound + _spriteWidth / 4, transform.position.y);
        }
        if (transform.position.y >= GameSceneController.upperBound + _spriteHeight / 4)
        {
            transform.position = new Vector3(transform.position.x, -GameSceneController.upperBound - _spriteHeight / 4);
        }
        if (transform.position.y <= -GameSceneController.upperBound - _spriteHeight / 4)
        {
            transform.position = new Vector3(transform.position.x, GameSceneController.upperBound + _spriteHeight / 4);
        }
        transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
        transform.Translate(_velocity * Time.deltaTime, Space.World);
    }

    

    

    

}
