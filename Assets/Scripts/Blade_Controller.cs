using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetBladeToMouse();
    }

    private void SetBladeToMouse() {
        var mousePos = Input.mousePosition;
        mousePos.z = 10; // distance from camera;

        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
