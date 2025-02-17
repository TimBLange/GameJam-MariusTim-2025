using UnityEngine;

public class pl_headbobbing : MonoBehaviour
{
    [SerializeField] GameObject pivotCam;

    [Range(0.001f, 0.01f)] [SerializeField] float amount = 0.002f;
    [Range(1f, 30f)] [SerializeField] float frequenzy = 10f;
    [Range(10f, 100f)] [SerializeField] float smooth = 10f;
    float mult=1.5f;
    float currentMult = 1f;
    Vector3 startPos;
    void Start()
    {
        startPos = pivotCam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForBobTrigger();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentMult = mult;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentMult = 1;
        }
    }

    private void CheckForBobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (inputMagnitude > 0)
        {
            StartBob();
        }
        else
        {
            StopBob();
        }
    }
    private void StopBob()
    {
        if (pivotCam.transform.localPosition == startPos) return;
        pivotCam.transform.localPosition = Vector3.Lerp(pivotCam.transform.localPosition, startPos, Time.deltaTime);
    }
    private Vector3 StartBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequenzy) * amount *1.4f*currentMult, smooth * Time.deltaTime);
        pos.y += Mathf.Lerp(pos.y, Mathf.Cos(Time.time * frequenzy/2) * amount * 1.4f * currentMult, smooth * Time.deltaTime);
        pivotCam.transform.localPosition += pos;
        return pos;
    }
}
