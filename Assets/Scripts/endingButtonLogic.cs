using UnityEngine;

public class endingButtonLogic : MonoBehaviour,IInteractable
{
    bool canBePressed;
    sceneLoading scM;
    void Start()
    {
        scM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<sceneLoading>();
        canBePressed = false;
    }

    public void Interact()
    {
        if (canBePressed)
        {
            Debug.Log("YOU HAVE WON THE GAME");
            scM.LoadScene("Ending");
            canBePressed = false;
        }
        else
        Debug.Log("EHHH not finished yet");
    }
    public void ActivateButton()
    {
        canBePressed = true;
    }
}
