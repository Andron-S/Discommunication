using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public string ItemID { get => _itemSO.ItemID; }

    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private ItemSO _itemSO = null;
    [SerializeField] private bool _itemIsLoaded = false;
    [Space]
    [SerializeField] private float _colliderDisablTimeAfterDrop = 1f;
    [SerializeField] private UnityEvent _onDroped;
    [SerializeField] private UnityEvent _onCollected;
    [SerializeField] private UnityEvent _onNotCollected;
    [SerializeField] private bool _destroyOnCollect = true;

    private void Awake()
    {
        StartCoroutine(DisableColliderFor(_colliderDisablTimeAfterDrop));
    }

    private void Start()
    {
        if (_itemSO != null && !_itemIsLoaded)
        {
            LoadItemSO(_itemSO);
        }
    }


    //что бы не писать отдельный эдитор для работы но можно было работать прямо из него 
#if UNITY_EDITOR
    [Space]
    [SerializeField] private bool _updateData = false;
    private void Update()
    {
        if (_updateData)
        {
            if (_itemSO != null)
            {
                _UpdateItemData();
            }
            _updateData = false;
        }

        if(_itemSO != null)
        {
            _itemSO.CheckItemBase();
        }
    }
#endif
    public void OnDropped()
    {
        _onDroped.Invoke();
        StartCoroutine(DisableColliderFor(_colliderDisablTimeAfterDrop));
    }

    public void LoadItemSO(ItemSO itemSO)
    {
        Debug.Log($"{this}>>>loading ItemSO {itemSO.ItemID}");
        if (_itemSO != null)
        {
            Debug.LogWarning($"{this}>>>data is already loaded : {_itemSO}");
            return;
        }

        _itemSO = itemSO;
        _UpdateItemData();
        _itemIsLoaded = true;

        Debug.Log($"{this}>>>data is loaded");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItemCollector itemCollector;
        if (collision.TryGetComponent(out itemCollector))
        {
            transform.position = collision.transform.position;
            _CollectItem(itemCollector);
        }
    }


    private void _UpdateItemData()
    {
        _LoadSprite();
    }


    private void _CollectItem(IItemCollector collector)
    {
        
        var collectedSuccessful = collector.TryCollectItem(_itemSO);
        if (collectedSuccessful)
        {
            _onCollected.Invoke();
            if (_destroyOnCollect)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            _onNotCollected.Invoke();
        }
    }


    private void _LoadSprite()
    {
        _spriteRenderer.sprite = _itemSO.ItemSprite;
        _spriteRenderer.transform.localScale = _itemSO.ItemSpriteScale;
    }


    private IEnumerator DisableColliderFor(float seconds)
    {
        var collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(seconds);
        collider.enabled = true;
    }
    
}
