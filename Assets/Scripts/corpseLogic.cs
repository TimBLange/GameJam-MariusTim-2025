using UnityEngine;

public class corpseLogic : MonoBehaviour,IInteractable
{
    pl_inventory plI;

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
}
