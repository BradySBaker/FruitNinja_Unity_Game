using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit_Spawner : MonoBehaviour
{
    public GameObject fruitToSpawn;
    public float minWait = .3f;
    public float maxWait = 1f;
    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    private IEnumerator SpawnFruit() {
        while (true) {
            

            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

/*            Destroy(fruit, 5);*/
        }
    }
}
