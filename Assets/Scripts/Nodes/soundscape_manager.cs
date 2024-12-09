using UnityEngine;

public class soundscape_manager : MonoBehaviour
{
    public Soundscape[] TempSoundscapes;
    public static Soundscape[] soundscapes;
    [System.Serializable]
    public struct Soundscape
    {
        [Tooltip("What sound will be played")]
        public AudioClip sound;
        [Tooltip("Is only shown by advanced subtitles?")]
        public bool advanced;
        [Tooltip("Has subtitles by default")]
        public bool subtitles;
        [Tooltip("What will apppear in subtitles")]
        public string text;
        [Tooltip("Color of a subtitle")]
        public Color color;
        [Range(0f, 1f)]
        public float volume;
        public bool Loop;
        public bool Modular;
        public AudioClip[] ExtraClips;
    }
    public void Awake()
    {
        soundscapes = TempSoundscapes;
        Debug.Log("Successfully loaded all soundscapes!");
    }
}