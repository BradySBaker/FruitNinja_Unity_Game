using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    private float pushForce = 20f;

    Rigidbody2D rb;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CreateSlicedFruit();
        }
    }


    public void CreateSlicedFruit() {
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        Rigidbody[] rbsOnSliced = inst.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rbsOnSliced) {
            r.velocity = new Vector3(0, rb.velocity.y, 0);
            r.transform.rotation = Random.rotation; 

            Vector3 explosionDirection = Random.onUnitSphere;

            float explosionForce = Random.Range(3, 10);

            r.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);

        }
        Destroy(inst, 5);
        Destroy(gameObject);
    }

}
