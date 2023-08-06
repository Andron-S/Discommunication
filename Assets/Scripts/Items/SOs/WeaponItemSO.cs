using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO/Items", menuName = "SO/Create new Weapon Item")]
public class WeaponItemSO : ItemSO
{

    public Weapon SpawnWeaponAtHand(Transform handTransform)
    {
        var newWeapon = SpawnItemObjectAt(handTransform.position);
        newWeapon.GetComponent<Collider2D>().enabled = false;
        newWeapon.transform.parent = handTransform;
        newWeapon.transform.rotation = handTransform.rotation;
        return newWeapon.GetComponent<Weapon>();
    }


    public override void OnCollected(IItemCollector collector)
    {
        WeaponContainer weaponContainer;

        if (collector.GetCollectorObject().TryGetComponent(out weaponContainer))
        {
            weaponContainer.EquipItem(this);
        }
    }

    public override void OnDropped(IItemCollector collector)
    {
        WeaponContainer weaponContainer;

        if (collector.GetCollectorObject().TryGetComponent(out weaponContainer))
        {
            weaponContainer.RemoveWeaponFromHand();
        }
    }
}
