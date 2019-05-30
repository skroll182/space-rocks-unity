using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Ship : MonoBehaviour
{
    private const int _rotationSpeed = 200;
    private float _speed;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody2D.transform.Rotate(new Vector3(0, 0, 1), _rotationSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody2D.transform.Rotate(new Vector3(0, 0, 1), -_rotationSpeed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody2D.AddRelativeForce(Vector2.right * _speed * Time.deltaTime);
        }
    
    }
}
