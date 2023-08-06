using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Collectable,
    Consuming
}


[ExecuteAlways]
public abstract class ItemSO : ScriptableObject
{
    public string ItemID { get => _itemID; }
    public Sprite ItemSprite { get => _itemSpriteSettings.sprite; }
    public Vector2 ItemSpriteScale { get => _itemSpriteSettings.spriteScale; }
    public ItemType ItemType { get => _itemType; }

    [SerializeField] private string _itemID;
    [SerializeField] private SpriteSettings _itemSpriteSettings;
    [ContextMenuItem("Spawn Object", "SpawnItemObject")]
    [SerializeField] private GameObject _clearItemBase;
    [SerializeField] private ItemType _itemType = ItemType.Collectable;

    //что бы не писать отдельный эдитор для работы но можно было работать прямо из него 
#if UNITY_EDITOR
    public void CheckItemBase()
    {
        Item item;
        if(_clearItemBase.TryGetComponent(out item))
        {

        }
        else
        {
            Debug.LogError($"{this}>>>_clearItemBase does not contains Item Component");
        }
    }
#endif

    [ContextMenu("Spawn Item Object")]
    public Item SpawnItemObject()
    {
        return SpawnItemObjectAt(Vector2.zero);
    }

    public Item SpawnItemObjectAt(Vector2 position)
    {
        var itemInstance = Instantiate(_clearItemBase, position, Quaternion.identity);
        var item = itemInstance.GetComponent<Item>();
        item.LoadItemSO(this);
        return item;
    }

    public abstract void OnCollected(IItemCollector collector);
    public abstract void OnDropped(IItemCollector collector);
}


[System.Serializable]
public class SpriteSettings
{
    public Sprite sprite;
    public Vector2 spriteScale = new Vector2(1, 1);
}