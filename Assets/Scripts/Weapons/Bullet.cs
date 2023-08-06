using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 50;
    [SerializeField] private float _speed = 50;

    private float _flyTime = 0;
    private float _maxFlyTime;

    public void FlyLive()
    {
        _maxFlyTime = 10;
        StartCoroutine(Fly());
    }

    public void MeleeLive()
    {
        _speed = 0.3f;
        _maxFlyTime = 0.3f;
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        while (_flyTime < _maxFlyTime)
        {
            transform.position += transform.up * _speed * Time.deltaTime * -1;
            _flyTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
