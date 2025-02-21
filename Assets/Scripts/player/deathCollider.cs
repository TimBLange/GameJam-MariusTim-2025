using UnityEngine;

public class deathCollider : MonoBehaviour
{
    [SerializeField]AudioSource aS;
    [SerializeField] AudioClip[] audios;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out pl_dieLogic dL))
        {
            aS.clip = audios[Random.Range(0, audios.Length - 1)];
            aS.Play();
            dL.Die();
        }
    }
}
