using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food_Spawner : MonoBehaviour
{
    public Transform[] spawnPlaces;

    public GameObject[] foodToSpawn;

    [HideInInspector] public float minWait = 5f;
    [HideInInspector] public float maxWait = 7f;

    [HideInInspector] public float subtractionSpeed = .2f;

    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    private IEnumerator SpawnFruit() {
        while (true) {
            

            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            
            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject food = Instantiate(foodToSpawn[Random.Range(0, foodToSpawn.Length)], t.position, t.rotation);

            Rigidbody2D rb = food.GetComponent<Rigidbody2D>();

            rb.AddForce(t.transform.up * Random.Range(25, 35), ForceMode2D.Impulse);

            rb.AddTorque(Random.Range(-2, 2), ForceMode2D.Impulse);

            Destroy(food, 5);


            if (minWait > .05) {
                minWait -= subtractionSpeed;
                maxWait -= subtractionSpeed;

                if (subtractionSpeed > .01) {
                    subtractionSpeed -= .01f;
                }
            }


        }
    }
}
