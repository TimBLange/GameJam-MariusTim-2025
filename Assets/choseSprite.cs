using UnityEngine;

public class choseSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer sR;
    [SerializeField] Sprite[] sprites;
    void Awake()
    {
        sR.sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
