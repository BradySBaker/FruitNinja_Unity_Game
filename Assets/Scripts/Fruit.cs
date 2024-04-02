using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    private float pushForce = 20f;
    private float spawnTime = 0;

    Rigidbody2D rb;

    private void Start() {
        spawnTime = Time.fixedTime;
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }


    public void CreateSlicedFruit() {
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);


        Rigidbody[] rbsOnSliced = inst.GetComponentsInChildren<Rigidbody>();

        if (rbsOnSliced.Length < 1) {
            Rigidbody2D[] rbsOnSlice2D = inst.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D r in rbsOnSlice2D) {
                r.velocity = rb.velocity;
                r.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)); 

                Vector2 explosionDirection = Random.insideUnitCircle;

                float explosionForce = Random.Range(4, 6);

                r.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);

            }
        }

        foreach (Rigidbody r in rbsOnSliced) {
            r.velocity = rb.velocity;
            r.transform.rotation = Random.rotation;

            Vector3 explosionDirection = Random.onUnitSphere;

            float explosionForce = Random.Range(2, 5);

            r.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);

        }
        Destroy(inst, 5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Time.fixedTime - spawnTime < .25)
        {
            return;
        }

        Blade_Controller b = collision.GetComponent<Blade_Controller>();



        if (!b) { return; }

        CreateSlicedFruit();
    }

}
