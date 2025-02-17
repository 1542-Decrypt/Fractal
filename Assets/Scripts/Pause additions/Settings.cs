using NUnit.Framework;
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider[] Sliders;
    public TMP_Dropdown[] Lists;
    public Toggle[] Toggles;
    public AudioMixer mixer;
    public RenderTexture text;
    public Camera cam;
    public PostProcessVolume volume;
    public CharacterControl character;
    public GameObject CrosshairObj, RenderText;
    private bool DevComment = true;
    private bool Crosshair = true;
    private bool ReversedMouse = false;
    private bool Filter = true;
    private bool AO = true;
    private bool ChromAbbr = true;
    private bool Bloom = true;
    private bool Grain = true;
    private bool Vignette = true;
    private bool MuteWhenClosed = true;
    private bool DReflections = true;

    private float Sensivity = 1.0f;
    private float MasterVolume = 1.0f;
    private float MusicVolume = 1.0f;
    private float SoundVolume = 1.0f;

    private int Quality = 2;
    private int DisplayMode = 1;
    private int Captioning = 0;
    private int Resolution = 0;

    private int RevMouseMod;
    public static int Captions;
    public static bool MWC;

    private Bloom bloom = null;
    private AmbientOcclusion ao = null;
    private Grain grain = null;
    private ChromaticAberration ca = null;
    private Vignette vignette = null;
    private void Start()
    {
        AudioListener.volume = 0;
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out ao);
        volume.profile.TryGetSettings(out grain);
        volume.profile.TryGetSettings(out ca);
        volume.profile.TryGetSettings(out vignette);
        LoadSettings();
    }
    private void Update()
    {
        if (MWC && !Application.isFocused)
        {
            GetComponent<Pause>().PauseGame();
        }
    }
    public void Apply()
    {
        SettingsPrefs Prefs = new SettingsPrefs(Crosshair, ReversedMouse, Filter, AO, ChromAbbr, Bloom, Vignette, Grain, MuteWhenClosed, Sensivity, MasterVolume, SoundVolume, MusicVolume, Quality, DisplayMode, Captioning, Resolution, DevComment, DReflections);
        string settings = JsonUtility.ToJson(Prefs, true);
        File.WriteAllText(Application.persistentDataPath + "/options.json", settings);
        LoadSettings();
    }
    public void Cancel()
    {
        if (!File.Exists(Application.persistentDataPath + "/options.json"))
        {
            Crosshair = true;
            ReversedMouse = false;
            Filter = true;
            AO = true;
            ChromAbbr = true;
            Bloom = true;
            Grain = true;
            Vignette = true;
            MuteWhenClosed = true;
            DReflections = true;
            Sensivity = 2.0f;
            MasterVolume = 0f;
            MusicVolume = 0f;
            SoundVolume = 0f;
            Quality = 2;
            DisplayMode = 1;
            Captioning = 0;
        }
        else
        {
            LoadSettings();
        }
    }
    public void LoadSettings()
    {
        if (!File.Exists(Application.persistentDataPath + "/options.json"))
        {
            return;
        }
        string json = File.ReadAllText(Application.persistentDataPath + "/options.json");
        SettingsPrefs data = JsonUtility.FromJson<SettingsPrefs>(json);
        Sliders[0].value = data.Sensivity;
        Sliders[1].value = data.MasterVolume;
        Sliders[2].value = data.MusicVolume;
        Sliders[3].value = data.SoundVolume;
        Lists[0].value = data.Quality;
        Lists[1].value = data.DisplayMode;
        //Lists[2].value = data.Resolution;
        Lists[3].value = data.Captioning;
        Toggles[0].isOn = data.DevComment;
        Toggles[1].isOn = data.Crosshair;
        Toggles[2].isOn = data.ReversedMouse;
        Toggles[3].isOn = data.Filter;
        Toggles[4].isOn = data.AO;
        Toggles[5].isOn = data.ChromAbbr;
        Toggles[6].isOn = data.Bloom;
        Toggles[7].isOn = data.Vignette;
        Toggles[8].isOn = data.Grain;
        Toggles[9].isOn = data.MuteWhenClosed;
        Toggles[10].isOn = data.DReflections;
        if (CrosshairObj == null)
        {
            mixer.SetFloat("master", Mathf.Log10(data.MasterVolume) * 20);
            mixer.SetFloat("music", Mathf.Log10(data.MusicVolume) * 20);
            mixer.SetFloat("sfx", Mathf.Log10(data.SoundVolume) * 20);
        }
        else
        {
            mixer.SetFloat("master", Mathf.Log10(data.MasterVolume) * 20);
            mixer.SetFloat("music", Mathf.Log10(data.MusicVolume) * 20);
            mixer.SetFloat("sfx", Mathf.Log10(data.SoundVolume) * 20);
            print("Loaded Values");
            foreach (dev_comment DC in GameObject.FindObjectsByType<dev_comment>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                print("Name is " + DC.gameObject.name);
                DC.gameObject.SetActive(data.DevComment);
            }
            CrosshairObj.SetActive(data.Crosshair);
            ao.active = data.AO;
            bloom.active = data.Bloom;
            grain.active = data.Grain;
            ca.active = data.ChromAbbr;
            vignette.active = data.Vignette;
            foreach (ReflectionProbe reflections in GameObject.FindObjectsByType<ReflectionProbe>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (!data.DReflections)
                    reflections.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.OnAwake;
                else
                    reflections.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.EveryFrame;
            }
            if (!data.Filter)
            {
                cam.targetTexture = null;
                RenderText.SetActive(false);
            }
            else
            {
                cam.targetTexture = text;
                RenderText.SetActive(true);
            }
            if (data.ReversedMouse)
                RevMouseMod = -1;
            else if (!data.ReversedMouse)
            {
                RevMouseMod = 1;
            }
            character.lookSpeed = data.Sensivity * RevMouseMod;
            QualitySettings.SetQualityLevel(data.Quality);
            if (data.DisplayMode == 0)
            {
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            }
            else if (data.DisplayMode == 1)
            {
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }
            else if (data.DisplayMode == 2)
            {
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }
            if (data.Captioning == 0)
            {
                Captions = 0;
            }
            else if (data.Captioning == 1)
            {
                Captions = 1;
            }
            else if (data.Captioning == 2)
            {
                Captions = 2;
            }
            MWC = data.MuteWhenClosed;
        }
    }
    public void SetBool11(bool value)
    {
        DReflections = value;
    }
    public void SetBool0(bool value)
    {
        DevComment = value;
    }
    public void SetBool1(bool value)
    {
        Crosshair = value;
    }
    public void SetBool2(bool value)
    {
        ReversedMouse = value;
    }
    public void SetBool3(bool value)
    {
        Filter = value;
    }
    public void SetBool4(bool value)
    {
        AO = value;
    }
    public void SetBool5(bool value)
    {
        ChromAbbr = value;
    }
    public void SetBool6(bool value)
    {
        Bloom = value;
    }
    public void SetBool7(bool value)
    {
        Grain = value;
    }
    public void SetBool8(bool value)
    {
        Vignette = value;
    }
    public void SetBool9(bool value)
    {
        MuteWhenClosed = value;
    }
    public void SetFloat1(float value)
    {
        Sensivity = value;
    }
    public void SetFloat2(float value)
    {
        MasterVolume = value;
    }
    public void SetFloat3(float value)
    {
        MusicVolume = value;
    }
    public void SetFloat4(float value)
    {
        SoundVolume = value;
    }
    public void SetInt(int value)
    {
        Quality = value;
    }
    public void SetInt1(int value)
    {
        DisplayMode = value;
    }
    public void SetInt2(int value)
    {
        Captioning = value;
    }
    public void SetInt3(int value)
    {
        Resolution = value;
    }
}

