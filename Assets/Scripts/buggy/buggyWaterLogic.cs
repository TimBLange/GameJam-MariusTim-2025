using UnityEngine;

public class buggyWaterLogic : MonoBehaviour, IInteractable
{
    pl_inventory plI;
    AudioSource aS;
    private void Start()
    {
        aS = GetComponent<AudioSource>();
        plI = GameObject.FindWithTag("Player").GetComponent<pl_inventory>();
        
    }
    public void Interact()
    {
        if (plI.equipmentInHand.CompareTag("Broom"))
        {
            aS.Play();
            plI.broomBloodMeterCurrent = 0;
        }
    }
}
