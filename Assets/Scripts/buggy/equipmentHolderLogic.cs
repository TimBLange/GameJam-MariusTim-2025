using UnityEngine;

public class equipmentHolderLogic : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject equipmentOnBuggy;
    pl_inventory plI;

    private void Awake()
    {
        plI = GameObject.FindWithTag("Player").GetComponent<pl_inventory>();
    }
    public void Interact()
    {
        if (!plI.equipmentInHandBool)
        {
            Debug.Log($"{gameObject} + {equipment.activeSelf}");
            plI.PutInHand(equipment);
            equipmentOnBuggy.SetActive(false);
        }
        else
        {
            
            if (plI.equipmentInHand.CompareTag(equipment.tag))
            {
                plI.TakeFromHand();
                equipmentOnBuggy.SetActive(true);
            }
        }
    }
}
