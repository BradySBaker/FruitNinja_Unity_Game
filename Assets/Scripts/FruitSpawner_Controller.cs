using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit_Spawner : MonoBehaviour
{
    public Transform[] spawnPlaces;

    public GameObject[] fruitToSpawn;
    public float minWait = .3f;
    public float maxWait = 1f;
    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    private IEnumerator SpawnFruit() {
        while (true) {
            

            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            
            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject fruit = Instantiate(fruitToSpawn[Random.Range(0, fruitToSpawn.Length)], t.position, t.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(25, 35), ForceMode2D.Impulse);


            Destroy(fruit, 5);
        }
    }
}
