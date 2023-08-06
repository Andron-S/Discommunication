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
    [SerializeField] private float _activateDelay;

    private float _delayCounter;
    private bool _isActivateRecharged;
    private Ray _DebugRayAbility;


    public float Damage { get => _damage; set => _damage = value; }

    public static Action<float> OnDelayCounterChanged;

    private void Start()
    {
        _activatePoint.position = transform.position;
        _damage = 500;
        _range = 2f;
        _activateDelay = 4;
        _isActivateRecharged = true;
    }

    private void Update()
    {
        _DebugRayAbility = new Ray(_activatePoint.position, _activatePoint.right);
        Debug.DrawRay(_activatePoint.position, transform.right * _range, Color.red);
    }

    public int TryEat()
    {
        if (_isActivateRecharged == false)
        {
            return 0;
        }

        RaycastHit2D[] hittedArray = Physics2D.RaycastAll(_activatePoint.position, _activatePoint.right);

        foreach (var hitted in hittedArray)
        {
            if (hitted.collider.TryGetComponent(out Player player) == false)
            {
                if (hitted.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(Damage);
                    Debug.Log("Eat ability hited on Enemy");

                    var attackDelayCorutine = StartCoroutine(CalculatingAttackDelay());

                    return (int)Damage; // how get 
                }
            }
        }
        return 0;
    }

    public IEnumerator CalculatingAttackDelay()
    {
        RaiseDelayAbilityCounter();
        yield return new WaitForSeconds(_activateDelay);

        _isActivateRecharged = true;
    }

    private void RaiseDelayAbilityCounter()
    {
        _delayCounter -= Time.deltaTime; // to Reloading Viewer in UI
        OnDelayCounterChanged?.Invoke(_delayCounter);
    }
}