using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private TrailRenderer tr;
    private AudioSource audioS;

    private Vector3 lastMousePos;
    public float minVelo = .1f;
    public float soundVelo = 1;

    private Collider2D col;
    void Awake()
    {
        audioS = GetComponent<AudioSource>();
        tr = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            tr.Clear();
        }
        SetBladeToMouse();
    }

    private void FixedUpdate() {
        col.enabled = ReturnIsMouseMovingAndPlaySound();
    }

    private void SetBladeToMouse() {
        var mousePos = Input.mousePosition;
        mousePos.z = 10; // distance from camera;

        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool ReturnIsMouseMovingAndPlaySound() {
        Vector3 curMousePos = transform.position;
        float traveled = (lastMousePos - curMousePos).magnitude;
        lastMousePos = curMousePos;

        if (traveled > 1.5) {
            audioS.pitch = 2/traveled;
            audioS.Play();
        }

        if (traveled > minVelo) {
            return true;
        } else {
            return false;
        }
    }

}
