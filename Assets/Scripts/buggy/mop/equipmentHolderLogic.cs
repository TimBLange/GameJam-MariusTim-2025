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
            Debug.Log($"{equipment.name}(Clone)");
            if (pl_invent.equipmentInHand.name == $"{equipment.name}(Clone)")
            {
                pl_invent.TakeFromHand();
                equipmentOnBuggy.SetActive(true);
            }
        }
    }
}
