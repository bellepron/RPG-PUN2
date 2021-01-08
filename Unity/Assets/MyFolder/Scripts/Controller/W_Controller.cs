using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class W_Controller : MonoBehaviour
{
    [Header("Metrics")]
    public float damp; //delay
    [Range(1, 20)]
    public float rotationSpeed;
    [Range(1, 20)]
    public float strafeTurnSpeed;
    float normalFov;
    public float sprintFov;

    float inputX;
    float inputY;
    float maxSpeed;

    public Transform model;

    PhotonView photonView;
    Animator animator;
    Vector3 stickDirection;
    Camera mainCam;

    public KeyCode sprintButton = KeyCode.LeftShift;
    public KeyCode walkButton = KeyCode.C;

    public enum MovementType { Directional, Strafe };
    public MovementType movementType;
    public bool isStrafeMoving;


    void Start()
    {
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;
    }

    void LateUpdate()
    {
        if (photonView.IsMine == true)
            Movement();
    }

    void Movement()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        if (movementType == MovementType.Strafe)
        {
            animator.SetFloat("iX", inputX, damp, Time.deltaTime * 10);
            animator.SetFloat("iY", inputY, damp, Time.deltaTime * 10);

            isStrafeMoving = inputX != 0 || inputY != 0;

            if (isStrafeMoving)
            {
                float yawCamera = mainCam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), strafeTurnSpeed * Time.deltaTime);
                animator.SetBool("strafeMoving", true);
            }
            else
            {
                animator.SetBool("strafeMoving", false);
            }
        }

        if (movementType == MovementType.Directional)
        {
            InputMove();
            InputRotation();
            if (Input.GetKey(sprintButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, sprintFov, Time.deltaTime * 2);
                maxSpeed = 2;
                inputX *= maxSpeed;
                inputY *= maxSpeed;
            }
            else if (Input.GetKey(walkButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
                maxSpeed = 0.2f;
                inputX *= maxSpeed;
                inputY *= maxSpeed;
            }
            else
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
                maxSpeed = 1;
                inputX *= maxSpeed;
                inputY *= maxSpeed;
            }

            stickDirection = new Vector3(inputX, 0, inputY);
        }
    }

    void InputMove()
    {
        animator.SetFloat("speed", Vector3.ClampMagnitude(stickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }

    void InputRotation()
    {
        Vector3 rotationOffset = mainCam.transform.TransformDirection(stickDirection);
        rotationOffset.y = 0;

        model.forward = Vector3.Slerp(model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }
}