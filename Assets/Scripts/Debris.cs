using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private float _disappearingSpeed;
    private Vector3 _moveDirection;
    private Vector3 _velocity;

    private Color _color;
    // Start is called before the first frame update
    void Start()
    {
        _disappearingSpeed = Random.Range(0.01f, 0.05f);
        _moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
        _velocity = new Vector3(speed*_moveDirection.x, speed*_moveDirection.y, 0);
        _color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        _color.a -= _disappearingSpeed;
        transform.Translate(_velocity * Time.deltaTime, Space.World);
        if (_color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
