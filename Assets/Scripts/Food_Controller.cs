using Unity.VisualScripting;
using UnityEngine;

public class Food_Controller : MonoBehaviour
{
    public AudioClip sound;
    public GameObject slicedFoodPrefab;
    public GameManager gameManager;

    public GameObject particlePrefab;

    public Color sliceColor;
    private float spawnTime = 0;

    private int particleCount = 0;

    Rigidbody2D rb;

    private void Start() {
        spawnTime = Time.fixedTime;
        gameManager = FindObjectOfType<GameManager>();
        if ( gameManager == null) {
            Debug.LogError("Cannot find GameManager!!!");
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }


    private void SpawnAndAttachParticles(Transform t) {
        if (particleCount > 1) {
            return;
        }
        GameObject particleInst = Instantiate(particlePrefab, t.position, Quaternion.identity);
        particleInst.GetComponent<ParticleSystem>().startColor = sliceColor;
        particleInst.transform.SetParent(t);
        particleCount++;
    }


    public void CreateSlicedFood() {
        GameObject inst = Instantiate(slicedFoodPrefab, transform.position, transform.rotation);


        Rigidbody[] rbsOnSliced = inst.GetComponentsInChildren<Rigidbody>();

        if (rbsOnSliced.Length < 1) {
            Rigidbody2D[] rbsOnSlice2D = inst.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D r in rbsOnSlice2D) {
                r.velocity = rb.velocity;
                r.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)); 

                Vector2 explosionDirection = Random.insideUnitCircle;

                float explosionForce = Random.Range(4, 6);

                r.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);

                SpawnAndAttachParticles(r.transform);
            }
        }

        foreach (Rigidbody r in rbsOnSliced) {
            r.velocity = rb.velocity;
            r.transform.rotation = Random.rotation;

            Vector3 explosionDirection = Random.onUnitSphere;

            float explosionForce = Random.Range(2, 5);

            r.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);


            SpawnAndAttachParticles(r.transform);
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

        if (!b) {
            if (collision.tag == "DeathArea") {
                gameManager.DecreaseHealth(tag);
            }
            return; 
        }

        gameManager.HandleFruitHit(gameObject.tag, sound);

        CreateSlicedFood();
    }

}
