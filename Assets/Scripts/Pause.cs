using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
public class Pause : MonoBehaviour
{
    public Button savebutton;
    public LaserGunLogic gun;
    public CharacterControl fps;
    public GameObject PauseUI, SavingUI;
    public GameObject prefab, prefparent, noSavesText;
    public SavingSystem SaveSys;
    public KeyCode PauseButton;

    public UnityEvent WarningOverwrite;
    public UnityEvent WarningDelete;
    public UnityEvent WarningExit;

    public static int SaveIndex;
    public static bool Paused;
    private bool isGunDisabledByDef;
    public void PauseGame()
    {
        if (SavingUI.activeSelf)
        {
            RefreshSaveFolder();
        }
        isGunDisabledByDef = true;
        if (gun.EnabledLaser)
        {
            isGunDisabledByDef = false;
            gun.EnabledLaser = false;
        }
        Paused = true;
        fps.enabled = false;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
  
        PauseUI.SetActive(true);
    }
    public void ContinueGame()
    {
        if (!isGunDisabledByDef)
            gun.EnabledLaser = true;
        Paused = false;
        fps.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        if (!SavingSystem.Loading)
        {
            AudioListener.pause = false;
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
        SaveIndex = -1;
        if (!SavingSystem.isSavingEnabled)
        {
            savebutton.interactable = false;
        }
        else
        {
            savebutton.interactable = true;
        }
        if (prefparent.transform.childCount > 1)
        {
            SaveSlotHandler[] children = prefparent.transform.GetComponentsInChildren<SaveSlotHandler>();
            foreach (var child in children)
            {
                Destroy(child.gameObject);
            }
        }
        string[] files = Directory.GetFiles(Application.persistentDataPath + "/saves");
        GameObject slot = null;
        float pos = -30 + 55;
        if (files.Length > 0)
        {
            noSavesText.SetActive(false);
            foreach (string file in files)
            {
                slot = Instantiate(prefab, prefparent.transform);
                slot.GetComponent<RectTransform>().localPosition = new Vector3(114.715f, pos - 55, 0);
                pos = slot.GetComponent<RectTransform>().localPosition.y;
                slot.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                slot.transform.GetChild(5).GetComponent<Toggle>().group = prefparent.GetComponent<ToggleGroup>();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(file, FileMode.Open);
                GameData data = bf.Deserialize(stream) as GameData;
                stream.Close();
                slot.GetComponent<SaveSlotHandler>().Index = data.SaveID;
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
    public void SaveGame(bool Accept)
    {
        if (SaveIndex != -1 && Accept != true)
        {
            WarningOverwrite.Invoke();
            return;
        }
        SaveSys.SaveTrigger(SaveIndex);
        RefreshSaveFolder();
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
    public void DeleteSave(bool Accept)
    {
        if (SaveIndex == -1)
        {
            return;
        }
        else if (Accept == true)
        {
            SaveSys.Delete(SaveIndex);
            RefreshSaveFolder();
        }
        else
        {
            WarningDelete.Invoke();
            return;
        }
    }
}


