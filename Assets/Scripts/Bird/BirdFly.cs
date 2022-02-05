using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BirdState
{

    Flying,
    Ragdoll
}

public class BirdFly : MonoBehaviour
{
    [SerializeField] Text timeText;

    public int MaxPoints = 7;

    public int Points = 0;

    [SerializeField] GameObject myRagdollPrefab;

    [SerializeField] BirdState currentBirdState;

    Rigidbody birdRigidbody;

    [SerializeField] float speed;

    [SerializeField] float currentSpeed;

    [SerializeField] Animator birdAnimator;


    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCamera;

    [SerializeField] CinemachineTransposer cameraTransposer;


    // Start is called before the first frame update
    void Start()
    {
        Points = 0;

        currentBirdState = BirdState.Flying;

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

    public float CurrentTime = 0;

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        timeText.text = (CurrentTime % 60).ToString("F2");

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
        cameraTransposer.m_FollowOffset = new Vector3(0, rotationX/4, -10);
    }

    void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentSpeed = speed * 4;
        }

        if(Input.GetMouseButtonUp(0))
        {
            currentSpeed = speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("obstacle"))
        {
            // enter ragdoll mode
            var rag = Instantiate(myRagdollPrefab, this.transform.position, Quaternion.identity);

            virtualCamera.LookAt = rag.transform;
            virtualCamera.Follow = rag.transform;

            cameraTransposer.m_FollowOffset = new Vector3(0, 5 , -10);

            Destroy(this.gameObject);

        }

    }


}
