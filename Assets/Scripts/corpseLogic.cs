using UnityEngine;

public class corpseLogic : MonoBehaviour,IInteractable
{
    [SerializeField] pl_inventory plI;

    public void Interact()
    {
        if (!plI.equipmentInHandBool)
        {
            Debug.Log("Pick Up Corpse");
            plI.PutInHand(this.gameObject);
        }
    }
}
