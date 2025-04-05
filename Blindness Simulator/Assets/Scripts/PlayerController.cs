using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMoveInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    public Transform playerCamera;
    public CharacterController characterController;
    public float speed;
    public float jumpForce;
    public float sensitivity;
    private float gravity = -9.81f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        if (characterController.isGrounded)
        {
            Velocity.y = -1;

            if (Input.GetKeyDown(KeyCode.Space)) {
                Velocity.y = jumpForce;
            }
        }
        else
        {
            Velocity.y += gravity * Time.deltaTime;
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerMoveInput);
        characterController.Move(MoveVector * speed * Time.deltaTime);
        characterController.Move(Velocity * Time.deltaTime);
    }

    void MoveCamera()
    {
        xRot -= PlayerMouseInput.y * sensitivity;
        transform.Rotate(0f, PlayerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
