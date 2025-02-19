using UnityEngine;
using UnityEngine.UI;

public class trashCountManager : MonoBehaviour
{
    GameObject[] trashList;

    [SerializeField] Slider trashSlider;
    [SerializeField] TMPro.TextMeshProUGUI percenttageTMP;


    private float trashCount;
    private float currentCleanedTrash;
    private float currentPercent;
    public static trashCountManager instance;
   
    void Awake()
    {
        currentCleanedTrash = 0;
        trashCount = 0;
        currentPercent =0;
        CalcTrash();

        if (instance == null)
        {
            instance = this;
        }
    }



    public void CountTrash()
    {
        Debug.Log("trashCount in CountTrash() "+trashCount);
        trashCount = 0;
        CountBlood(); CountFire(); CountCorpse(); CountFTS();
        

    }
    public void CalcTrash()
    {
        CountTrash();
        currentPercent = (currentCleanedTrash / trashCount) * 100;
        currentPercent = Mathf.Clamp(currentPercent, 0, 100);
        Debug.Log("trashCount in CalcTrash() "+trashCount);
        Debug.Log("Percentage in CalcTrash() " + currentPercent);
        trashSlider.value = currentPercent;
        percenttageTMP.text = currentPercent.ToString("0") + "%";
    }
    private void CountBlood()
    {
        trashList = GameObject.FindGameObjectsWithTag("Blood");
        trashCount += trashList.Length;
    }
    private void CountFire()
    {
        trashList = GameObject.FindGameObjectsWithTag("Fire");
        trashCount += trashList.Length;
    }
    private void CountCorpse()
    {
        trashList = GameObject.FindGameObjectsWithTag("Corpse");
        trashCount += trashList.Length;
    }
    private void CountFTS()
    {
        trashList = GameObject.FindGameObjectsWithTag("FullTrashSack");
        trashCount += trashList.Length;
    }
   
    public void CleanedTrashUp()
    {
        currentCleanedTrash += 1;
        Debug.Log("currentCleanedTrash in CleanedTrashUp "+currentCleanedTrash);
        CalcTrash();
    }
    
    
}
