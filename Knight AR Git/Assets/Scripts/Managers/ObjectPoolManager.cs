using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPool
{
    DamagePopup = 0
}

public class ObjectPoolManager : CustomMonoBehaviour
{
    [Space]
    [SerializeField]
    ObjectPoolInfoAsset objectPoolInfoAsset;
    [Space]
    public List<GameObject> damagePopupObjectPoolList;

    void Awake()
    {
        objectPoolInfoAsset.damagePopupPrefabInfos.DoForAll((damagePopupController) =>
        {
            for (int i = 0; i < damagePopupController.poolObjectCount; i++)
            {
                damagePopupObjectPoolList.Add(Spawn(damagePopupController.prefab, transform));
                damagePopupObjectPoolList[i].SetActive(false);
            }
        });
    }

    public GameObject Spawn(GameObject prefab, Transform parent)
    {
        return Instantiate(prefab, parent);
    }

    public void Spawn(ObjectPool objectPool, Transform parent,float damage = 0)
    {
        switch (objectPool)
        {
            case ObjectPool.DamagePopup:
                var obj = damagePopupObjectPoolList.ObjectPoolSpawn(parent,active: false);
                var damagePopupController = obj.GetComponent<DamagePopupController>();
                damagePopupController.damage = damage;
                obj.SetActive(true);
                break;
            default:
                break;
        }
    }
}

public static class ObjectPoolManagerExtensions
{
    public static GameObject ObjectPoolSpawn(this IList<GameObject> list, Transform parent, bool active = true)
    {
        var obj = list[0];
        list.RemoveAt(0);
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(active);

        return obj;
    }

    public static void ObjectPoolReturn(this IList<GameObject> list, GameObject prefab)
    {
        prefab.transform.SetParent(GameManager.instance.objectPoolManager.transform);
        list.Add(prefab);
    }
}