using UnityEngine;

public class pl_inventory : MonoBehaviour
{
    [Header("Equipment")]
    public bool equipmentInHandBool = false;
    [HideInInspector] public GameObject equipmentInHand;
    [SerializeField] Transform equipmentPosition;

    InventoryState currentState;
    InventoryState empty = new EmptyState();
    InventoryState corpse = new CorpseState();
    InventoryState broom = new BroomState();
    InventoryState fireEx = new FireExState();

    public GameObject cam;
    [Header("FireEx")]
    [SerializeField] public ParticleSystem particleSys;
    [SerializeField] public float fireExRange;
    [SerializeField] public float FireExDamageInSeconds;
    
    [Header("Corpse")]
    [HideInInspector] public Rigidbody corpseRb;
    [HideInInspector] public Collider corpseCollider;
    [SerializeField] float corpseThrowForce;
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        SwitchState(empty);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this);
    }

    public void PutInHand(GameObject newEquip)
    {
        

        switch (newEquip.tag)
        {
            case "FireEx":
                equipmentInHand = Instantiate(newEquip, equipmentPosition.position, equipmentPosition.rotation, equipmentPosition);
                SwitchState(fireEx);
                particleSys = equipmentInHand.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
                break;
            case "Corpse":
                equipmentInHand = newEquip;
                corpseRb=newEquip.GetComponent<Rigidbody>();
                corpseCollider = newEquip.GetComponent<Collider>();
                corpseRb.isKinematic = true;
                corpseCollider.enabled = false;
                newEquip.transform.position = equipmentPosition.position;
                newEquip.transform.rotation = equipmentPosition.rotation;
                newEquip.transform.parent = equipmentPosition;

                SwitchState(corpse);
                break;
            case "Broom":
                SwitchState(broom);
                break;
            default:
                SwitchState(empty);
                break;
        }
        
    }
    public void TakeFromHand()
    {
        switch (equipmentInHand.tag)
        {
            case "FireEx":
                particleSys = null;
                Destroy(equipmentInHand);
                break;
            case "Corpse":
                equipmentInHand.transform.parent = null;
                corpseCollider.enabled = true;
                corpseRb.isKinematic = false;
                corpseRb.AddForce(cam.transform.forward*corpseThrowForce, ForceMode.Impulse);
                equipmentInHand.transform.parent = null;
                corpseRb = null;
                break;
            case "Broom":
                Destroy(equipmentInHand);
                break;
            default:
                SwitchState(empty);
                break;
        }
        
        SwitchState(empty);
    }

    public void TrashCorpse()
    {
        Destroy(equipmentInHand);
        
        SwitchState(empty);
    }
    public void SwitchState(InventoryState newState)
    {
        if (currentState != null)
        {
            currentState.OnEnd(this);
        }
        currentState = newState;
        currentState.OnStart(this);
    }
}

public abstract class InventoryState
{
    public abstract void OnStart(pl_inventory pI);
    public abstract void OnUpdate(pl_inventory pI);
    public abstract void OnEnd(pl_inventory pI);

}

public class FireExState : InventoryState
{
    public override void OnStart(pl_inventory pI)
    {
        Debug.Log("FireExStateOn");
        pI.equipmentInHandBool = true;
    }
    public override void OnUpdate(pl_inventory pI) 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Use FireEx");
            pI.particleSys.Play();
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Stop Use FireEx");
            pI.particleSys.Stop();
        }

        if (Input.GetMouseButton(0))
        {
            Ray r = new Ray(pI.cam.transform.position, pI.cam.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, pI.fireExRange))
            {

                if (hitInfo.collider.gameObject.TryGetComponent(out fireLogic fL))
                {
                    fL.Extinguish(pI.FireExDamageInSeconds);

                }
            }
        }
    }
    public override void OnEnd(pl_inventory pI)
    {
        Debug.Log("FireExStateOff");
        
    }
}

public class BroomState : InventoryState
{
    public override void OnStart(pl_inventory pI) 
    {
        Debug.Log("BroomStateON");
        pI.equipmentInHandBool = true;
    }
    public override void OnUpdate(pl_inventory pI) { }
    public override void OnEnd(pl_inventory pI) 
    {
        Debug.Log("BroomStateOFF");
    }
}
public class CorpseState : InventoryState
{
    public override void OnStart(pl_inventory pI)
    {
        Debug.Log("CorpseStateOn");
        pI.equipmentInHandBool = true;
    }
    public override void OnUpdate(pl_inventory pI)
    {
        if (Input.GetMouseButtonDown(0))
        {
            pI.TakeFromHand();
        }
    }
    public override void OnEnd(pl_inventory pI) 
    {
        Debug.Log("CorpseStateOFF");
    }
}
public class EmptyState : InventoryState
{
    public override void OnStart(pl_inventory pI)
    {
        Debug.Log("EmptyStateOn");
        pI.equipmentInHandBool = false;
    }
    public override void OnUpdate(pl_inventory pI) { }
    public override void OnEnd(pl_inventory pI)
    {
        Debug.Log("EmptyStateOFF");
    }
}
