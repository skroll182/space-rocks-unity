using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private Sprite[] sprites = new Sprite[3];
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _upperBound;
    private float _rightBound;
    private float _spriteWidth;
    private float _spriteHeight;
    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
        _velocity = new Vector3(speed*_moveDirection.x, speed*_moveDirection.y, 0);
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        Sprite spr = GetComponent<SpriteRenderer>().sprite;
        _spriteWidth = spr.texture.width / spr.pixelsPerUnit;
        _spriteHeight = spr.texture.height / spr.pixelsPerUnit;
        _upperBound = Camera.main.transform.position.y + Camera.main.orthographicSize;
        _rightBound = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        GetComponent<CircleCollider2D>().radius = _spriteWidth / 2.0f;
        switch (spr.name)
        {
            case "spr_asteroid_huge":
                this.name = "Huge asteroid";
                break;
            case "spr_asteroid_med":
                this.name = "Medium asteroid";
                break;
            case "spr_asteroid_small":
                this.name = "Small asteroid";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= _rightBound + _spriteWidth / 4)
        {
            transform.position = new Vector3(-_rightBound - _spriteWidth / 4, transform.position.y);
        }
        if (transform.position.x <= -_rightBound - _spriteWidth / 4)
        {
            transform.position = new Vector3(_rightBound + _spriteWidth / 4, transform.position.y);
        }
        if (transform.position.y >= _upperBound + _spriteHeight / 4)
        {
            transform.position = new Vector3(transform.position.x, -_upperBound - _spriteHeight / 4);
        }
        if (transform.position.y <= -_upperBound - _spriteHeight / 4)
        {
            transform.position = new Vector3(transform.position.x, _upperBound + _spriteHeight / 4);
        }
        transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
        transform.Translate(_velocity * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        switch (this.gameObject.name)
        {
            case "Huge Asteroid":
                for (int i = 0; i < 2; i++)
                {
                    GameObject newAsteroid = Instantiate(this.gameObject, this.transform.position, new Quaternion());
                    newAsteroid.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/spr_asteroid_med");
                }
                break;
            case "Medium Asteroid":
                for (int i = 0; i < 2; i++)
                {
                    GameObject newAsteroid = Instantiate(this.gameObject, this.transform.position, new Quaternion());
                    newAsteroid.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/spr_asteroid_small");
                }
                break;
        }
    }
}
