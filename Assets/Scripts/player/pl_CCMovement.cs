using UnityEngine;

public class pl_CCMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    CharacterController characterC;
    Vector3 nextMov;
    void Start()
    {
        characterC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        nextMov = ((transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical")))*speed;
        characterC.Move(nextMov * Time.deltaTime);
    }
}
