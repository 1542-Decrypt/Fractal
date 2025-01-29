using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

// im SUPER proud of this one as well as the whole soundscape system.
public class sound_node : MonoBehaviour
{
    public bool DoNotPlay;
    [Tooltip("The object that will emit sound. Will be set automatically to this objects parent if not set at first.")]
    public AudioSource AudiatedObject;
    private GameObject subtitle_object;
    bool Loop;
    AudioClip clip;
    public GameObject TextPrefab;
    public UnityEvent On_Sound_Finish;

    private bool isWaitingToPlay = false;
    private float DelayTimeGlobal;
    private bool delayPaused = false;
    public void Awake()
    {
        //Checking if sound node is a child to set audiated object to parent, bc I dont want to do it manually.
        if (base.transform.parent != null && AudiatedObject == null)
        {
            AudiatedObject = base.transform.parent.gameObject.GetComponent<AudioSource>();
        }
        subtitle_object = GameObject.Find("SubtitleHelper");
    }
    async public void PlayAudio(int SoundID)
    {
        if (DoNotPlay)
        {
            return;
        }
        try
        {
            Loop = soundscape_manager.soundscapes[SoundID].Loop;
        }
        catch (Exception e)
        {
            print(e + "happened for some reason ig");
        }
        //Default hold time. + Checking if sound should be looped or not.
        float SoundLength = 1000;
        clip = soundscape_manager.soundscapes[SoundID].sound;
        AudiatedObject.outputAudioMixerGroup = soundscape_manager.soundscapes[SoundID].group;
        if (Loop)
        {
            AudiatedObject.loop = true;
        }
        else
        {
            AudiatedObject.loop = false;
        }
        AudiatedObject.volume = soundscape_manager.soundscapes[SoundID].volume;
        AudiatedObject.clip = clip;
        SoundLength = AudiatedObject.clip.length * 1000;
        AudiatedObject.Play();
        //Preventing issues with looped sounds (yes, moving elevator, thats about you)
        if (soundscape_manager.soundscapes[SoundID].Modular)
        {
            int RandSound = UnityEngine.Random.Range(0, soundscape_manager.soundscapes[SoundID].ExtraClips.Length);
            AudiatedObject.clip = soundscape_manager.soundscapes[SoundID].ExtraClips[RandSound];
            AudiatedObject.Play();
        }
        WaitToFinish(Mathf.RoundToInt(SoundLength));
        if (Settings.Captions == 0 || soundscape_manager.soundscapes[SoundID].subtitles == false)
        {
            return;
        }
        if ((soundscape_manager.soundscapes[SoundID].subtitles && (Settings.Captions == 2 || Settings.Captions == 1) && soundscape_manager.soundscapes[SoundID].advanced != true) || (soundscape_manager.soundscapes[SoundID].advanced && Settings.Captions == 1))
        {
            //Checking if text is first in chain
            if (subtitle_handler.texts.Count == 0)
                subtitle_object.GetComponent<Animation>().Play("ShowSubt");
            //Setting text
            GameObject newtext = Instantiate(TextPrefab, subtitle_object.transform.GetChild(0));
            newtext.GetComponent<TextMeshProUGUI>().text = soundscape_manager.soundscapes[SoundID].text;
            SetColorNoAlpha(SoundID, newtext.GetComponent<TextMeshProUGUI>());
            subtitle_handler.texts.Add(newtext);
            print(subtitle_handler.texts.Count);
            //Setting holdTime for subtitle
            if (SoundLength > 1)
                SoundLength = AudiatedObject.clip.length * 1000;
            //Showing & hiding text
            newtext.GetComponent<Animation>().Play("ShowText");
            //4000 (+1000 default) is the default 5 second subtitle time.
            await Task.Delay((int)SoundLength + 4000);
            try
            {
                newtext.GetComponent<Animation>().Play("HideText");
            }
            catch
            {
                return;
            }
            //Checking if this is the last text in the chain to remove background with it
            if (subtitle_handler.texts.Count <= 1)
                subtitle_object.GetComponent<Animation>().Play("HideSubt");

            await Task.Delay(1000);
            //Removing text object from subtitle object to not stack them up
            subtitle_handler.texts.Remove(newtext);
            print(subtitle_handler.texts.Count);
            Destroy(newtext);
        }
    }
    async void WaitToFinish(int SoundLength)
    {
        await Task.Delay(SoundLength);
        On_Sound_Finish.Invoke();
    }
    public void StopAudio()
    {
        //Mostly unused but why dont have it.
        AudiatedObject.loop = false;
        AudiatedObject.Stop();
    }
    public void Disable()
    {
        DoNotPlay = true;
    }
    void SetColorNoAlpha(int SoundID, TextMeshProUGUI text)
    {
        //Ignoring alpha to not fuck stuff up
        Color col = text.color;
        col.r = soundscape_manager.soundscapes[SoundID].color.r;
        col.g = soundscape_manager.soundscapes[SoundID].color.g;
        col.b = soundscape_manager.soundscapes[SoundID].color.b;
        text.color = col;
    }
    public void PlayAudioDelay(int SoundID, int DelayTime)
    {
        if (!Pause.Paused)
        {
            DelayTimeGlobal = DelayTime;
            isWaitingToPlay = true;
            if (!isWaitingToPlay)
                PlayAudio(SoundID);
        }
    }
    public void ContinueAudio()
    {
        if (Pause.Paused)
        {
            AudiatedObject.Play();
        }
        if (delayPaused)
        {
            delayPaused = false;
        }
    }
    private void Update()
    {
        if (isWaitingToPlay && delayPaused != true)
        {
            float ContDelayTime = DelayTimeGlobal;
            if (ContDelayTime > 0)
            {
                ContDelayTime -= Time.deltaTime;
            }
            else
            {
                isWaitingToPlay = false;
            }
        }
    }
}
