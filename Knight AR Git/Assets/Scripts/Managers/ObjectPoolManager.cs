using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPoolType
{
    DamagePopup = 0,
    BloodSprayEffect = 10
}

public class ObjectPoolManager : CustomMonoBehaviour
{
    [Space]
    public Transform objectPoolContainer;
    [Space]
    [SerializeField]
    ObjectPoolInfoAsset objectPoolInfoAsset;

    [Header("Just For Debug!")]
    public List<GameObject> damagePopupObjectPoolList;
    public List<GameObject> bloodSprayEffectObjectPoolList;

    void Awake()
    {
        objectPoolInfoAsset.objectPoolInfos.DoForAll((objectPoolInfo) =>
        {
            /// Objects Spawn in Object Pool Container.
            switch (objectPoolInfo.objectPoolType)
            {
                case ObjectPoolType.DamagePopup:
                    FillObjectPoolList(objectPoolInfo, ref damagePopupObjectPoolList, objectPoolInfo.count);
                    break;
                case ObjectPoolType.BloodSprayEffect:
                    FillObjectPoolList(objectPoolInfo, ref bloodSprayEffectObjectPoolList, objectPoolInfo.count);
                    break;
            }
        });
    }

    void FillObjectPoolList(ObjectPoolInfo objectPoolInfo, ref List<GameObject> objectPoolList, int count)
    {
        for (int i = 0; i < objectPoolInfo.count; i++)
        {
            objectPoolList.Add(Spawn(objectPoolInfo.prefab, objectPoolContainer));
            objectPoolList[i].SetActive(false);
        }
    }

    private GameObject Spawn(GameObject prefab, Transform parent)
    {
        return Instantiate(prefab, parent);
    }

    public void Spawn(ObjectPoolType objectPoolType, Transform parent, Vector3 localPosition, float damage = 0)
    {
        SpawnBase(objectPoolType, parent, localPosition, damage);
    }

    public void Spawn(ObjectPoolType objectPoolType, Transform parent, float damage = 0)
    {
        SpawnBase(objectPoolType, parent, Vector3.zero, damage);
    }

    public void Spawn(ObjectPoolType objectPoolType, Vector3 position, float damage = 0)
    {
        SpawnBase(objectPoolType, null, position, damage);
    }

    private void SpawnBase(ObjectPoolType objectPoolType, Transform parent, Vector3 position, float damage = 0)
    {
        switch (objectPoolType)
        {
            case ObjectPoolType.DamagePopup:
                var obj = damagePopupObjectPoolList.ObjectPoolSpawn(parent, active: false);
                var damagePopupController = obj.GetComponent<DamagePopupController>();
                damagePopupController.damage = damage;
                obj.transform.localPosition = Vector3.zero;
                obj.SetActive(true);
                break;
            case ObjectPoolType.BloodSprayEffect:
                bloodSprayEffectObjectPoolList.ObjectPoolSpawn(parent).GetComponent<ParticleSystem>().Play();
                break;
        }
    }
}

[System.Serializable]
public class ObjectPoolInfo : ISerializationCallbackReceiver
{
    [HideInInspector]
    public string name;
    public ObjectPoolType objectPoolType;
    public GameObject prefab;
    public int count;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        name = prefab.name;
    }
}

public static class ObjectPoolManagerExtensions
{
    static CustomMonoBehaviour customMonoBehaviour;

    public static GameObject ObjectPoolSpawn(this IList<GameObject> list, Transform parent, bool active = true)
    {
        var obj = list[0];
        list.RemoveAt(0);
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(active);

        return obj;
    }

    public static GameObject ObjectPoolSpawn(this IList<GameObject> list, Vector3 position, bool active = true)
    {
        var obj = list[0];
        list.RemoveAt(0);
        obj.transform.position = position;
        obj.SetActive(active);

        return obj;
    }

    public static GameObject ObjectPoolSpawn(this IList<GameObject> list, Transform parent,Vector3 localPosition, bool active = true)
    {
        var obj = list[0];
        list.RemoveAt(0);
        obj.transform.SetParent(parent);
        obj.transform.localPosition = localPosition;
        obj.SetActive(active);

        return obj;
    }

    public static void ObjectPoolReturn(this IList<GameObject> list, GameObject prefab)
    {
        customMonoBehaviour = GameManager.instance.GetComponent<CustomMonoBehaviour>();
        customMonoBehaviour.StartCoroutine(customMonoBehaviour.WaitForSecondsCoroutine(0.1f, action: () =>
            {
                prefab.transform.SetParent(GameManager.instance.objectPoolManager.objectPoolContainer);
                list.Add(prefab);
            }));
    }
}