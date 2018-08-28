using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolInfoAsset", menuName = "Knight AR/ObjectPool/Object Pool Info Asset")]
public class ObjectPoolInfoAsset : ScriptableObject
{
    [Space]
    [Reorderable]
    public List<DamagePopupPrefabInfo> damagePopupPrefabInfos = new List<DamagePopupPrefabInfo>(2);

    [System.Serializable]
    public class DamagePopupPrefabInfo : ISerializationCallbackReceiver
    {
        public string name;
        public GameObject prefab;
        public float poolObjectCount = 10;

        public void OnAfterDeserialize()
        {

        }

        public void OnBeforeSerialize()
        {
            name = prefab.name;
        }
    }
}

public class ReorderableAttribute : PropertyAttribute
{
    public bool showAdd = true;
    public bool showDelete = true;
    public bool showOrder = true;
    public bool showBox = true;
}
