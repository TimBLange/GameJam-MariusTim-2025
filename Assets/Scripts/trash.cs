using UnityEngine;

public class trash : MonoBehaviour
{
    
    void Awake()
    {

        trashCountManager.instance.CalcTrash();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
