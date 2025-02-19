using UnityEngine;

public class ranodmizeBP : MonoBehaviour
{
    [SerializeField] Mesh[] meshes;
    [SerializeField] MeshFilter mR;
    void Awake()
    {
        mR.mesh = meshes[Random.Range(0, 4)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
