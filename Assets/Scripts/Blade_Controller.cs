using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 lastMousePos;
    public float minVelo = .1f;

    private Collider2D col;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
    }

    void Update()
    {
        SetBladeToMouse();
    }

    private void FixedUpdate() {
        col.enabled = IsMouseMoving();
    }

    private void SetBladeToMouse() {
        var mousePos = Input.mousePosition;
        mousePos.z = 10; // distance from camera;

        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool IsMouseMoving() {
        Vector3 curMousePos = transform.position;
        float traveled = (lastMousePos - curMousePos).magnitude;
        lastMousePos = curMousePos;

        if (traveled > minVelo) {
            return true;
        } else {
            return false;
        }
    }

}
