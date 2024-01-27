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

    private CapsuleCollider capsuleCollider;
    private bool isGrounded = true;
    private Rigidbody rigidBody;
    private bool hasFeather;
    private int horizontalMovement;




    private Vector3 point1, point2;


    private void Start() {
        capsuleCollider = playerCapsule.GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Update() {
        FeatherCheck();
        GroundCheck();
        MoveHorizontal();
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
        RaycastHit hitInfo;
        isGrounded = !Physics.SphereCast(groundCheck.position, 0.5f, transform.up * -1, out hitInfo, groundLayer);
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(groundCheck.position, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(point1, 0.1f);
        Gizmos.DrawSphere(point2, 0.1f);
    }

    private void MoveHorizontal() {
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, moveSpeed * horizontalMovement * -1);
    }

    public void OnJump(InputValue value) {
        jump = value.Get<float>();
        if (jump > 0) {
            if(isGrounded)
                rigidBody.velocity = new Vector3(0, jump * jumpForce, rigidBody.velocity.z);
        }
    }

    public void OnMove(InputValue value) {
        horizontalMovement = (int)value.Get<Vector2>().x;
    }

    public void OnAttack() {
        if (hasFeather) {
            Attack();
        }
    }

    public void Attack() {
        Debug.Log("Attack");
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
