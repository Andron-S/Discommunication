using System.Collections;

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour
{
    //[SerializeField] private float _meleeDamage = 4;
    [SerializeField] private float _durable = 0.5f;
    //[SerializeField] private float _beginRotation;
    [SerializeField] private ItemSO _bulletTemplate;
    [SerializeField] private bool _isMeleeMode = false;

    private BulletBeginer _bulletBeginer;
    private SpriteRenderer _spriteRenderer;
    private Color _color;
    private bool _isCanAttack;
    private float _durableTime;
    private Coroutine _inJob;

    public void Start()
    {
        _bulletBeginer = GetComponentInChildren<BulletBeginer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        _isCanAttack = true;
        _durableTime = 0;
    }

    //public void SetDurable(float durable)
    //{
    //    _durable = durable;
    //}

    public void Attack()
    {
        if (_isCanAttack)
        {
            StopAttack();

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
        }

        if (_durable == 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void MeleeAttack()
    {
        _inJob = StartCoroutine(StopAttack());
        var createdBullet = _bulletTemplate.SpawnItemObjectAt(_bulletBeginer.transform.position);
        createdBullet.transform.up = _bulletBeginer.transform.right;
        _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, 0);
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

    private IEnumerator StopAttack()
    {
        _isCanAttack = false;

        for (_durableTime = 0; _durableTime < _durable; _durableTime += Time.deltaTime)
        {
            yield return null;
        }

        _isCanAttack = true;
    }
}
