using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField]
    private float jumpHeight = 300;
    [SerializeField]
    private float maxSpeed = 1000;
    [SerializeField]
    private float x_input;
    [SerializeField]
    public bool canJump;

    #endregion

    #region Physics_variables
    Rigidbody2D rb;
    #endregion

    #region Gameplay_variables
    private bool hasKey;
    public class RecordedFrame {
        public Vector2 position;
        public float rotation;
        public Vector2 velocity;
        public float time;
    }
    #endregion

    #region Interact_variables 
    private Vector2 currDirection;
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

        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        } else if (Input.GetKeyDown(KeyCode.R)) {
            Reboot();
        }
    }


    #region Movement_functions
    private void Move()
    {   
        x_input = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(x_input * moveSpeed, 0));
        currDirection = new Vector2(x_input, 0);

        if (rb.linearVelocity.x > maxSpeed) {
			rb.linearVelocity = new Vector2 (maxSpeed, rb.linearVelocity.y);
		}
		if (rb.linearVelocity.x < -maxSpeed) {
			rb.linearVelocity = new Vector2 (-maxSpeed, rb.linearVelocity.y);
		}

        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            canJump = false;
            rb.AddForce(new Vector2(0, jumpHeight));
        }
    }

    #endregion

    #region Gameplay_functions
    public void CollectedKey() {
        hasKey = true;
    }

    private void Reboot() {
        Debug.Log("reboot");
    }
    #endregion

    #region Interact_functions
    private void Interact() {
        Debug.Log("asdasdas");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(rb.position + currDirection, Vector2.one, 0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            if (hit.transform.CompareTag("Exit")) {
                if (hasKey) {
                    hit.transform.GetComponent<Door>().NextLevel();
                } else {
                    Debug.Log("no key");
                }
            }
        }
    }
    #endregion

    #region Access Functions
    public bool check_HasKey() {
        return hasKey;
    }
    #endregion
}
