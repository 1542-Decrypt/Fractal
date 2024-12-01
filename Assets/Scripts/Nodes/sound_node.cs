using System.Threading.Tasks;
using UnityEngine;
using TMPro;

// im SUPER proud of this one as well as the whole soundscape system.
public class sound_node : MonoBehaviour
{
    [Tooltip("The object that will emit sound. Will be set automatically to this objects parent if not set at first.")]
    public AudioSource AudiatedObject;
    private GameObject subtitle_object;
    public bool Loop;
    AudioClip clip;
    public GameObject TextPrefab;
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
        //Default hold time. + Checking if sound should be looped or not.
        float SoundLength = 1000;
        clip = soundscape_manager.soundscapes[SoundID].sound;
        if (Loop)
        {
            AudiatedObject.loop = true;
        }
        else
        {
            AudiatedObject.loop = false;
        }
        AudiatedObject.clip = clip;
        AudiatedObject.Play();
        //Preventing issues with looped sounds (yes, moving elevator, thats about you)
        if (soundscape_manager.soundscapes[SoundID].advanced == false)
        {
            return;
        }
        //Checking if text is first in chain
        if (subtitle_handler.texts.Count == 0)
            subtitle_object.GetComponent<Animation>().Play("ShowSubt");
        //Setting text
        GameObject newtext = Instantiate(TextPrefab, subtitle_object.transform.GetChild(0));
        newtext.GetComponent<TextMeshProUGUI>().text = soundscape_manager.soundscapes[SoundID].text;
        SetColorNoAlpha(SoundID, newtext.GetComponent<TextMeshProUGUI>());
        subtitle_handler.texts.Add(newtext);
        //Setting holdTime for subtitle
        if (SoundLength > 1)
            SoundLength = AudiatedObject.clip.length * 1000;
        //Showing & hiding text
        newtext.GetComponent<Animation>().Play("ShowText");
        //4000 (+1000 default) is the default 5 second subtitle time.
        await Task.Delay((int)SoundLength + 4000);
        newtext.GetComponent<Animation>().Play("HideText");
        //Checking if this is the last text in the chain to remove background with it
        if (subtitle_handler.texts.Count <= 1)
            subtitle_object.GetComponent<Animation>().Play("HideSubt");

        await Task.Delay(1000);
        //Removing text object from subtitle object to not stack them up
        subtitle_handler.texts.Remove(newtext);
        Destroy(newtext);
    }
    public void StopAudio()
    {
        //Mostly unused by why dont have it.
        AudiatedObject.Stop();
        subtitle_object.GetComponent<Animation>().Play("HideSubt");
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
}
