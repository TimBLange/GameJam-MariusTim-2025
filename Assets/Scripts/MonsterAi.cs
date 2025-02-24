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
    [SerializeField] public float runSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] float waitStand;
    [HideInInspector] public GameObject player;
    public pl_movement plMovement;
    [SerializeField] LayerMask layerM;
    public bool inTrigger;
    [SerializeField] public GameObject[] patrolPoints;
    [SerializeField] AudioClip[] monsterSounds;
    AudioSource aS;
    public Vector3 currentDestination;
    [SerializeField] public Animator aM;
    void Start()
    {
        aS = GetComponent<AudioSource>();
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
        inTrigger = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        plMovement = player.GetComponent<pl_movement>();
        runSpeed = plMovement.runSpeed + 2;
        normalSpeed = plMovement.walkSpeed;
        SwitchStates(patrol);

    }
    private void Update()
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
        if (Physics.Linecast(transform.position + transform.up * 1.5f, player.transform.position, out hit, layerM))
        {
            if (hit.transform == player.transform)
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
    public void NewPos()
    {
        SwitchStates(stand);
        StartCoroutine(NewPosEnum());

    }
    IEnumerator NewPosEnum()
    {
        yield return new WaitForSecondsRealtime(waitStand);
        Transform closestPoint = null;
        float shortestDistance = Mathf.Infinity;

        
            foreach (GameObject point in patrolPoints)
            {

                float distance = Vector3.Distance(transform.position, point.transform.position);

                if (distance < shortestDistance && point.transform.position != nextDestination && Random.Range(0, 10) <= 4)
                {

                    shortestDistance = distance;
                    closestPoint = point.transform;


                }


            }


            if (closestPoint != null)
            {
                nextDestination = closestPoint.position;
            }
            else
            {
                nextDestination = patrolPoints[Random.Range(0, patrolPoints.Length)].transform.position;
            }
        
        SwitchStates(patrol);

    }
    public void PlayerDetected()
    {
        StartCoroutine(PlayerDetectedEnum());
    }

    public void currentBuggyPos(Transform buggy)
    {
        StopAllCoroutines();
        StartCoroutine(currentBuggyPosEnum(buggy));
        Debug.Log("buggy");
        
    }

    IEnumerator currentBuggyPosEnum(Transform buggy)
    {
        yield return new WaitForSeconds(0.1f);
        SwitchStates(stand);
        nextDestination = buggy.position;
        SwitchStates(patrol);
    }
    IEnumerator PlayerDetectedEnum()
    {

        SwitchStates(stand);
        PlaySound();
        navMeshAgent.destination = transform.position;
        gameObject.transform.LookAt(player.transform.position);
        yield return new WaitForSeconds(0.5f);
        nextDestination = player.transform.position;
        SwitchStates(hunt);
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
    }
    public void PlaySound()
    {
        aS.clip = monsterSounds[Random.Range(0, monsterSounds.Length - 1)];
        aS.Play();
    }
}

public abstract class MonsterStates
{
    public abstract void OnStart(MonsterAi mAI);
    public abstract void OnUpdate(MonsterAi mAI);
    public abstract void OnEnd(MonsterAi mAI);
}

public class MonsterPatrol : MonsterStates
{
    public override void OnStart(MonsterAi mAI)
    {
        mAI.aM.SetBool("walk", true);
        mAI.aM.SetBool("run", false);
        Debug.Log("Patrol");
        mAI.navMeshAgent.speed = mAI.normalSpeed;
        mAI.Walk();
    }
    public override void OnUpdate(MonsterAi mAI)
    {
        
        if (mAI.navMeshAgent.remainingDistance <= 0.1f)
        {
            Debug.Log("arrived at point");
            mAI.NewPos();
        }


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
        mAI.aM.SetBool("walk", false);
        mAI.aM.SetBool("run", false);

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
        mAI.aM.SetBool("walk", false);
        mAI.aM.SetBool("run", true);
        Debug.Log("Hunting");
        mAI.navMeshAgent.speed = mAI.runSpeed;
    }
    public override void OnUpdate(MonsterAi mAI)
    {
        if ( mAI.CheckInDistance())
        {
            mAI.nextDestination = mAI.player.transform.position;
        }
        if (!mAI.CheckIfInSight() || !mAI.inTrigger)
        {
            if (mAI.navMeshAgent.remainingDistance <= 0.5f)
                mAI.NewPos();
        }
        
        

        mAI.Walk();
    }
    public override void OnEnd(MonsterAi mAI)
    {
        mAI.PlaySound();
    }
}
