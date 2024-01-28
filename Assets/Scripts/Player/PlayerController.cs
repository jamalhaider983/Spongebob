using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsIntimidating;
    //[SerializeField] private float intimidationDuration = 2f;
    //private CharacterController characterController;
    public float moveSpeed, jumpForce;
    private float jump, verticalSpeed;
    [SerializeField] private LayerMask groundLayer, featherLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject playerCapsule;
    [SerializeField] private GroundCheck groundCheckScript;
    [SerializeField] private FeatherDetector featherDetector;
    public Animator animator;

    public GameObject featherInHand;

    public bool isIntimidating= false;
    private CapsuleCollider capsuleCollider;
    private float yRotation;

    private Rigidbody rigidBody;
    private bool hasFeather;
    private int horizontalMovement;




    private Vector3 point1, point2;


    private void Start() {
        featherDetector.OnFeatherUpdated = (bool value) =>
        {
            featherInHand.SetActive(value);
        };
        featherInHand.SetActive(false);
        capsuleCollider = playerCapsule.GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Update() {
        FeatherCheck();
        GroundCheck();
        Tickle();
        Intemediate();
        MoveHorizontal();
    }

    public void Intemediate() {
        if (Input.GetKeyDown(KeyCode.K)) {
            isIntimidating = true;
            if (transform.localScale.x < 1) {
                yRotation = 0f;
                transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
            } else {
                yRotation = 180f;
                transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
            }
            animator.SetBool("butShake", true);
        }
        if (Input.GetKeyUp(KeyCode.K)) {
            isIntimidating = false;
            animator.SetBool("butShake", false);
        }
    }

    public void Tickle() {
        if (Input.GetKeyDown(KeyCode.H)) {
            animator.SetBool("tickle", true);
        }
        if (Input.GetKeyUp(KeyCode.H)) {
            animator.SetBool("tickle", false);
        }
    }

    public void FixedUpdate() {
        
    }

    public void FeatherCheck() {


        //float capsuleRadius = capsuleCollider.radius;
        //float capsuleHeight = capsuleCollider.height;

        //point1 = playerCapsule.transform.position + Vector3.up * capsuleHeight * 0.5f;
        //point2 = playerCapsule.transform.position - Vector3.up * capsuleHeight * 0.5f;
        //if (Physics.CapsuleCast(point1, point2, capsuleRadius, playerCapsule.transform.up * -1, out RaycastHit hit, 5f, featherLayer)) {
        //    Debug.Log("Hitting Feather");
        //}

        //Debug.DrawLine(point1, point2, Color.green);
    }

    private void GroundCheck() {
        //RaycastHit hitInfo;
        //isGrounded = !Physics.SphereCast(groundCheck.position, 0.5f, transform.up * -1, out hitInfo, groundLayer);
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(groundCheck.position, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(point1, 0.1f);
        Gizmos.DrawSphere(point2, 0.1f);
    }

    private void MoveHorizontal() {
        if (groundCheckScript.isGrounded == true) {
            //Debug.Log(horizontalMovement);
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, moveSpeed * horizontalMovement * -1);
        } else if(groundCheckScript.isGrounded == false) {
            //Debug.Log(horizontalMovement / 2);
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, moveSpeed * (horizontalMovement * 0.7f) * -1);
        }
        if (!isIntimidating) { 
            yRotation = -90f;
        }
        if (rigidBody.velocity.z > 1) {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            yRotation = 0f;
        } else if (rigidBody.velocity.z < -1) {

            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            yRotation = 180f;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
        bool animatorRunning = (rigidBody.velocity.z > 0 || rigidBody.velocity.z < 0);
        animator.SetBool("running", animatorRunning);
    }


    public void OnJump(InputValue value) {
        jump = value.Get<float>();
        if (jump > 0) {
            if (groundCheckScript.isGrounded) { 
                rigidBody.velocity = new Vector3(0, jump * jumpForce, rigidBody.velocity.z);
                animator.SetTrigger("jump");
            }
        }
    }

    public void OnMove(InputValue value) {
        horizontalMovement = (int)value.Get<Vector2>().x;
        Debug.Log("OnMove " + horizontalMovement);
    }

    public void OnTickle(InputValue value) {
        //Debug.Log("OnTickle");
        //if (hasFeather) {
        //    //animator.SetBool("tickle",);
        //}
    }

    public void OnTaunt() { 
    
    }

    private void OnIntimidateInput(InputAction.CallbackContext context)
    {
        Intimidate();
    }

    public void Intimidate()
    {   if(IsIntimidating) return;
        IsIntimidating = true;
        ResetIntimidation();
    }

    private async void ResetIntimidation()
    {
        //await Task.Delay((int)intimidationDuration *1000);
        IsIntimidating = false;
    }

    private void OnDestroy()
    {
        //intimidateInput.Disable();
    }
}
