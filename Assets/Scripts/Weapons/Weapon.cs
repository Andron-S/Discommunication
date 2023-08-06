using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon: MonoBehaviour
{
    [SerializeField] private float _meleeDamage = 4;
    [SerializeField] private float _durable = 5;
    //[SerializeField] private float _beginRotation;
    [SerializeField] private DamagableItemSO _bulletTemplate;
    [SerializeField] private bool _isMeleeMode = false;

    private BulletBeginer _bulletBeginer;
    private SpriteRenderer _spriteRenderer;
    private Color _color;

    public void Start()
    {
        _bulletBeginer = GetComponentInChildren<BulletBeginer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
    }

    public void Attack()
    {
        if (_durable > 0)
        {
            if (_isMeleeMode)
            {
                MeleeAttack();
            }
            else
            {
                DistantAttack();
            }

            _durable--;
        }

        if (_durable == 0)
        {
            Destroy(gameObject);
        }
    }

    private void MeleeAttack()
    {
        var createdBullet = _bulletTemplate.SpawnItemObjectAt(_bulletBeginer.transform.position);
        createdBullet.transform.up = _bulletBeginer.transform.right;
        _spriteRenderer.color = new Color (_color.r, _color.g, _color.b, 0);
        StartCoroutine(BulletLive(createdBullet));
        createdBullet.GetComponent<Bullet>().MeleeLive();
    }

    private void DistantAttack()
    {
        var createdBullet = _bulletTemplate.SpawnItemObjectAt(_bulletBeginer.transform.position);
        createdBullet.transform.up = _bulletBeginer.transform.up;
        createdBullet.GetComponent<Bullet>().FlyLive();
    }

    private IEnumerator BulletLive(Item item)
    {
        while (item != null)
        {
            yield return null;
        }

        _spriteRenderer.color = _color;
    }
}
