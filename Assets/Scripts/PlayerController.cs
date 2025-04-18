//partially using //https://www.youtube.com/watch?v=f473C43s8nE

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Parts")]
    public GameObject targetFollow; //this is for the cinemachine
    public GameObject playerMesh; //player's body
    public GameObject armPivot; //players arm
    public GameObject lArmPivot; //left arm

    Rigidbody rb;

    [Header("Movement Settings")]
    public float speed = 100f;
    public float sprintModifier = 50f;
    public float fallingGravity = -1f;

    //input
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Transform orientation;

    //drag
    [Header("Drag")]
    public float groundDrag;
    public float playerHeight;

    [Header("Ground")]
    public LayerMask whatIsGround;
    bool grounded;
    float sprinting = 0f;

    [Header("Camera Settings")]
    public float sensX = 10f; //horizontal sensitivity
    public float sensY = 10f; //vertical sensitivity
    public float maxLookAngle = 20f;
    public float minLookAngle = -20f;

    Vector2 mouseRot;
    float xRotation;
    float yRotation;

    //animation
    Animator lArmAnimator;

    //ammo
    AmmoManager ammoManager;

    //health
    HealthSystem healthSystem;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        orientation = targetFollow.transform;

        //animation
        lArmAnimator = lArmPivot.GetComponent<Animator>();

        ammoManager = GetComponent<AmmoManager>();
        healthSystem = GetComponent<HealthSystem>();
    }

    void Update()
    {
        //make sure we are on the ground
        CheckGround();

        //get WASD + sprint
        KeyboardInput();

        ApplyDrag();

        //rotate the camera
        RotateCamera();

        AnimateParts();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    void MovePlayer() {
        moveDirection = playerMesh.transform.forward * verticalInput + playerMesh.transform.right * horizontalInput;

        //no delta time with fixed update
        rb.AddForce(moveDirection.normalized * (speed + (sprinting * sprintModifier)), ForceMode.Force);
        
        if (!grounded) {
            //rb.AddForce(moveDirection.normalized * (1/4) * (speed + (sprinting * sprintModifier)), ForceMode.Force);

            //gravity
            float gravity = rb.linearVelocity.y + fallingGravity;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, gravity, rb.linearVelocity.z);
        }
    }

    void KeyboardInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)) {
            sprinting = 1;
        } else {
            sprinting = 0;
        }
    }

    void CheckGround() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    void ApplyDrag() {

        //always apply drag!

        rb.linearDamping = groundDrag;

        //if (grounded == true) {
        //    rb.linearDamping = groundDrag;
        //} else {
        //    rb.linearDamping = 0;
        //}
    }

    void RotateCamera() {
        mouseRot.x = Input.GetAxisRaw("Mouse X");
        mouseRot.y = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseRot.x * Time.deltaTime * sensX;
        xRotation -= mouseRot.y * Time.deltaTime * sensY;

        //stop from looking too far up or down
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);

        targetFollow.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerMesh.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        armPivot.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    void SpeedControl() {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limit the magnitude of speed
        Vector3 limitedVelocity = flatVelocity.normalized * speed;
        rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
    }

    void AnimateParts() {

        //walking animation
        lArmAnimator.SetFloat("input", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void OnCollisionEnter(Collision collision) {
        //check if enemy
        if(collision.gameObject.tag == "Enemy") {
            healthSystem.Damage(1);
        }

        //check if ammo crate
        if (collision.gameObject.tag == "AmmoCrate") {
            ammoManager.ReloadAmmo();

            //remove create
            Destroy(collision.gameObject);
        }
    }
}
