using UnityEngine;

public class deathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out pl_dieLogic dL))
        {
            dL.Die();
        }
    }
}
