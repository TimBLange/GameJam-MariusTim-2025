using UnityEngine;

public class fireLogic : MonoBehaviour
{
    [SerializeField] ParticleSystem partSys;
    [SerializeField] float lifeInSeconds;
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
