using UnityEngine;

public class buggyTrashLogic : MonoBehaviour,IInteractable
{
    
    [SerializeField] pl_inventory pl_invent;

    public void Interact()
    {
        if (!pl_invent.equipmentInHandBool)
        {
            
        }
        else
        {

            if (pl_invent.equipmentInHand.CompareTag("Corpse"))
            {
                pl_invent.TrashCorpse();
                
               
            }
        }
    }
}
