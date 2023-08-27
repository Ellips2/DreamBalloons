using System.Collections.Generic;
using UnityEngine;

public class Pool <T> where T : MonoBehaviour
{
    private List<T> poolActive = new List<T>();
    private List<T> poolNonActive = new List<T>();
    private T prefab;
    private Transform parent;

    public Pool (T newPrefab, Transform newParent)
    {
        prefab = newPrefab;
        parent = newParent;
    }

    public T GetItem()
    {
        T newItem;
        if (poolNonActive.Count == 0)
        {
            newItem = CreateItem();
            poolActive.Add(newItem);
        }
        else
        {
            newItem = poolNonActive[0];
            newItem.gameObject.SetActive(true);
            poolActive.Add(newItem);
            poolNonActive.RemoveAt(0);
        }
        return newItem;
    }

    public void DisbaleItem(T poolObject)
    {
        poolActive.Remove(poolObject);
        poolNonActive.Add(poolObject);
    }

    public List<T> GetListActiveItem()
    {
        return poolActive;
    }

    private T CreateItem()
    { 
        return GameObject.Instantiate<T>(prefab, parent);
    }
}
