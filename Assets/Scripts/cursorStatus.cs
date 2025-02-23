using UnityEngine;

public class cursorStatus : MonoBehaviour
{
    [SerializeField] CursorLockMode lM;
    void Start()
    {
        Cursor.lockState = lM;
    }

    
}
