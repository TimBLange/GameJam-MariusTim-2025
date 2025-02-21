using UnityEngine;

public class endingButtonLogic : MonoBehaviour,IInteractable
{
    bool canBePressed;
    void Start()
    {
        canBePressed = false;
    }

    public void Interact()
    {
        if (canBePressed)
        {
            Debug.Log("YOU HAVE WON THE GAME");
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
