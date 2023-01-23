using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] float speed = 1f;

    private float xRotation = 0f;
    [SerializeField] float mouseSensitivity = 100f;

    [SerializeField] Vector3 velocity;

    private float gravity = -15f;


    [SerializeField] Transform groundChecker;
    [SerializeField] float groundCheckerRadius;
    [SerializeField] LayerMask obstacleLayer;

    private bool isGround;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

        Vector3 moveVelocity = moveInputs * Time.deltaTime * speed;
        controller.Move(moveVelocity);


        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        xRotation=Mathf.Clamp(xRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);


        isGround = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, obstacleLayer);
        if (!isGround)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        

        else
        {
            velocity.y = -0.05f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocity.y = Mathf.Sqrt(0.05f* -2f* gravity);
        }
        controller.Move(velocity);

    }
}
