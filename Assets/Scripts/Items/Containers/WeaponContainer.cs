using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : ItemContainer, IItemCollector
{
    [SerializeField] private WeaponHand _weaponHand;
    [SerializeField] private Weapon _equipedWeapon;
    

    public GameObject GetCollectorObject()
    {
        return gameObject;
    }


    public bool TryAttack()
    {
        if(_equipedWeapon != null)
        {
            _equipedWeapon.Attack();
        }
        return true;
    }

    public void EquipItem(WeaponItemSO weaponItem)
    {
        var weapon = weaponItem.SpawnWeaponAtHand(_weaponHand.transform);
        if (_equipedWeapon != null)
        {
            Destroy(_equipedWeapon.gameObject);
            _equipedWeapon = null;
        }

        _equipedWeapon = weapon;
    }


    public void RemoveWeaponFromHand()
    {
        if(_equipedWeapon != null)
        {
            Destroy(_equipedWeapon.gameObject);
            _equipedWeapon = null;
        }
    }


    public void DropItem()
    {
        var droppingItem = _itemInventory.RemoveItem(0);
        if(droppingItem != null)
        {
            droppingItem.SpawnItemObjectAt(transform.position);
            droppingItem.OnDropped(this);
        }
    }


    public bool TryCollectItem(ItemSO itemData)
    {
        if(itemData is WeaponItemSO)
        {
            if (_itemInventory.TryAddItem(itemData))
            {
                itemData.OnCollected(this);
                return true;
            }
        }
        return false;
    }


    protected override void _OnInventotyChanged()
    {
        
    }
}