[Serializable]
public class SettingsPrefs
{
    public string appVersion;

    public bool DevComment;
    public bool Crosshair;
    public bool ReversedMouse;
    public bool Filter;
    public bool AO;
    public bool ChromAbbr;
    public bool Bloom;
    public bool Grain;
    public bool Vignette;
    public bool MuteWhenClosed;
    public bool DReflections;

    public float Sensivity;
    public float MasterVolume;
    public float MusicVolume;
    public float SoundVolume;

    public int Quality;
    public int DisplayMode;
    public int Captioning;
    public int Resolution;

    public SettingsPrefs(bool Crshr, bool RevM, bool Fltr, bool AmbO, bool ChrAbbr, bool Blom, bool Grn, bool Vign, bool MWC, float Snsv, float MastVol, float MusVol, float SndVol, int Qual, int DM, int Capt, int Res, bool devComment, bool DReflect)
    {
        appVersion = Application.version;
        bool[] Bools = { Crshr, RevM, Fltr, AmbO, ChrAbbr, Blom, Grn, Vign, MWC, DReflect };
        float[] Floats = { Snsv, MastVol, MusVol, SndVol };
        int[] Ints = { Qual, DM, Capt, Res };
        Crosshair = Bools[0];
        ReversedMouse = Bools[1];
        Filter = Bools[2];
        AO = Bools[3];
        ChromAbbr = Bools[4];
        Bloom = Bools[5];
        Grain = Bools[6];
        Vignette = Bools[7];
        MuteWhenClosed = Bools[8];
        Sensivity = Floats[0];
        MasterVolume = Floats[1];
        MusicVolume = Floats[2];
        SoundVolume = Floats[3];
        Quality = Ints[0];
        DisplayMode = Ints[1];
        Captioning = Ints[2];
        Resolution = Ints[3];
        DevComment = devComment;
        DReflections = DReflect;
    }
}