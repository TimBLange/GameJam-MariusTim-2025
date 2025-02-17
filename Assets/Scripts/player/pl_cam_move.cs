using UnityEngine;

public class pl_cam_move : MonoBehaviour
{
    GameObject cam;
    [SerializeField]float sens;
    float currentY =0;
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * sens;
        float inputY = Input.GetAxis("Mouse Y") * sens;
        currentY -= inputY;
        currentY = Mathf.Clamp(currentY, -60, 60);
        cam.transform.localEulerAngles = Vector3.right * currentY;
        transform.localEulerAngles += Vector3.up * inputX ;
    }
}
