using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    private float pushForce = 20f;

    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CreateSlicedFruit();
        }
    }


    public void CreateSlicedFruit() {
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        Rigidbody[] rbsOnSliced = inst.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rbsOnSliced) {
            r.velocity = rb.velocity;
            r.transform.rotation = Random.rotation; 

            Vector3 explosionDirection = Random.onUnitSphere;

            float explosionForce = Random.Range(1, 4);

            r.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);

        }
        Destroy(inst, 5);
        Destroy(gameObject);
    }

}
