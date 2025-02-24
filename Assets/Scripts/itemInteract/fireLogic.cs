using UnityEngine;

public class fireLogic : MonoBehaviour
{
    [SerializeField] ParticleSystem partSys;
    [SerializeField] float lifeInSeconds;
    [SerializeField] trashCountManager tcm;
    float startLifeTime;
    

    public void Extinguish(float damage)
    {
        if (lifeInSeconds <= 0)
        {
            Destroy(this.gameObject);
        }

        lifeInSeconds = lifeInSeconds - damage*Time.deltaTime;
        
    }
    public void CleanBlood()
    {
        Destroy(this.gameObject);


    }
    private void OnDestroy()
    {
        Debug.Log("I am destroyed");

        trashCountManager.instance.CleanedTrashUp();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position+Vector3.up, 0.25f);
    }
}
