using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EatAbility : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private Transform _activatePoint;
    [SerializeField] private float _range;
    [SerializeField] private float _delay;

    private bool _isActivateRecharged;
    private Vector2 _currentPlayerDirection;
    private LayerMask _ignoredLayer;

    public float Damage { get => _damage; set => _damage = value; }

    public static Action<float> OnDelayCounterChanged;

    private void Start()
    {
        _activatePoint.position = transform.position;
        _damage = 500;
        _range = 2f;
        _delay = 1.5f;
        _isActivateRecharged = true;

        PlayerMovement.OnRotated += GetPlayerDirection;

        _ignoredLayer = gameObject.transform.parent.gameObject.layer;
    }

    public int TryEat()
    {
        if (_isActivateRecharged == false)
        {
            return 0;
        }

        Debug.Log("c1ir");
        RaycastHit2D[] hittedArray = Physics2D.RaycastAll(_activatePoint.position, _currentPlayerDirection * _range);

        foreach (var hitted in hittedArray)
        {
            if(hitted.collider.gameObject.layer != _ignoredLayer)
            {
                if (hitted.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(Damage);
                    return (int)Damage;
                }
            }
        }

        _isActivateRecharged = false;
        StartCoroutine(CalculatingAttackDelay());

        return 0;
    }

    public IEnumerator CalculatingAttackDelay()
    {
        //RaiseDelayAbilityCounter();
        yield return new WaitForSeconds(_delay);

        Debug.Log("cir");
        _isActivateRecharged = true;
    }

    public void GetPlayerDirection(Vector2 playerDirection)
    {
        _currentPlayerDirection = playerDirection;
    }

    //private void RaiseDelayAbilityCounter()
    //{
    //    _delayCounter -= Time.deltaTime; // to Reloading Viewer in UI
    //    OnDelayCounterChanged?.Invoke(_delayCounter);
    //}
}