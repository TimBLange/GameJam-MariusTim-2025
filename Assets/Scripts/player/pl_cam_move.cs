using UnityEngine;
using UnityEngine.UI;
interface IInteractable
{
    public void Interact();
}
public class pl_cam_move : MonoBehaviour
{
    GameObject cam;
    [SerializeField]float sens;
    float currentY =0;

    
    [SerializeField]
    [Range(1, 10)]
    float interactorRange;
    [SerializeField] RawImage recticle;
    [SerializeField] Texture recticleNormal;
    [SerializeField] Texture recticleActive;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() //Yo, wenn man das auf FixedUpdate stellt statt nur Update dann is das jittering weg aber man muss die sens neu konfigurieren
                // (er meinte auch, dass alles was physics benutzt in FixedUpdate sollte)
    {
        float inputX = Input.GetAxis("Mouse X") * sens;
        float inputY = Input.GetAxis("Mouse Y") * sens;
        currentY -= inputY;
        currentY = Mathf.Clamp(currentY, -60, 60);
        cam.transform.localEulerAngles = Vector3.right * currentY;
        transform.localEulerAngles += Vector3.up * inputX ;

        Debug.DrawRay(cam.transform.position, cam.transform.forward * interactorRange);

        Ray r = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactorRange))
        {

            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
            {
                recticle.texture = recticleActive;
                /*Debug.Log(interactObject);*/
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObject.Interact();
                }
            }

        }
        else
        {
            recticle.texture = recticleNormal;
        }

    }

}
