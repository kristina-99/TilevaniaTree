using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]float jumpSpeed = 5.0f;
    [SerializeField] float moveSpeed = 10f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
            
        if (value.isPressed)
        {
            myRigidBody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
        
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.linearVelocity.y);
        myRigidBody.linearVelocity = playerVelocity;
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
        }
    }
}
