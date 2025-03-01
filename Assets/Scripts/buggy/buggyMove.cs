using UnityEngine;
using UnityEngine.AI;

public class buggyMove : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    public Vector3 buggyPingPoint;

    BuggyState currentState;
    public BuggyState buggyDrive = new BuggyDriveState();
    public BuggyState buggyStand = new BuggyStandState();
    public AudioSource aS;
    [SerializeField] public MonsterAi mAI;
    private void Awake()
    {
        
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
        UpdateBuggyPingPoint(currentBuggyPingPoint);
        SwitchState(buggyDrive);
    }

    public void UpdateBuggyPingPoint(Vector3 newPos)
    {
        buggyPingPoint = newPos;
        meshAgent.destination = buggyPingPoint;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PingPoint"))
        {
            SwitchState(buggyStand);
        }
    }
    public void setMonsterBuggy()
    {
        mAI.currentBuggyPos(this.gameObject.transform);
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
        bM.aS.volume = 0.100f;
        
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
        bM.setMonsterBuggy();
        bM.aS.volume = 0.225f;
    }
    public override void OnLateUpdate(buggyMove bM)
    {
        
        
       
    }
    public override void OnEnd(buggyMove bM)
    {

    }
}
