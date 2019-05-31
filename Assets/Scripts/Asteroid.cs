using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 0.5f;
    [SerializeField] private float minSpeed = -0.5f;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private Sprite[] sprites = new Sprite[3];
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    private float _upperBound;
    private float _rightBound;
    private float _spriteWidth;
    private float _spriteHeight;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _moveDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        _rigidbody2D.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
        Sprite spr = GetComponent<SpriteRenderer>().sprite;
        _spriteWidth = spr.texture.width / spr.pixelsPerUnit;
        _spriteHeight = spr.texture.height / spr.pixelsPerUnit;
        _upperBound = Camera.main.transform.position.y + Camera.main.orthographicSize;
        _rightBound = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        GetComponent<CircleCollider2D>().radius = _spriteWidth / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody2D.transform.position.x >= _rightBound + _spriteWidth / 4)
        {
            _rigidbody2D.transform.position = new Vector3(-_rightBound - _spriteWidth / 4, _rigidbody2D.transform.position.y);
        }
        if (_rigidbody2D.transform.position.x <= -_rightBound - _spriteWidth / 4)
        {
            _rigidbody2D.transform.position = new Vector3(_rightBound + _spriteWidth / 4, _rigidbody2D.transform.position.y);
        }
        if (_rigidbody2D.transform.position.y >= _upperBound + _spriteHeight / 4)
        {
            _rigidbody2D.transform.position = new Vector3(_rigidbody2D.transform.position.x, -_upperBound - _spriteHeight / 4);
        }
        if (_rigidbody2D.transform.position.y <= -_upperBound - _spriteHeight / 4)
        {
            _rigidbody2D.transform.position = new Vector3(_rigidbody2D.transform.position.x, _upperBound + _spriteHeight / 4);
        }
        _rigidbody2D.transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
    }
}
