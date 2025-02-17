using UnityEngine;

public class pl_inventory : MonoBehaviour
{
    
    [HideInInspector] public bool equipmentInHandBool = false;
    [HideInInspector] public GameObject equipmentInHand;
    [SerializeField] Transform equipmentPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutInHand(GameObject newEquip)
    {
        equipmentInHandBool = true;
        equipmentInHand=Instantiate(newEquip, equipmentPosition.position, equipmentPosition.rotation, equipmentPosition);
    }
    public void TakeFromHand()
    {
        equipmentInHandBool = false;
        Destroy(equipmentInHand);
    }
}
