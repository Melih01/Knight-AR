using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static float GetAngle(this Vector2 vector)
    {
        return Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
    }

    public static void DoForAll<T>(this IList<T> list, System.Action<T> action)
    {
        int listCount = list.Count;

        for (int i = 0; i < listCount; i++)
        {
            T listItem = list[i];
            action(listItem);
        }
    }
}