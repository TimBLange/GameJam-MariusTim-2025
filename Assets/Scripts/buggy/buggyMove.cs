using UnityEngine;
using UnityEngine.AI;

public class buggyMove : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    public Vector3 buggyPingPoint;

    BuggyState currentState;
    public BuggyState buggyDrive = new BuggyDriveState();
    public BuggyState buggyStand = new BuggyStandState();
    public float startAcceleration;
    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        startAcceleration = meshAgent.acceleration;
        SwitchState(buggyStand);
    }

    private void LateUpdate()
    {
        currentState.OnLateUpdate(this);
        
    }

    public void SwitchState(BuggyState newState)
    {
        if (currentState != null)
        {
            currentState.OnEnd(this);
        }

        currentState = newState;
        currentState.OnStart(this);
    }

    public void StartBuggyMove(Vector3 currentBuggyPingPoint)
    {
        buggyPingPoint = currentBuggyPingPoint;
        SwitchState(buggyDrive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PingPoint"))
        {
            SwitchState(buggyStand);
        }
    }
}
public abstract class BuggyState
{
    public abstract void OnStart(buggyMove bM);
    public abstract void OnLateUpdate(buggyMove bM);
    public abstract void OnEnd(buggyMove bM);
}

public class BuggyStandState : BuggyState
{
    public override void OnStart(buggyMove bM)
    {
        Debug.Log("BuggyStand");
        bM.buggyPingPoint = bM.transform.position;
        bM.meshAgent.acceleration = 0;


    }
    public override void OnLateUpdate(buggyMove bM)
    {
        
    }
    public override void OnEnd(buggyMove bM)
    {

    }
}

public class BuggyDriveState : BuggyState
{
    public override void OnStart(buggyMove bM)
    {
        Debug.Log("BuggyDrives");
        bM.meshAgent.acceleration = bM.startAcceleration;
    }
    public override void OnLateUpdate(buggyMove bM)
    {
        
        bM.meshAgent.destination = bM.buggyPingPoint;
       
    }
    public override void OnEnd(buggyMove bM)
    {

    }
}
