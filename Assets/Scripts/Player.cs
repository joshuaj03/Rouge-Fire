using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    
    [SerializeField] private Vector3 movementDirection;
    private float peakDirection;
    //[SerializeField] private float peakSpeed;

    //Movement
    [SerializeField] private float walkSpeed;
    [SerializeField] public float runSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity = 9.8f;

    //Camera Variables FPS/TPP
    [SerializeField] private Transform cameraTransform;
    private float cameraRotX;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float lookXLimit = 45;

    //Attack Setting
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private UIManager uiManager;
    [SerializeField] private int attackImpact;

    //Scope
    [SerializeField] private Camera cam;
    [SerializeField] private float defaultFOV = 60;

    public bool isDead = false;
    public bool isAim = false;
    private bool isAimAnimationPlaying = false;
    [SerializeField] public Animator animator;
    [SerializeField] public AudioSource hurt;
    /*
    //Bounce Variables
    [SerializeField] private float bounceForce = 5f;
    [SerializeField] private float bounceDamp = 0.5f;
    private bool isBouncing = false;
    */

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        uiManager = FindObjectOfType<UIManager>();
        currentHealth = maxHealth;

    }

    /*private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }*/

    private void Update()
    {
        Boolean isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeedV = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float currentSpeedH = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        Vector3 tempDirection = (transform.forward * currentSpeedV) + (transform.right * currentSpeedH);
        Vector3 resetPosition;
        movementDirection.x = tempDirection.x;
        movementDirection.z = tempDirection.z;

        if (Input.GetButton("Jump") && characterController.isGrounded && !isDead)
        {
            animator.Play("Jump");
            movementDirection.y = jumpPower;
            //isBouncing = true;
        }

        if (isRunning)
        {
            animator.Play("Run");
        }
        else if (currentSpeedV != 0 || currentSpeedH != 0)
        {
            animator.Play("Walk");
        }

        if (!characterController.isGrounded)
        {
            movementDirection.y -= gravity * Time.deltaTime;
        }
        /*else
        {
            if(isBouncing)
            {
                movementDirection.y += bounceForce;
                isBouncing = false;
            }
        }*/

        characterController.Move(movementDirection * Time.deltaTime);

        if (transform.position.y <= -100)
        {
            resetPosition = transform.position;
            resetPosition.x = 0;
            resetPosition.y = 0;
            resetPosition.z = 0;
            transform.position = resetPosition;
            movementDirection.y = -7;
        }

        cameraRotX += -Input.GetAxis("Mouse Y") * lookSpeed;
        cameraRotX = Mathf.Clamp(cameraRotX, -lookXLimit, lookXLimit);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0);

        //Peak
        /*if(Input.GetKeyDown(KeyCode.Q)) 
        {
            peakDirection = 20;
            //peakDirection = Mathf.Clamp(peakDirection, -peakLimit, 0);
            transform.rotation = Quaternion.Euler(0, Input.GetAxis("Mouse X"), peakDirection);


        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            transform.rotation = Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            peakDirection = -20;
            //peakDirection = Mathf.Clamp(peakDirection, 0 , peakLimit);
            transform.rotation = Quaternion.Euler(0, Input.GetAxis("Mouse X"), peakDirection); ;
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            transform.rotation = Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0);
        }*/

        //Dead like position
        if (currentHealth == 0)
        {
            isDead = true;
            runSpeed = 0;
            walkSpeed = 0;
            float xrotate = transform.eulerAngles.x;
            xrotate = Mathf.Clamp(xrotate, -160, 60); //This line isn't working
            transform.rotation = Quaternion.Euler(xrotate, 0, 90);
            //transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);

        }

        //Scope
        if (Input.GetMouseButton(1))
        {
            if (!isAimAnimationPlaying)
            {
                animator.Play("Aim");
                isAimAnimationPlaying = true;
            }
            cam.fieldOfView = (defaultFOV / 2);
            isAim = true;
        }
        else
        {
            if (isAimAnimationPlaying)
            {
                animator.Play("Idle");
                isAimAnimationPlaying = false;
            }
            cam.fieldOfView = defaultFOV;
            isAim = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("Hit!");
            hurt.Play();
            currentHealth -= attackImpact;
            uiManager.OnHealthReduced(currentHealth);
        }
    }
}
