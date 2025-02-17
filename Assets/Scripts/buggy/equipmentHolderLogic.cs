using UnityEngine;

public class equipmentHolderLogic : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject equipmentOnBuggy;
    [SerializeField] pl_inventory pl_invent;

    public void Interact()
    {
        if (!pl_invent.equipmentInHandBool)
        {
            Debug.Log($"{gameObject} + {equipment.activeSelf}");
            pl_invent.PutInHand(equipment);
            equipmentOnBuggy.SetActive(false);
        }
        else
        {
            
            if (pl_invent.equipmentInHand.CompareTag(equipment.tag))
            {
                pl_invent.TakeFromHand();
                equipmentOnBuggy.SetActive(true);
            }
        }
    }
}
