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
    }
    public void Interact()
    {
        SpawnBuggy();
    }
    
    private void SpawnBuggy()
    {
        buggy.transform.position = spawnPlatform.transform.position + transform.up*2;
        buggy.transform.rotation = spawnPlatform.transform.rotation;
        buggyM.SwitchState(buggyM.buggyStand);
        buggyM.buggyPingPoint = transform.position;
    }
}
