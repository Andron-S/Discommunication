using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : PlayerControl
{
    [SerializeField] private float _speed;

    protected Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    protected Vector2 _direction;
    protected Vector2 _move;

    public override void Awake()
    {
        base.Awake();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        PlayerInput.Player.Move.performed += movementContext => GetDirection();
    }

    private void Start()
    {
        _speed = 7;
        _rigidbody2D.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        GetDirection();
        Move();
        Rotate();
    }

    private void GetDirection()
    {
        _direction = PlayerInput.Player.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        _move = new Vector2(_direction.x, _direction.y);
        float scaledMoveSpeed = _speed * Time.fixedDeltaTime;

        _rigidbody2D.MovePosition(_rigidbody2D.position + _move * scaledMoveSpeed);
    }

    private void Rotate()
    {
        if (_direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(-_direction.x, _direction.y) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}