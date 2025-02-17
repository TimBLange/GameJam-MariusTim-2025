using UnityEngine;


interface IInteractable
{
    public void Interact();
}
public class pl_interactor : MonoBehaviour
{
    [SerializeField] Transform interactorSource;
    [SerializeField]
    [Range(1,10)]
    float interactorRange;
    void Update()
    {
        Debug.DrawRay(interactorSource.position, interactorSource.forward * interactorRange);
        
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactorRange))
            {
            
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
                {
                Debug.Log(interactObject);
                if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactObject.Interact();
                    }
                }
            }
        
    }

    
}
