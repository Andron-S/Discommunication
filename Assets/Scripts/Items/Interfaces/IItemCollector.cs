using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemCollector
{
    bool TryCollectItem(ItemSO itemData);
    GameObject GetCollectorObject();
}
