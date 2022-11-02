using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    private Vector2 mousePos;
    public Rigidbody2D rb;
    Camera cam;
    public GameObject aimbox;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        if (this.transform.parent.gameObject.GetComponent<Movement>().mine == true)
        {
            aimbox.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );

        transform.up = direction;
    }
}
