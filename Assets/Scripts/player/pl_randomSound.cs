using UnityEngine;
using System.Collections;

public class pl_randomSound : MonoBehaviour
{
    [SerializeField] AudioClip[] audios;
    AudioSource aS;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        aS = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomSound());
    }

    IEnumerator PlayRandomSound()
    {
        transform.localPosition = player.transform.position - player.transform.forward * (-7) - player.transform.right * Random.Range(-10, 10);
        float waitFloat = Random.Range(20, 30);
        yield return new WaitForSeconds(waitFloat);
        aS.clip = audios[Random.Range(0, audios.Length - 1)];
        aS.Play();
        StartCoroutine(PlayRandomSound());
    }
}
