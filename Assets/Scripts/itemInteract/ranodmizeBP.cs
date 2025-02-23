using UnityEngine;

public class ranodmizeBP : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    
    void Awake()
    {
        gameObjects[Random.Range(0, 5)].SetActive(true);
    }

    
}
