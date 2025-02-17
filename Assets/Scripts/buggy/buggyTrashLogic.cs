using UnityEngine;
using TMPro;

public class buggyTrashLogic : MonoBehaviour,IInteractable
{
    
    
    [SerializeField] int maxCorpseCount;
    [SerializeField] int currentCorpseCount=0;
    [SerializeField] TMPro.TextMeshPro trashTMP;
    pl_inventory plI;

    private void Awake()
    {
        plI = GameObject.FindWithTag("Player").GetComponent<pl_inventory>();
    }
    public void Interact()
    {
        if (!plI.equipmentInHandBool)
        {
            currentCorpseCount = 0;
            UpdateTMP();
        }
        else
        {

            if (plI.equipmentInHand.CompareTag("Corpse"))
            {
                FillTrash();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Corpse"))
        {
            FillTrash();
        }
    }
    private void FillTrash()
    {
        if (currentCorpseCount != maxCorpseCount)
        {
            currentCorpseCount++;
            UpdateTMP();
            plI.TrashCorpse();
        }
        
    }

    private void UpdateTMP()
    {
        trashTMP.text = $"{currentCorpseCount}/{maxCorpseCount}";
    }
}
