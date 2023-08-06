using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 50;
    [SerializeField] private float _speed = 50;

    private float _flyTime = 0;

    private void Update()
    {
        Debug.Log(transform.position);
    }

    public void FlyLive()
    {
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        while (Physics2D.RaycastAll(transform.position, transform.up).Length == 0 && _flyTime < 10)
        {
            transform.position += transform.up * _speed * Time.deltaTime * -1;
            _flyTime += Time.deltaTime;
            yield return null;
        }

        if (Physics2D.RaycastAll(transform.position, transform.up).Length > 0)
        {
            if (Physics2D.RaycastAll(transform.position, transform.up)[0].collider.TryGetComponent<IDamageable>(out IDamageable liver))
            {
                Attack(liver);
            }
        }

        Destroy(gameObject);
    }

    private void Attack(IDamageable liver)
    {
        liver.TakeDamage(_damage);
    }
}
