using UnityEngine;
using TMPro;

public class buggyTrashLogic : MonoBehaviour,IInteractable
{

    [SerializeField] GameObject fullTrashSack;
    [SerializeField] int maxCorpseCount;
    [SerializeField] int minCorpseSackCount;
    [SerializeField] int currentCorpseCount=0;
    [SerializeField] TMPro.TextMeshPro trashTMP;
    pl_inventory plI;

    private void Awake()
    {
        plI = GameObject.FindWithTag("Player").GetComponent<pl_inventory>();
        UpdateTMP();
    }
    public void Interact()
    {
        if (!plI.equipmentInHandBool)
        {
            if(currentCorpseCount >= minCorpseSackCount)
            {
                plI.PutInHand(fullTrashSack);
                currentCorpseCount = 0;
                UpdateTMP();

            }
            
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
        if (currentCorpseCount < minCorpseSackCount)
        {
            trashTMP.color = Color.red;
        }
        else
        {
            trashTMP.color = Color.green;
        }
        trashTMP.text = $"{currentCorpseCount}/{maxCorpseCount}";
    }
}
