using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshController : CustomMonoBehaviour
{
    NavMeshSurface navMeshSurface;

    void OnEnable()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        //navMeshSurface.BuildNavMesh();
    }
}
