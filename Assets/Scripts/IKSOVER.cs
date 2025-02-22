using UnityEngine;

public class IKSOVER : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] float footspacing;
    [SerializeField] float stepDistance;
    Vector3 NextPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(body.position + (body.right * footspacing), Vector3.down);
        if(Physics.Raycast(ray,out RaycastHit hit, 10, 7))
        {
            if (Vector3.Distance(NextPos, hit.point) > stepDistance)
            {
                NextPos = hit.point;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(NextPos, 0.5f);
    }
}
