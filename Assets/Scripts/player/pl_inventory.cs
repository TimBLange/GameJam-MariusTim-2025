using UnityEngine;
using UnityEngine.UI;


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
    InventoryState fullTrashSack = new FullTrashSackState();

    public GameObject cam;
    [Header("FireEx")]
    [SerializeField] public ParticleSystem particleSys;
    [SerializeField] public float fireExRange;
    [SerializeField] public float FireExDamageInSeconds;
    [SerializeField] public float FireExFuelMax;
    [SerializeField] public float FireExFuelCurrent;
    [SerializeField] public float FireExFuelMultiplier;
    [SerializeField] public TMPro.TextMeshProUGUI fireTMP;
    [SerializeField] public AudioClip firePickUpSound;
    [SerializeField] public AudioClip fireUseSound;
    [SerializeField] public AudioClip PutDownSound;
    [Header("Throwables")]
    [SerializeField] float throwForce;
    [HideInInspector] public Rigidbody throwableRb;
    [HideInInspector] public Collider throwableCollider;
    [SerializeField] public AudioClip corpsePickUp;
    [SerializeField] public AudioClip corpseThrow;
    [Header("Broom")]
    [SerializeField] public float broomRange;
    [SerializeField] public int broomBloodMeterMax;
    public int broomBloodMeterCurrent;
    [SerializeField] public GameObject bloodPrefab;
    [SerializeField] public AudioClip bloodSound;
    [SerializeField] public AudioClip broomPutDownSound;

    public AudioSource aS;
    
    void Awake()
    {
        aS = GetComponent<AudioSource>();
        FireExFuelCurrent = FireExFuelMax;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        SwitchState(empty);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this);
        if (currentState != fireEx && FireExFuelCurrent < FireExFuelMax)
        {
            ReloadFireEx();
        }

    }

    public void PutInHand(GameObject newEquip)
    {


        switch (newEquip.tag)
        {
            case "FireEx":
                equipmentInHand = Instantiate(newEquip, equipmentPosition.position, equipmentPosition.rotation, equipmentPosition);
                SwitchState(fireEx);
                particleSys = equipmentInHand.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
                aS.clip = firePickUpSound;
                break;
            case "Corpse":
                aS.clip = corpsePickUp;
                equipmentInHand = newEquip;
                throwableRb = newEquip.GetComponent<Rigidbody>();
                throwableCollider = newEquip.GetComponent<Collider>();
                throwableRb.isKinematic = true;
                throwableCollider.enabled = false;
                newEquip.transform.position = equipmentPosition.position;
                newEquip.transform.rotation = equipmentPosition.rotation;
                newEquip.transform.parent = equipmentPosition;

                SwitchState(corpse);
                break;
            case "FullTrashSack":
                aS.clip = corpsePickUp;
                if (!newEquip.TryGetComponent(out testScript tS))
                {
                    equipmentInHand = newEquip;
                    newEquip.transform.position = equipmentPosition.position;
                    newEquip.transform.rotation = equipmentPosition.rotation;
                    newEquip.transform.parent = equipmentPosition;
                }
                else
                {
                    equipmentInHand = Instantiate(newEquip, equipmentPosition.position, equipmentPosition.rotation, equipmentPosition);
                    Destroy(equipmentInHand.GetComponent<testScript>());
                }
                throwableRb = equipmentInHand.GetComponent<Rigidbody>();
                throwableCollider = equipmentInHand.GetComponent<Collider>();
                throwableRb.isKinematic = true;
                throwableCollider.enabled = false;
                SwitchState(fullTrashSack);
                break;
            case "Broom":
                equipmentInHand = Instantiate(newEquip, equipmentPosition.position, equipmentPosition.rotation, equipmentPosition);
                aS.clip = broomPutDownSound;
                SwitchState(broom);
                
                break;
            default:
                SwitchState(empty);
                break;
        }
        aS.Play();
    }
    public void TakeFromHand()
    {
        switch (equipmentInHand.tag)
        {
            case "FireEx":
                particleSys = null;
                Destroy(equipmentInHand);
                aS.clip = PutDownSound;
                break;
            case "Corpse":
                aS.clip = corpseThrow;
                equipmentInHand.transform.parent = null;
                throwableCollider.enabled = true;
                throwableRb.isKinematic = false;
                throwableRb.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
                equipmentInHand.transform.parent = null;
                throwableRb = null;
                break;
            case "FullTrashSack":
                aS.clip = corpseThrow;
                equipmentInHand.transform.parent = null;
                throwableCollider.enabled = true;
                throwableRb.isKinematic = false;
                throwableRb.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
                equipmentInHand.transform.parent = null;
                throwableRb = null;
                break;
            case "Broom":
                aS.clip = broomPutDownSound;
                Destroy(equipmentInHand);
                
                break;
            default:
                SwitchState(empty);
                break;
        }

        aS.Play();
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
    public void ReloadFireEx()
    {
        FireExFuelCurrent += FireExDamageInSeconds * Time.deltaTime * FireExFuelMultiplier;
        FireExFuelCurrent = Mathf.Clamp(FireExFuelCurrent, 0, FireExFuelMax);

    }
    public void UnloadFireEx()
    {
        FireExFuelCurrent -= FireExDamageInSeconds * Time.deltaTime * FireExFuelMultiplier;
        FireExFuelCurrent = Mathf.Clamp(FireExFuelCurrent, 0, FireExFuelMax);


        

    }

    public void SpawnNewBlood(RaycastHit hitInfo)
    {
        
        GameObject blood = Instantiate(bloodPrefab, hitInfo.point+Vector3.up, Quaternion.identity);
        broomBloodMeterCurrent--;
        trashCountManager.instance.CalcTrash();
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
        pI.fireTMP.gameObject.SetActive(true);
        Debug.Log("FireExStateOn");
        pI.equipmentInHandBool = true;
        pI.fireTMP.text = pI.FireExFuelCurrent.ToString("0")+"%";
    }
    public override void OnUpdate(pl_inventory pI)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pI.FireExFuelCurrent > 0)
            {
                pI.aS.clip = pI.fireUseSound;
                pI.aS.Play();
            }
            
            Debug.Log("Use FireEx");
            pI.particleSys.Play();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            pI.aS.Pause();
            Debug.Log("Stop Use FireEx");
            pI.particleSys.Stop();
        }

        if (Input.GetMouseButton(0) && pI.FireExFuelCurrent > 0)
        {
            pI.UnloadFireEx();
            pI.fireTMP.text = pI.FireExFuelCurrent.ToString("0") + "%";
            Ray r = new Ray(pI.cam.transform.position, pI.cam.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, pI.fireExRange))
            {

                if (hitInfo.collider.gameObject.TryGetComponent(out fireLogic fL))
                {
                    fL.Extinguish(pI.FireExDamageInSeconds);

                }
            }
        }
        else if (Input.GetMouseButton(0) && pI.FireExFuelCurrent <= 0)
        {
            pI.particleSys.Stop();
        }
    }
    public override void OnEnd(pl_inventory pI)
    {
        Debug.Log("FireExStateOff");
        pI.fireTMP.gameObject.SetActive(false);
    }
}

public class BroomState : InventoryState
{
    public override void OnStart(pl_inventory pI)
    {
        Debug.Log("BroomStateON");
        pI.equipmentInHandBool = true;
    }
    public override void OnUpdate(pl_inventory pI)
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log("Use Mop");
            Ray r = new Ray(pI.cam.transform.position, pI.cam.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, pI.broomRange))
            {
                
                if (pI.broomBloodMeterCurrent < pI.broomBloodMeterMax)
                {
                    if (hitInfo.collider.gameObject.TryGetComponent(out fireLogic fL) && pI.broomBloodMeterCurrent < pI.broomBloodMeterMax &&hitInfo.transform.CompareTag("Blood"))
                    {
                        pI.aS.clip = pI.bloodSound;
                        pI.aS.Play();
                        fL.CleanBlood();
                        pI.broomBloodMeterCurrent++;
                    }
                    
                }
                else
                {
                    pI.aS.clip = pI.bloodSound;
                    pI.aS.Play();
                    pI.SpawnNewBlood(hitInfo);

                }
                
                
            }






        }


    }
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
public class FullTrashSackState : InventoryState
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

