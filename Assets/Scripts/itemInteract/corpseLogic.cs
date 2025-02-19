using UnityEngine;

public class corpseLogic : MonoBehaviour, IInteractable
{
    pl_inventory plI;
    [SerializeField] trashCountManager tcm;
    private void Awake()
    {
        plI = GameObject.FindWithTag("Player").GetComponent<pl_inventory>();
    }
    public void Interact()
    {
        if (!plI.equipmentInHandBool)
        {
            Debug.Log("Pick Up Corpse");
            plI.PutInHand(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("TrashCompactor"))
        {
            
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("I am destroyed");

        trashCountManager.instance.CleanedTrashUp();
    }
}
