using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;         
    public float spawnInterval = 3f;   
    [SerializeField]public bool spawnute = false;
    public float timer;


    private void Start()
    {
        Spawn();
    }
    private void Update()
    {
        if (!spawnute)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                Spawn();
                timer = 0f;
            }
        }

    }

    /*
    private IEnumerator SpawnovanieCoroutine()
    {
        if (!spawnute)
        {

            yield return new WaitForSeconds(spawnInterval);

        }


    
    }
    */
    public void Spawn()
    {


            print("SPAWN");
            Instantiate(fruitPrefab);
            spawnute = true;

        
           

    }


}

