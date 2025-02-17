using UnityEngine;
using UnityEngine.Audio;

public class soundscape_manager : MonoBehaviour
{
    [Tooltip("Add new soundscapes there. Mind that soundscapes do not persist if you close the game, but do persist between levels. So if you want to add new sounds, reset them to default once you finish the chamber or exit the game.")]
    public Soundscape[] TempSoundscapes;
    public static Soundscape[] soundscapes;
    [System.Serializable]
    public struct Soundscape
    {
        [Tooltip("Is it an SFX, Music or none for previous 2 (Master)?")]
        public AudioMixerGroup group;
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
        [Tooltip("Volume of a sound")]
        public float volume;
        [Tooltip("Is sound looped?")]
        public bool Loop;
        [Tooltip("Does this soundscape is using multiple sound files (example: footsteps, object colission.")]
        public bool Modular;
        [Tooltip("Audio files for Modular soundscapes.")]
        public AudioClip[] ExtraClips;
    }
    public void Awake()
    {
        soundscapes = TempSoundscapes;
        Debug.Log("Successfully loaded all soundscapes!");
    }
    public void Refresh()
    {
        soundscapes = TempSoundscapes;
        Debug.Log("Successfully refreshed all soundscapes!");
    }
}