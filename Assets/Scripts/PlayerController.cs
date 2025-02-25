using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed = 3;
    public float jumpHeight = 300;
    public float maxSpeed = 1000;
    float x_input;
    public bool canJump;

    #endregion

    #region Physics_variables
    Rigidbody2D rb;
    #endregion

    #region Gameplay_variables
    private bool hasKey;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    #region Movement_functions
    private void Move()
    {   
        x_input = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(x_input * moveSpeed, 0));

        if (rb.linearVelocity.x > maxSpeed) {
			rb.linearVelocity = new Vector2 (maxSpeed, rb.linearVelocity.y);
		}
		if (rb.linearVelocity.x < -maxSpeed) {
			rb.linearVelocity = new Vector2 (-maxSpeed, rb.linearVelocity.y);
		}

        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            rb.AddForce(new Vector2(0, jumpHeight));
        }
    }

    #endregion

    #region Gameplay_functions
    public void CollectedKey() {
        hasKey = true;
    }
    #endregion
}
