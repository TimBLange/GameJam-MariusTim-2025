using UnityEngine;

public class trashCompactorLogic : MonoBehaviour
{
    pl_inventory plI;

    private void Awake()
    {
        plI = GameObject.FindWithTag("Player").GetComponent<pl_inventory>();
    }
    public void Interact()
    {
        if (!plI.equipmentInHandBool && plI.equipmentInHand.CompareTag("FullTrashSack"))
        {
            FillTrash();

        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.CompareTag("FullTrashSack"))
        {
            FillTrash();
        }
    }
    private void FillTrash()
    {
        
            plI.TrashCorpse();
        

    }
}
