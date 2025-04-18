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

    public UnityEngine.Object groundLidarPoint;
    public UnityEngine.Object objectLidarPoint;
    public UnityEngine.Object plantLidarPoint;
    public UnityEngine.Object interactibleLidarPoint;
    public UnityEngine.Object grassLidarPoint;
    public Transform playerCamera;
    public Transform whiteCane;
    public CharacterController cc;
    public float speed = 3;
    public float sensitivity = 5;
    public int auraSpeed = 5;
    public float gravity = -9.81f;
    private Vector3 whiteCaneStore;
    private Vector3 AuraStore;

    public float startTimer = 30f;

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
        if(startTimer <= 0)
        {
            PlayerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            PlayerMouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            WhiteCane();
            AuraPoints();
            MovePlayer();
            MoveCamera();
            startTimer = 0;
        }
        else
        {
            startTimer -= Time.fixedDeltaTime;

            if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.O))
            {
                startTimer = 0;
            }
        }
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

        Vector3 MoveVector = transform.TransformDirection(PlayerMoveInput);

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

        if (Physics.Raycast(transform.position, whiteCane.position - transform.position, out Hit, 3, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
        {
            if (Vector3.Distance(whiteCaneStore, Hit.point) > 0.01f)
            {
                Quaternion quaternion = Quaternion.LookRotation(Hit.normal);

                switch (Hit.collider.tag)
                {
                    case "Object":
                        Instantiate(objectLidarPoint, Hit.point, quaternion);
                        break;
                    case "Plant":
                        Instantiate(plantLidarPoint, Hit.point, quaternion);
                        break;
                    case "Grass":
                        Instantiate(grassLidarPoint, Hit.point, quaternion);
                        break;
                    case "Interactible":
                        Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                        break;
                    case "Table":
                        Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                        break;
                    case "Sofa":
                        Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                        break;
                    case "Sink":
                        Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                        break;
                    case "Bed":
                        Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                        break;
                    case "Wardrobe":
                        Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                        break;
                    case "Fridge":
                        Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                        break;
                    default:
                        Instantiate(groundLidarPoint, Hit.point, quaternion);
                        break;
                }
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

            if (Physics.Raycast(transform.position, randomDirection, out Hit, 2, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                if(!Hit.collider.CompareTag("Player"))
                {
                    if (Vector3.Distance(AuraStore, Hit.point) > 0.5f)
                    {
                        Quaternion quaternion = Quaternion.LookRotation(Hit.normal);

                        switch (Hit.collider.tag)
                        {
                            case "Object":
                                Instantiate(objectLidarPoint, Hit.point, quaternion);
                                break;
                            case "Plant":
                                Instantiate(plantLidarPoint, Hit.point, quaternion);
                                break;
                            case "Interactible":
                                Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                                break;
                            case "Grass":
                                Instantiate(grassLidarPoint, Hit.point, quaternion);
                                break;
                            case "Table":
                                Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                                break;
                            case "Sofa":
                                Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                                break;
                            case "Sink":
                                Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                                break;
                            case "Bed":
                                Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                                break;
                            case "Wardrobe":
                                Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                                break;
                            case "Fridge":
                                Instantiate(interactibleLidarPoint, Hit.point, quaternion);
                                break;
                            default:
                                Instantiate(groundLidarPoint, Hit.point, quaternion);
                                break;
                        }
                    }
                    AuraStore = Hit.point;
                }
            }
        }
    }
}
