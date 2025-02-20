using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed = 3;
    float x_input;

    #endregion

    #region Physics_variables
    Rigidbody2D rb;
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


    private void Move()
    {   
        x_input = Input.GetAxisRaw("Horizontal");
        Vector2 vector = Vector2.zero;
        if (x_input == -1) {
            vector = Vector2.left;
        } else if (x_input == 1) {
            vector = Vector2.right;
        }
        rb.linearVelocityX = vector.x * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(new Vector2(0,100));
        }
    }
}
