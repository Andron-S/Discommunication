using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO/Items", menuName = "SO/Create new DefaultItem")]
public class DefaultItemSO : ItemSO
{
    public override void OnCollected(IItemCollector collector)
    {
        Debug.Log($"{this} was callected by {collector}");
    }

    public override void OnDropped(IItemCollector collector)
    {
        Debug.Log($"{this} was droped by {collector}");
    }
}
