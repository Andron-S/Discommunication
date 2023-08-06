using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : PlayerControl
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Vector2 _direction;
    private Vector2 _move;
    private Vector2 _mousePosition;

    public static event Action<Vector2> OnRotated;

    public override void Awake()
    {
        base.Awake();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        Input.Player.Move.performed += movementContext => GetDirection();
        Input.Player.MousePosition.performed += mousePositionConytext => OnMousePosition();
    }

    private void Start()
    {
        _speed = 7;
        _rigidbody2D.gravityScale = 0;

        OnRotated?.Invoke(_mousePosition);
    }

    private void FixedUpdate()
    {
        GetDirection();
        Move();
        FaceOnMousePointer();
        //Rotate();
    }

    public float Speed { get => _speed; set => _speed = value; }

    private void GetDirection()
    {
        _direction = Input.Player.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        var newDirection = new Vector2(_direction.x, _direction.y);
        _move = newDirection;

        float scaledMoveSpeed = _speed * Time.fixedDeltaTime;

        _rigidbody2D.MovePosition(_rigidbody2D.position + _move * scaledMoveSpeed);
    }

    private void FaceOnMousePointer()
    {
        Vector2 facindDirection = _mousePosition - _rigidbody2D.position;
        float angle = Mathf.Atan2(-facindDirection.x, facindDirection.y) * Mathf.Rad2Deg;
        _rigidbody2D.MoveRotation(angle);

        if(_rigidbody2D.rotation != angle)
        {
            OnRotated?.Invoke(facindDirection.normalized);
        }
    }

    private void Rotate()
    {
        if (_direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(-_direction.x, _direction.y) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnMousePosition()
    {
        _mousePosition = _camera.ScreenToWorldPoint(Input.Player.MousePosition.ReadValue<Vector2>());
    }
}