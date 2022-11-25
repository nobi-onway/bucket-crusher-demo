using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShawBehaviour : MonoBehaviour
{
    private Camera mainCam;
    private LayerMask backgroundMask;
    private Vector3 lastPosition;
    private bool isDragging;
    [SerializeField] private float speed;

    private const float MAX_SPEED = 4.0f;
    private const float FORD_MODE = 15.0f;

    private void OnValidate() {
        mainCam = Camera.main;
        backgroundMask = LayerMask.GetMask("Background");
    }

    private void Update()
    {
        Spin();
    }
    private void OnMouseDown() => lastPosition = Input.mousePosition;

    private void OnMouseDrag() {

        isDragging = true;

        Ray pointingRay = mainCam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(pointingRay, out RaycastHit hitInfo, 100f, backgroundMask))
        {
            Vector3 newPos = new Vector3(hitInfo.point.x, hitInfo.point.y, transform.position.z);
            transform.position = newPos;
        }
    }

    private void OnMouseUp() => isDragging = false;

    private void OnTriggerStay(Collider other) 
    {   
        Rigidbody objectRb = other.gameObject.GetComponent<Rigidbody>();

        if(!objectRb) return;

        Vector3 direction = other.transform.position - transform.position;

        objectRb.isKinematic = false;
        objectRb.AddForce(direction.normalized * FORD_MODE, ForceMode.Impulse);
    }

    private void Spin() {
        SpeedControl();

        transform.Rotate(0, 0, -360 * Time.deltaTime * speed);
    }

    private void SpeedControl()
    {
        if(isDragging) speed = (speed < MAX_SPEED) ? (speed + Time.deltaTime * 2) : MAX_SPEED;

        if(!isDragging) speed = (speed < 0) ? 0 : (speed - Time.deltaTime * 2);
    } 
}
