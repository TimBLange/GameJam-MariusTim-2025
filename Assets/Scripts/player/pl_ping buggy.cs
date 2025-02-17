using UnityEngine;

public class pl_pingbuggy : MonoBehaviour
{
    [SerializeField] Transform interactorSource;
    [SerializeField] GameObject buggyPingPoint;
    [SerializeField] buggyMove buggyLogic;
    GameObject currentBuggyPingPoint;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            
            if (Physics.Raycast(r, out RaycastHit hitInfo, Mathf.Infinity))
            {
                Debug.Log(hitInfo.point);
                if (currentBuggyPingPoint != null) 
                {
                    Destroy(currentBuggyPingPoint.gameObject);
                }

                currentBuggyPingPoint=Instantiate(buggyPingPoint, hitInfo.point, transform.rotation);
                buggyLogic.StartBuggyMove(hitInfo.point);
                
            }
        }
    }
}
