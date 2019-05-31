using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Ship : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 225.0f;
    [SerializeField] private float speed = 30.0f;
    [SerializeField] private GameObject bullet;
    private Rigidbody2D _rigidbody2D;
    private float _upperBound;
    private float _rightBound;
    private float _spriteWidth;
    private float _spriteHeight;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _upperBound = Camera.main.transform.position.y + Camera.main.orthographicSize;
        _rightBound = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        Sprite spr = GetComponent<SpriteRenderer>().sprite;
        _spriteWidth = spr.texture.width / spr.pixelsPerUnit;
        _spriteHeight = spr.texture.height / spr.pixelsPerUnit;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody2D.transform.Rotate(new Vector3(0, 0, 1), rotationSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody2D.transform.Rotate(new Vector3(0, 0, 1), -rotationSpeed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody2D.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if(_rigidbody2D.transform.position.x >= _rightBound + _spriteWidth / 4)
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

        _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -5.0f, 5.0f), Mathf.Clamp(_rigidbody2D.velocity.y, -5.0f, 5.0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Object.Destroy(this.gameObject);
    }
}
