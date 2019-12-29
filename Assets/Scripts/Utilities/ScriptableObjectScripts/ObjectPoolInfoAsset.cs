using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolInfoAsset", menuName = "Knight AR/Object Pool/Object Pool Info Asset")]
public class ObjectPoolInfoAsset : ScriptableObject
{
    [Space]
    [Reorderable]
    public List<ObjectPoolInfo> objectPoolInfos;
}

public class ReorderableAttribute : PropertyAttribute
{
    public bool showAdd = true;
    public bool showDelete = true;
    public bool showOrder = true;
    public bool showBox = true;
}
