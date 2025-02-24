using UnityEngine;

public class pl_movement : MonoBehaviour
{
    [Header("VARIABLES SPEED")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    private float currentSpeed;

    [Header("VARIABLES CROUCH")]
    public float normalHeight = 2;
    public float crouchHeight = 1;
    private float currentHeight;
    public CapsuleCollider plCollider;

    [Header("VARIABLES JUMP")]
    public float standingJumpForce = 2;
    public float crouchJumpForce = 1;
    private float currentJumpForce;
    public bool canJump = false;
    Rigidbody rB;

    public playerState crouchState = new PsCrouch();
    public playerState standingState = new PsStanding();
    private playerState currentState;
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        plCollider.height = normalHeight;
        
    }
    private void Awake()
    {
        SwitchState(standingState);
        currentSpeed = walkSpeed;
    }
    private void Update()
    {
        currentState.OnUpdate(this);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.OnFixedUpdate(this);
    }

    public void SwitchState(playerState newState)
    {
        if (currentState != null)
        {
            currentState.OnEnd(this);

            Debug.Log($"Old state {currentState}");
        }
        
        currentState = newState;
        currentState.OnStart(this);
        Debug.Log($"New state {currentState}");
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray r = new Ray(transform.position-new Vector3(0, currentHeight / 2, 0), -transform.up);
            if (Physics.Raycast(r, out RaycastHit hitInfo,0.3f ))
            {
                if(hitInfo.transform.tag!="Player")
                rB.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
            }

           
        }
    }
    #region Mechanics
    public void Walk()
    {
        float inputX = Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
        float inputZ = Input.GetAxis("Vertical") * Time.fixedDeltaTime;



        transform.position += transform.forward * currentSpeed * inputZ + transform.right * currentSpeed * inputX;
    }
    

    public void ToggleRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
        }
    }

    public void ToggleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SwitchState(crouchState);
            plCollider.height = currentHeight;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Ray r = new Ray(transform.position, transform.up);
            if (!Physics.Raycast(r, out RaycastHit hitInfo, 2,7))
            {
                SwitchState(standingState);
                transform.position = transform.position + Vector3.up * 0.1f;
                plCollider.height = currentHeight;
            }
            
        }
    }

    public void CrouchStats()
    {
        currentHeight = crouchHeight;
        currentSpeed = crouchSpeed;
        currentJumpForce = crouchJumpForce;
    }
    public void StandingStats()
    {
        currentHeight = normalHeight;
        currentSpeed = walkSpeed;
        currentJumpForce = standingJumpForce;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Color tmp = Color.cyan;
       
        Gizmos.color = tmp;
        Gizmos.DrawSphere(transform.position+Vector3.up, 0.2f);
    }
}

#region States
public abstract class playerState
{
    abstract public void OnUpdate(pl_movement pl);
    abstract public void OnFixedUpdate(pl_movement pl);
    abstract public void OnStart(pl_movement pl);
    abstract public void OnEnd(pl_movement pl);

}
public class PsCrouch : playerState
{
    override public void OnStart(pl_movement pl)
    {
        pl.CrouchStats();
    }
    override public void OnUpdate(pl_movement pl)
    {
        pl.ToggleCrouch();
        pl.Jump();
    }
    override public void OnFixedUpdate(pl_movement pl)
    {
        pl.Walk();
    }
    
    override public void OnEnd(pl_movement pl)
    {

    }
}

public class PsStanding : playerState
{
    override public void OnStart(pl_movement pl)
    {
        pl.StandingStats();
    }
    override public void OnUpdate(pl_movement pl)
    {
        pl.ToggleRun();
        pl.ToggleCrouch();
        pl.Jump();
    }
    override public void OnFixedUpdate(pl_movement pl)
    {
        pl.Walk();
    }
    
    override public void OnEnd(pl_movement pl)
    {

    }
}

#endregion
