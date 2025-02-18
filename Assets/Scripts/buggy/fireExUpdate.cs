using UnityEngine;
using TMPro;
public class fireExUpdate : MonoBehaviour
{
    [SerializeField] public TMPro.TextMeshPro FuelText;

  
    public void UpdateTMP(float d)
    {
        Debug.Log("Update TMPFuel");
        FuelText.text = d.ToString();
    }
}
