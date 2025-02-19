using UnityEngine;

public class buggyWaterLogic : MonoBehaviour, IInteractable
{
    pl_inventory plI;

    private void Start()
    {
        plI = GameObject.FindWithTag("Player").GetComponent<pl_inventory>();
        
    }
    public void Interact()
    {
        if (plI.equipmentInHand.CompareTag("Broom"))
        {
            plI.broomBloodMeterCurrent = 0;
        }
    }
}
