using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour
{
    Rigidbody birdRigidbody;

    [SerializeField] float speed;

    [SerializeField] float currentSpeed;

    [SerializeField] Animator birdAnimator;


    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCamera;

    [SerializeField] CinemachineTransposer cameraTransposer;


    // Start is called before the first frame update
    void Start()
    {
        birdRigidbody = this.GetComponent<Rigidbody>();

        birdAnimator = this.GetComponent<Animator>();

        cameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = speed;

    }

    public float rotationX;
    public float rotationY;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    // Update is called once per frame
    void Update()
    {
        birdAnimator.speed = currentSpeed/2000;

        HandleMouseInput();

        LetBirdFly();

    }

    void LetBirdFly()
    {
        birdRigidbody.velocity = transform.forward * currentSpeed * Time.deltaTime;


        // rotation on x-axis
        rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        // rotation on y-axis
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;

        // rotate bird
        this.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // offset camera
        cameraTransposer.m_FollowOffset = new Vector3(0, rotationX/10, -10);
    }

    void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentSpeed = speed * 2 + 5;
        }

        if(Input.GetMouseButtonUp(0))
        {
            currentSpeed = speed;
        }
    }

}
