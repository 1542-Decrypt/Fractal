using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    public MiscStuff speaker;
    public CharacterControl fps;
    public string[] groundTypes;
    public AudioClip[] jump;
    public AudioClip[] tilewalk;
    public AudioClip[] waterwalk;
    public AudioClip[] dirtwalk;
    public AudioClip[] woodwalk;
    List<AudioClip[]> theBOYS = new List<AudioClip[]>();
    internal int currentSurface;
    int randomNumber;
    int lastNumber;
    int maxAttempts = 10;
    internal bool Disabled = false;
    private void Start()
    {
        theBOYS.Add(tilewalk);
        theBOYS.Add(waterwalk);
        theBOYS.Add(dirtwalk);
        theBOYS.Add(woodwalk);
    }
    public void PlayFootStep()
    {
        for (int i = 0; i < groundTypes.Length; i++)
        {
            if (groundTypes[i] == speaker.floortag)
            {
                currentSurface = i;
                break;
            }
        }
        if (Disabled != true)
        {
            for (int i = 0; randomNumber == lastNumber && i < maxAttempts; i++)
            {
                randomNumber = Random.Range(0, theBOYS[currentSurface].Length);
            }
            lastNumber = randomNumber;
            AudioSource audio = GetComponent<AudioSource>();
            gameObject.GetComponent<AudioSource>().clip = theBOYS[currentSurface][randomNumber];
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void PlayJump()
    {
        for (int i = 0; randomNumber == lastNumber && i < maxAttempts; i++)
        {
            randomNumber = Random.Range(0, 6);
        }
        lastNumber = randomNumber;
        AudioSource audio = GetComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().clip = jump[randomNumber];
        gameObject.GetComponent<AudioSource>().Play();
    }
}
