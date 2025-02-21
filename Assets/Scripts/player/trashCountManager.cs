using UnityEngine;
using UnityEngine.UI;

public class trashCountManager : MonoBehaviour
{
    GameObject[] trashList;

    [SerializeField] Slider trashSlider;
    [SerializeField] TMPro.TextMeshProUGUI percenttageTMP;
    [SerializeField] endingButtonLogic eBL;
    private float currentTrashCount;
    private float lastTrashCount;
    private float maxTrashCount;
    private float currentCleanedTrash;
    private float currentPercent;
    public static trashCountManager instance;
   
    void Awake()
    {
        currentTrashCount = 0;
        currentCleanedTrash = 0;
        maxTrashCount = 0;
        currentPercent =0;
        CalcTrash();

        if (instance == null)
        {
            instance = this;
        }
    }

    public void CountTrash()
    {
        lastTrashCount = currentTrashCount;
        currentTrashCount = 0;
        CountBlood(); CountFire(); CountCorpse(); CountFTS();
        Debug.Log(currentTrashCount + currentCleanedTrash);
        if (lastTrashCount < currentTrashCount) 
        maxTrashCount += Mathf.Abs(lastTrashCount - currentTrashCount);


    }
    public void CalcTrash()
    {
        CountTrash();
        currentPercent = ( currentCleanedTrash / maxTrashCount) * 100;
        /*currentPercent = Mathf.Clamp(currentPercent, 0, 100);*/
        Debug.Log("trashCount  "+ maxTrashCount);
        Debug.Log("cleanedtrashCount  " + currentCleanedTrash);
        Debug.Log("Percentage " + currentPercent);
        trashSlider.value = currentPercent;
        percenttageTMP.text = currentPercent.ToString("0") + "%";
        if (currentPercent >= 100)
        {
            eBL.ActivateButton();
        }
    }
    private void CountBlood()
    {
        trashList = GameObject.FindGameObjectsWithTag("Blood");
        currentTrashCount += trashList.Length;
    }
    private void CountFire()
    {
        trashList = GameObject.FindGameObjectsWithTag("Fire");
        currentTrashCount += trashList.Length;
    }
    private void CountCorpse()
    {
        trashList = GameObject.FindGameObjectsWithTag("Corpse");
        currentTrashCount += trashList.Length;
    }
    private void CountFTS()
    {
        trashList = GameObject.FindGameObjectsWithTag("FullTrashSack");
        currentTrashCount += trashList.Length;
    }
   
    public void CleanedTrashUp()
    {
        currentCleanedTrash += 1;
        
        CalcTrash();
    }

    
    
    
}
