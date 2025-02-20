using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterAi : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public MonsterStates currentState;
    public MonsterStates patrol = new MonsterPatrol();
    public MonsterStates hunt = new MonsterHunt();
    public MonsterStates stand = new MonsterStand();
    [HideInInspector] public Vector3 nextDestination;
    [SerializeField] public float normalSpeed;
    [SerializeField] float maxDistance;
    [HideInInspector] public GameObject player;
    public pl_movement plMovement;
    [SerializeField] LayerMask layerM;
    public bool inTrigger;
    void Start()
    {
        inTrigger = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        plMovement = player.GetComponent<pl_movement>();
        SwitchStates(patrol);
    }
    private void LateUpdate()
    {
        currentState.OnUpdate(this);
    }

    public void SwitchStates(MonsterStates newState)
    {
        if (currentState != null)
        {
            currentState.OnEnd(this);
        }

        currentState = newState;
        currentState.OnStart(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }

    }
    public void Walk()
    {
        
        navMeshAgent.destination = nextDestination;
    }
    public bool CheckIfInSight()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position + transform.up * 1.5f, player.transform.position, out hit,layerM))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform==player.transform)
            {
                return true;
                
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("nothing");
            return false;
        }
        
    }
    public bool CheckInDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return maxDistance >= distance;
    }
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            
            Gizmos.DrawLine(transform.position + transform.up * 1.5f, player.transform.position);
        }
        
    }
    public void PlayerDetected()
    {
        StartCoroutine(PlayerDetectedEnum());
    }

    IEnumerator PlayerDetectedEnum()
    {
        SwitchStates(stand);
        navMeshAgent.destination = transform.position;
        gameObject.transform.LookAt(player.transform.position);
        yield return new WaitForSeconds(0.5f);
        nextDestination = player.transform.position;
        SwitchStates(hunt);
    }
    
}

public abstract class MonsterStates
{
    public abstract void OnStart(MonsterAi mAI);
    public abstract void OnUpdate(MonsterAi mAI);
    public abstract void OnEnd(MonsterAi mAI);
}

public class MonsterPatrol: MonsterStates
{
    public override void OnStart(MonsterAi mAI) 
    {
        Debug.Log("Patrol");
        mAI.navMeshAgent.speed = mAI.normalSpeed;
        mAI.Walk();
    }
    public override void OnUpdate(MonsterAi mAI) 
    {
        if (mAI.inTrigger && mAI.CheckIfInSight())
        {
            mAI.PlayerDetected();
        }
    }
    public override void OnEnd(MonsterAi mAI)
    {
        
    }
}
public class MonsterStand : MonsterStates
{
    public override void OnStart(MonsterAi mAI)
    {
        Debug.Log("Standing");

    }
    public override void OnUpdate(MonsterAi mAI)
    {

    }
    public override void OnEnd(MonsterAi mAI)
    {

    }
}



public class MonsterHunt : MonsterStates
{
    public override void OnStart(MonsterAi mAI)
    {
        Debug.Log("Hunting");
        mAI.navMeshAgent.speed = mAI.plMovement.runSpeed + 0.5f;
    }
    public override void OnUpdate(MonsterAi mAI) 
    {
        if (!mAI.CheckIfInSight() || !mAI.inTrigger) 
        {
            if(mAI.navMeshAgent.remainingDistance <= 0.5f)
            mAI.SwitchStates(mAI.patrol);
        }
        if (mAI.CheckIfInSight() && mAI.CheckInDistance())
        {
            mAI.nextDestination = mAI.player.transform.position;
        }
        
        mAI.Walk();
    }
    public override void OnEnd(MonsterAi mAI) { }
}
