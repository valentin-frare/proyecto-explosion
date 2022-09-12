using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCarScript : MonoBehaviour
{
    public Rigidbody sphereRB;
    
    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;

    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;
    
    private float normalDrag;
    public float modifiedDrag;
    
    public float alignToGroundTime;
    
    void Start()
    {
        // Detach Sphere from car
        sphereRB.transform.parent = null;

        normalDrag = sphereRB.drag;
    }
    
    void Update()
    {
        // Get Input
        moveInput = Mathf.Clamp(Input.GetAxisRaw("Vertical"), 1f/4f, 1f);
        turnInput = Input.GetAxisRaw("Horizontal");

        // Calculate Turning Rotation
        if (turnInput != 0)
        {
            float newRot = turnInput * turnSpeed * Time.deltaTime * moveInput;
            
            if (isCarGrounded)
            {
                if (Mathf.Abs(transform.rotation.y * Mathf.Rad2Deg) < 20)
                    transform.Rotate(0, newRot, 0, Space.World);
            }
        }
        else
        {
            float singleStep = turnSpeed * Time.deltaTime;

            Vector3 newDirection = new Vector3(0, 0, 0);

            Quaternion toRotateTo2 = Quaternion.FromToRotation(transform.forward, newDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo2, Time.smoothDeltaTime * 3f);
        }

        // Set Cars Position to Our Sphere
        transform.position = sphereRB.transform.position;

        // Raycast to the ground and get normal to align car with it.
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);
        
        // Rotate Car to align with ground
        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);
        
        // Calculate Movement Direction
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;
        
        // Calculate Drag
        sphereRB.drag = isCarGrounded ? normalDrag : modifiedDrag;
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration); // Add Movement
        else
            sphereRB.AddForce(transform.up * -200f); // Add Gravity
    }
}