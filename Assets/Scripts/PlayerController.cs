using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.SceneManagement;

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

    public class RecordedFrame
    {
        public Vector2 position;
        public float rotation;
        public Vector2 velocity;
        public float time;

        public override string ToString()
        {
            return $"{position} {rotation} {velocity} {time}";
        }
    }

    public static List<List<RecordedFrame>> allRecordings = new List<List<RecordedFrame>>();
    private List<RecordedFrame> currentRecording = new List<RecordedFrame>();
    static float roundStartTime; // used to record time relative to start of round
    #endregion

    #region Interact_variables
    private Vector2 currDirection;
    #endregion

    #region Animation

    private Animator anim;
    #endregion

    public GameManager gameManager;

    #region Unity_functions
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Debug.Log("awakeaaa");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        anim.SetFloat("facingDirection", 1);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        anim.SetBool("isJumping", !canJump);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        if (x_input != 0)
        {
            anim.SetFloat("facingDirection", x_input);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reboot();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // reset level if player mess up
            ClearAllRecording();
            ResetLevel();
        }
    }

    // Unity fn: Store position, rotation (as float), and velocity at each physics step
    void FixedUpdate()
    {
        currentRecording.Add(
            new RecordedFrame
            {
                position = transform.position,
                rotation = transform.rotation.eulerAngles.z,
                velocity = rb.linearVelocity,
                time = Time.time - roundStartTime,
            }
        );
    }
    #endregion

    #region Movement_functions
    private void Move()
    {
        x_input = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(x_input * moveSpeed, 0));
        currDirection = new Vector2(x_input, 0);

        if (rb.linearVelocity.x > maxSpeed)
        {
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);
        }
        if (rb.linearVelocity.x < -maxSpeed)
        {
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocity.y);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.NextScene();
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector2(0, jumpHeight));
        }
    }

    #endregion

    #region Gameplay_functions
    public void CollectedKey()
    {
        hasKey = true;
    }

    private void Reboot()
    {
        Debug.Log("reboot");
        SaveRecording();
        ResetLevel();
    }

    private void ResetLevel()
    {
        gameManager.ReloadCurrentLevel();
        roundStartTime = Time.time; // reset time for each reboot
    }

    private void SaveRecording()
    {
        allRecordings.Add(new List<RecordedFrame>(currentRecording));
        currentRecording.Clear();
    }

    private void ClearAllRecording()
    {
        allRecordings.Clear();
        currentRecording.Clear();
    }

    public List<List<RecordedFrame>> GetAllRecordings()
    {
        return allRecordings;
    }
    #endregion

    #region Interact_functions
    private void Interact()
    {
        Debug.Log("asdasdas");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            rb.position + currDirection,
            Vector2.one,
            0f,
            Vector2.zero
        );
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Exit"))
            {
                if (hasKey)
                {
                    hit.transform.GetComponent<Door>().NextLevel();
                    ClearAllRecording();
                    roundStartTime = Time.time; // reset time
                }
                else
                {
                    Debug.Log("no key");
                }
            }
        }
    }
    public bool check_HasKey() {
        return hasKey;
    }
    #endregion

    #region Debug_functions
    public void PrintRecording(List<RecordedFrame> recordedFrames)
    {
        foreach (RecordedFrame frame in recordedFrames)
        {
            Debug.Log(frame.ToString());
        }
    }

    #endregion

}
