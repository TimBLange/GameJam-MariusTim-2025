using UnityEngine;

public class pl_interactor : MonoBehaviour
{
    GameObject interactorSource;
    [SerializeField]
    [Range(1,10)]
    float interactorRange;

    private void Awake()
    {
        interactorSource = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Update()
    {
        Debug.DrawRay(interactorSource.transform.position, interactorSource.transform.forward * interactorRange);

        Ray r = new Ray(interactorSource.transform.position, interactorSource.transform.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactorRange))
            {
            
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
                {
                /*Debug.Log(interactObject);*/
                if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactObject.Interact();
                    }
                }
            }
        
    }

    
}
