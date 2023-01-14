using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    public float speed;

    private Vector3 playerVelocity;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpForce = 1.5f;

    public Camera cam;
    private Vector2 lookPosition;
    private float xRotation = 0f;
    public float xSens = 30f;
    public float ySens = 30f;

    public void Andar(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Saltar_(InputAction.CallbackContext context)
    {
        Saltar();
    }

    public void Olhar_(InputAction.CallbackContext context)
    {
        lookPosition = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        movePlayer();
        Olhar();

    }

    public void movePlayer()
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = moveInput.x;
        moveDirection.z = moveInput.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;

        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Saltar()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3f * gravity);

        }

    }

    public void Olhar()
    {
        xRotation -= (lookPosition.y * Time.deltaTime) * ySens;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (lookPosition.x * Time.deltaTime) * xSens);
    }
}
