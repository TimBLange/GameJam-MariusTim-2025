using UnityEngine;

public class buggySpawnLogic : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject spawnPlatform;
    GameObject buggy;
    buggyMove buggyM;
    private void Awake()
    {
        buggy = GameObject.FindGameObjectWithTag("Buggy");
        buggyM = buggy.GetComponent<buggyMove>();
        SpawnBuggy();
        
    }
    public void Interact()
    {
        SpawnBuggy();
    }
    
    private void SpawnBuggy()
    {
        buggyM.UpdateBuggyPingPoint(spawnPlatform.transform.position);
        buggyM.SwitchState(buggyM.buggyStand);
        buggy.transform.position = spawnPlatform.transform.position + transform.up*1.2f;
        buggy.transform.rotation = spawnPlatform.transform.rotation;
        
        
    }
}
