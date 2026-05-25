using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip catMusic;
    [SerializeField] AudioSource AS;

    void Start()
    {
        AS.clip = catMusic;
        AS.loop = true;
        AS.Play();
    }
}
