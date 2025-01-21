using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    public LaserGunLogic gun;
    public CharacterControl fps;
    public GameObject PauseUI;
    private sound_node[] SndNodes;
    public GameObject prefab, prefparent, noSavesText;
    public KeyCode PauseButton;

    public static int SaveIndex;
    private bool Paused;
    void Start()
    {
        SndNodes = FindObjectsByType<sound_node>(FindObjectsSortMode.None);
    }
    void PauseGame()
    {
        gun.EnabledLaser = false;
        Paused = true;
        fps.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        foreach (var node in SndNodes)
        {
            node.PauseAudio();
        }
        PauseUI.SetActive(true);
    }
    public void ContinueGame()
    {
        gun.EnabledLaser = true;
        Paused = false;
        fps.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        if (!SavingSystem.Loading)
        {
            foreach (var node in SndNodes)
            {
                node.ContinueAudio();
            }
        }
        PauseUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            if (!Paused)
                PauseGame();
            else
            {
                ContinueGame();
            }
        }
    }
    public void RefreshSaveFolder()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + "/saves");
        GameObject slot = null;
        float pos = -30 + 55;
        if (files.Length > 0)
        {
            noSavesText.SetActive(false);
            foreach (string file in files)
            {
                print(file[file.Length - 5].ToString());
                slot = Instantiate(prefab, prefparent.transform);
                slot.GetComponent<RectTransform>().localPosition = new Vector3(114.715f, pos - 55, 0);
                pos = slot.GetComponent<RectTransform>().localPosition.y;
                slot.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                slot.transform.GetChild(5).GetComponent<Toggle>().group = prefparent.GetComponent<ToggleGroup>();
                slot.GetComponent<SaveSlotHandler>().Index = int.Parse(file[file.Length - 5].ToString());
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(file, FileMode.Open);
                GameData data = bf.Deserialize(stream) as GameData;
                stream.Close();
                print(data.SceneName);
                slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = data.SceneName;
                slot.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = data.Date;
                slot.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = data.SaveType;
            }
        }
        else
        {
            noSavesText.SetActive(true);
        }
    }
    public void LoadGame()
    {
        if (SaveIndex == -1)
        {
            return;
        }
        else
        {
            SavingSystem.Load(SaveIndex);
        }
    }
    public void OpenWindow(GameObject window)
    {
        window.SetActive(true);
        if (window.name == "SaveWindow")
        {
            RefreshSaveFolder();
        }
    }
    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
}
