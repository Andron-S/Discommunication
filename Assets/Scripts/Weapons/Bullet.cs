using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 50;
    [SerializeField] private float _speed = 50;

    private float _flyTime = 0;

    public void FlyLive()
    {
        StartCoroutine(Fly());
        Destroy(gameObject);
    }

    private IEnumerator Fly()
    {
        while (Physics2D.RaycastAll(transform.position, transform.up).Length == 0 && _flyTime < 10)
        {
            transform.position += new Vector3(0, _speed, 0);
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
    }

    private void Attack(IDamageable liver)
    {
        liver.TakeDamage(_damage);
        Debug.Log("Damage is taken");
    }
}
