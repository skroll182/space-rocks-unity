using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ship : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 225.0f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private GameObject bullet;
    private Vector3 _velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private float _upperBound;
    private float _rightBound;
    private float _spriteWidth;
    private float _spriteHeight;
    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.Rotate(new Vector3(0, 0, 1), rotationSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, 1), -rotationSpeed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float x = transform.right.x;
            float y = transform.right.y;
            float velocityX = _velocity.x + acceleration * x * Time.deltaTime;
            float velocityY = _velocity.y + acceleration * y * Time.deltaTime;
            _velocity.Set(Mathf.Clamp(velocityX, -2, 2), Mathf.Clamp(velocityY, -2, 2), 0);
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject new_bullet = Instantiate(bullet, transform.position, new Quaternion());
            new_bullet.transform.rotation = transform.rotation;
        }
        if(transform.position.x >= _rightBound + _spriteWidth / 4)
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
        
        transform.Translate(_velocity * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
