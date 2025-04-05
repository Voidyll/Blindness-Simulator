using System.Data;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMoveInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    public UnityEngine.Object lidarPoint;
    public Transform playerCamera;
    public Transform whiteCane;
    public CharacterController cc;
    public float speed = 3;
    public float sensitivity = 5;
    public int auraSpeed = 5;
    public float gravity = -9.81f;
    private Vector3 whiteCaneStore;
    private Vector3 AuraStore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void FixedUpdate()
    {
        PlayerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        WhiteCane();
        AuraPoints();
        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        if (cc.isGrounded)
        {
            Velocity.y = -1;
        }
        else
        {
            Velocity.y += gravity * Time.fixedDeltaTime;
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerMoveInput).normalized;

        cc.Move(MoveVector * speed * Time.fixedDeltaTime);
        cc.Move(Velocity * Time.deltaTime);
    }

    void MoveCamera()
    {   
        xRot -= PlayerMouseInput.y * sensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.Rotate(0f, PlayerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    void WhiteCane()
    {
        RaycastHit Hit;

        if (Physics.Raycast(transform.position, whiteCane.position - transform.position, out Hit, 3))
        {
            if (Vector3.Distance(whiteCaneStore, Hit.point) > 0.01f)
            {
                Quaternion quaternion = Quaternion.LookRotation(Hit.normal);
                Instantiate(lidarPoint, Hit.point, quaternion);
            }
            whiteCaneStore = Hit.point;
        }
    }

    void AuraPoints()
    {
        for (int i = 0; i < auraSpeed; i++)
        {
            Vector3 randomDirection = Random.onUnitSphere;

            RaycastHit Hit;

            if (Physics.Raycast(transform.position, randomDirection, out Hit, 2))
            {
                if(!Hit.collider.CompareTag("Floor") && !Hit.collider.CompareTag("Player"))
                {
                    if (Vector3.Distance(AuraStore, Hit.point) > 0.5f)
                    {
                        Quaternion quaternion = Quaternion.LookRotation(Hit.normal);
                        Instantiate(lidarPoint, Hit.point, quaternion);
                    }
                    AuraStore = Hit.point;
                }
            }
        }
    }
}
