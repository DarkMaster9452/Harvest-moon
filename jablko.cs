using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jablko : MonoBehaviour
{
    public Transform treePosition;       
    public float spawnRadius = 2f;      

    
    void Start()
    {
        Vector3 spawnPosition = treePosition.position + new Vector3(Random.Range(-spawnRadius, spawnRadius),Random.Range(0.5f, 2f), 0 );

    }

  
    void Update()
    {
        
    }
}
