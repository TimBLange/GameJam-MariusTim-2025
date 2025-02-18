using UnityEngine;
using System.Collections;
using TMPro;
public class pl_dieLogic : MonoBehaviour
{

    [SerializeField] Transform respawnPosition;
    [SerializeField] int janitorCount;
    [SerializeField] TMPro.TextMeshProUGUI janitorTMP;
    [SerializeField] GameObject redScreen;
    [SerializeField] GameObject blood;
    [SerializeField] GameObject corpse;
    Animator anim;
    private pl_movement plMovevement;
    private pl_cam_move plCamMovement;
    
    void Start()
    {
        anim = redScreen.GetComponent<Animator>();
        plMovevement = GetComponent<pl_movement>();
        plCamMovement = GetComponent<pl_cam_move>();
        
        UpdateJanitorCountTMP();
        resetPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Die();
        }
    }
    public void Die()
    {
        StartCoroutine(dieCoroutine());


        for (int i = 0; i < 3; i++)
        {
            GameObject c = Instantiate(corpse, transform.position + transform.up*1.5f, Quaternion.identity);

        }
        GameObject b = Instantiate(blood, transform.position+transform.up, Quaternion.identity);


    }
    private void resetPosition()
    {
        transform.position = respawnPosition.position;
        transform.rotation = respawnPosition.rotation;
        anim.SetBool("died", false);
    }
    private void UpdateJanitorCountTMP()
    {
        janitorTMP.text = $"Janitor #{janitorCount}";
    }

    IEnumerator dieCoroutine()
    {
        
        anim.SetBool("died", true);
        janitorCount++;
        plMovevement.enabled = false;
        plCamMovement.enabled = false;
        yield return new WaitForSecondsRealtime(2f);
        resetPosition();
        UpdateJanitorCountTMP();
        
        plMovevement.enabled = true;
        plCamMovement.enabled = true;
        
    }

}
