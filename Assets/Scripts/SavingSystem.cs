using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
public class SavingSystem : MonoBehaviour
{
    public GameObject SaveGUI;
    public ShootLaser coordinates;
    public Pause pause;
    public StageSwitch helper;
    public static bool isSavingEnabled = false;
    public CharacterControl player;
    public LaserGunLogic laser;
    public Loading_Screen LoadingScreen;
    static public int currentSave = 0;
    static public string SaveType;
    static public bool Loading = false;
    static private int CurrentLoadingSave = 0;
    private void Awake()
    {
        //Creating saves folder if missing
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "saves"));
        }
        if (!Directory.Exists(Application.persistentDataPath + "/previews"))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "previews"));
        }
        //Refreshing currentSave
        string[] files = Directory.GetFiles(Application.persistentDataPath + "/saves");
        if (files.Length > 0)
        {
            foreach (string file in files)
            {
                currentSave = int.Parse(file[file.Length - 5].ToString());
            }
        }
        else
        {
            currentSave = 0;
        }
        //Setting PreStart changes so progression will be the same as it was at save time
        if (Loading)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/saves/game" + CurrentLoadingSave.ToString() + ".sav", FileMode.Open);
            GameData data = bf.Deserialize(stream) as GameData;
            stream.Close();
            helper.Check(data.Stage, true);
            for (int i = 0; i < GameObject.FindObjectsByType<Trigger_block>(FindObjectsSortMode.InstanceID).Length; i++)
            {
                GameObject.FindObjectsByType<Trigger_block>(FindObjectsSortMode.InstanceID)[i].Fired = data.ifTriggersFired[i];
            }
        }
    }
    private void Start()
    {
        // genius idea tbh. Just wait until scene fully loads (new scene), and then do the loading its presave state
        if (Loading)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/saves/game" + CurrentLoadingSave.ToString() + ".sav", FileMode.Open);
            GameData data = bf.Deserialize(stream) as GameData;
            stream.Close();
            pause.ContinueGame();
            helper.Check(data.Stage, false);
            player.gameObject.transform.position = new Vector3(data.PlayerPosition[0], data.PlayerPosition[1], data.PlayerPosition[2]);
            player.transform.rotation = new Quaternion(data.PlayerRotation[0], data.PlayerRotation[1], data.PlayerRotation[2], data.PlayerRotation[3]);
            if (data.hasLaserGun)
            {
                print("player had the gun");
                laser.Fired = true;
                laser.ForceGive = true;
                laser.GiveGun();
                laser.EnabledLaser = data.isLaserGunEnabled;
            }
            if (data.isLaserSummoned)
            {
                coordinates.bufDirection = new Vector3(data.LaserDir[0], data.LaserDir[1], data.LaserDir[2]);
                coordinates.bufPosition = new Vector3(data.LaserPos[0], data.LaserPos[1], data.LaserPos[2]);
            }
            coordinates.TriggersStuff = true;
            laser.IsSet = data.isLaserSummoned;
            if (GameObject.FindAnyObjectByType<drop_node>() != null)
            {
                drop_node[] temp_drop_nodes = GameObject.FindObjectsByType<drop_node>(FindObjectsSortMode.InstanceID);
                for (int i = 0; i < temp_drop_nodes.Length; i++)
                {
                    if (data.CubePositionX.Count > 0)
                    {
                        GameObject.FindObjectsByType<drop_node>(FindObjectsSortMode.InstanceID)[i].cube = Instantiate(temp_drop_nodes[i].prefab, new Vector3(data.CubePositionX[i], data.CubePositionY[i], data.CubePositionZ[i]
                            ), new Quaternion(data.CubeRotationX[i], data.CubeRotationY[i], data.CubeRotationZ[i], data.CubeRotationW[i]));
                    }
                }
            }
            for (int i = 0; i < GameObject.FindObjectsByType<button_node>(FindObjectsSortMode.InstanceID).Length; i++)
            {
                GameObject.FindObjectsByType<button_node>(FindObjectsSortMode.InstanceID)[i].isPressed = data.ifButtonsPressed[i];
            }
            for (int i = 0; i < GameObject.FindObjectsByType<Rotating_platform_logic>(FindObjectsSortMode.InstanceID).Length; i++)
            {
                GameObject.FindObjectsByType<Rotating_platform_logic>(FindObjectsSortMode.InstanceID)[i].gameObject.transform.rotation = new Quaternion(data.RotPanelX[i], data.RotPanelY[i], data.RotPanelZ[i], data.RotPanelW[i]);
            }
            for (int i = 0; i < GameObject.FindObjectsByType<CubeCollisionSound>(FindObjectsSortMode.InstanceID).Length; i++)
            {
                if (data.ifObjectHeld[i])
                {
                    player.gameObject.GetComponent<Pickup_system>().Grab(GameObject.FindObjectsByType<CubeCollisionSound>(FindObjectsSortMode.InstanceID)[i].gameObject);
                }
            }
            for (int i = 0; i < GameObject.FindObjectsByType<PropObject>(FindObjectsSortMode.InstanceID).Length; i++)
            {
                GameObject.FindObjectsByType<PropObject>(FindObjectsSortMode.InstanceID)[i].gameObject.transform.position = new Vector3(data.ObjPositionX[i], data.ObjPositionY[i], data.ObjPositionZ[i]);
                GameObject.FindObjectsByType<PropObject>(FindObjectsSortMode.InstanceID)[i].gameObject.transform.rotation = new Quaternion(data.ObjRotationX[i], data.ObjRotationY[i], data.ObjRotationZ[i], data.ObjRotationW[i]);
            }
            CurrentLoadingSave = 0;
            Debug.Log("Game Loaded Successfully");
            Loading = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6) && isSavingEnabled)
        {
            currentSave += 1;
            SaveType = "QUICK SAVE";
            Save(player, laser, currentSave, coordinates, SaveGUI, false);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            Load(currentSave);
        }
    }
    public void SaveTrigger(int SaveID)
    {
        bool Overwrite;
        if (SaveID > 0)
        {
            Overwrite = true;
        }
        else
        {
            Overwrite = false;
            currentSave += 1;
            SaveID = currentSave;
        }
        Debug.LogWarning("Save file being Overwritten is " + Overwrite + ", and its ID is currently " + SaveID);
        SaveType = "QUICK SAVE";
        Save(player, laser, SaveID, coordinates, SaveGUI, Overwrite);
    }
    public void Delete(int SaveSlot)
    {
        currentSave = SaveSlot - 1;
        File.Delete(Application.persistentDataPath + "/saves/game" + SaveSlot.ToString() + ".sav");
        string pathToImg = Application.persistentDataPath + "/previews/" + SaveSlot.ToString() + ".png";
        if (File.Exists(pathToImg))
            File.Delete(pathToImg);
    }
    public void Save(CharacterControl player, LaserGunLogic gun, int SaveSlot, ShootLaser coords, GameObject SGUI, bool Overwrite)
    {
        Debug.LogWarning("Current Slot of saving is " + SaveSlot);
        BinaryFormatter bf = new BinaryFormatter();
        StartCoroutine(SaveImage(SaveSlot));
        if (Overwrite == false)
        {
            while (File.Exists(Application.persistentDataPath + "/saves/game" + SaveSlot.ToString() + ".sav"))
            {
                SaveSlot++;
            }
            currentSave = SaveSlot;
        }
        FileStream stream = new FileStream(Application.persistentDataPath + "/saves/game" + SaveSlot.ToString() + ".sav", FileMode.Create, FileAccess.Write);
        GameData data = new GameData(player, gun, coords, SaveSlot);
        bf.Serialize(stream, data);
        stream.Close();
        SGUI.GetComponent<Animation>().Play();
        Debug.LogWarning("Saved " + "game" + SaveSlot.ToString() + ".sav" + " to" + Application.persistentDataPath + "/saves");
    }
    public void Load(int SaveSlot)
    {
        if (File.Exists(Application.persistentDataPath + "/saves/game" + SaveSlot.ToString() + ".sav"))
        {
            Loading = true;
            CurrentLoadingSave = SaveSlot;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/saves/game" + SaveSlot.ToString() + ".sav", FileMode.Open);
            GameData data = bf.Deserialize(stream) as GameData;
            stream.Close();
            LoadingScreen.LoadScene(data.SceneName);
        }
        else
        {
            Debug.LogError("Save file at " + Application.persistentDataPath + "/saves/game" + SaveSlot.ToString() + ".sav" + " is not found!");
        }
    }
    static IEnumerator SaveImage(int SaveSlot)
    {
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/previews/" + SaveSlot + ".png", 1);

    }
}
[Serializable]
public class GameData
{
    public int Stage;
    public string Date;
    public string SaveType;
    public int SaveID;
    public string SceneName;
    public bool hasLaserGun;
    public bool isLaserGunEnabled;
    public bool isLaserSummoned;
    //-
    public List<bool> ifObjectHeld = new List<bool>();
    //-
    public List<bool> ifTriggersFired = new List<bool>();
    //-
    public List<bool> ifButtonsPressed = new List<bool>();
    //-
    public float[] PlayerPosition = { 0f, 0f, 0f };
    //-
    public float[] PlayerRotation = { 0f, 0f, 0f, 0f };
    //-
    public List<float> CubePositionX = new List<float>();
    public List<float> CubePositionY = new List<float>();
    public List<float> CubePositionZ = new List<float>();
    //-
    public List<float> CubeRotationX = new List<float>();
    public List<float> CubeRotationY = new List<float>();
    public List<float> CubeRotationZ = new List<float>();
    public List<float> CubeRotationW = new List<float>();
    //-
    public List<float> ObjPositionX = new List<float>();
    public List<float> ObjPositionY = new List<float>();
    public List<float> ObjPositionZ = new List<float>();
    //-
    public List<float> ObjRotationX = new List<float>();
    public List<float> ObjRotationY = new List<float>();
    public List<float> ObjRotationZ = new List<float>();
    public List<float> ObjRotationW = new List<float>();
    //-
    public float[] LaserPos = { 0f, 0f, 0f };
    public float[] LaserDir = { 0f, 0f, 0f };
    //-
    public List<float> RotPanelX = new List<float>();
    public List<float> RotPanelY = new List<float>();
    public List<float> RotPanelZ = new List<float>();
    public List<float> RotPanelW = new List<float>();

    public GameData(CharacterControl player, LaserGunLogic gun, ShootLaser coords, int id)
    {
        Stage = LevelStageHelper.ChamberStage;
        Date = DateTime.Now.ToString();
        SaveType = SavingSystem.SaveType;
        Debug.Log(player.gameObject.name);
        SaveID = id;
        SceneName = SceneManager.GetActiveScene().name;
        PlayerPosition[0] = player.gameObject.transform.position.x;
        PlayerPosition[1] = player.gameObject.transform.position.y;
        PlayerPosition[2] = player.gameObject.transform.position.z;

        PlayerRotation[0] = player.gameObject.transform.rotation.x;
        PlayerRotation[1] = player.gameObject.transform.rotation.y;
        PlayerRotation[2] = player.gameObject.transform.rotation.z;
        PlayerRotation[3] = player.gameObject.transform.rotation.w;

        LaserPos[0] = coords.bufPosition.x;
        LaserPos[1] = coords.bufPosition.y;
        LaserPos[2] = coords.bufPosition.z;

        LaserDir[0] = coords.bufDirection.x;
        LaserDir[1] = coords.bufDirection.y;
        LaserDir[2] = coords.bufDirection.z;
        isLaserGunEnabled = gun.EnabledLaser;
        hasLaserGun = gun.isGiven;
        isLaserSummoned = gun.IsSet;
        try
        {
            GameObject.FindObjectsByType<drop_node>(FindObjectsSortMode.InstanceID);
        }
        catch
        {
            Debug.LogWarning("There are no cubes, thus their stats wont be saved");
        }
        if (GameObject.FindObjectsByType<Rotating_platform_logic>(FindObjectsSortMode.InstanceID).Length > 0)
        {
            foreach (Rotating_platform_logic rotplat in GameObject.FindObjectsByType<Rotating_platform_logic>(FindObjectsSortMode.InstanceID))
            {
                RotPanelX.Add(rotplat.gameObject.transform.rotation.x);
                RotPanelY.Add(rotplat.gameObject.transform.rotation.y);
                RotPanelZ.Add(rotplat.gameObject.transform.rotation.z);
                RotPanelW.Add(rotplat.gameObject.transform.rotation.w);
            }
        }
        if (GameObject.FindObjectsByType<button_node>(FindObjectsSortMode.InstanceID).Length > 0)
        {
            foreach (button_node button in GameObject.FindObjectsByType<button_node>(FindObjectsSortMode.InstanceID))
            {
                ifButtonsPressed.Add(button.isPressed);
            }
        }
        if (GameObject.FindObjectsByType<drop_node>(FindObjectsSortMode.InstanceID).Length > 0)
        {
            foreach (drop_node node in GameObject.FindObjectsByType<drop_node>(FindObjectsSortMode.InstanceID))
            {
                if (node.cube != null)
                {
                    CubePositionX.Add(node.cube.transform.position.x);
                    CubePositionY.Add(node.cube.transform.position.y);
                    CubePositionZ.Add(node.cube.transform.position.z);

                    CubeRotationX.Add(node.cube.transform.rotation.x);
                    CubeRotationY.Add(node.cube.transform.rotation.y);
                    CubeRotationZ.Add(node.cube.transform.rotation.z);
                    CubeRotationW.Add(node.cube.transform.rotation.w);
                }
            }
        }
        if (GameObject.FindObjectsByType<CubeCollisionSound>(FindObjectsSortMode.InstanceID).Length > 0)
        {
            foreach (CubeCollisionSound node in GameObject.FindObjectsByType<CubeCollisionSound>(FindObjectsSortMode.InstanceID))
            {
                if (node.isCube != true)
                {
                    ObjPositionX.Add(node.transform.position.x);
                    ObjPositionY.Add(node.transform.position.y);
                    ObjPositionZ.Add(node.transform.position.z);

                    ObjRotationX.Add(node.transform.rotation.x);
                    ObjRotationY.Add(node.transform.rotation.y);
                    ObjRotationZ.Add(node.transform.rotation.z);
                    ObjRotationW.Add(node.transform.rotation.w);
                }
                if (node.transform.parent == player.gameObject.GetComponent<Pickup_system>().objectHolder)
                {
                    ifObjectHeld.Add(true);
                }
                else
                {
                    ifObjectHeld.Add(false);
                }
            }
        }
        if (GameObject.FindObjectsByType<PropObject>(FindObjectsSortMode.InstanceID).Length > 0)
        {
            foreach (PropObject node in GameObject.FindObjectsByType<PropObject>(FindObjectsSortMode.InstanceID))
            {
                ObjPositionX.Add(node.transform.position.x);
                ObjPositionY.Add(node.transform.position.y);
                ObjPositionZ.Add(node.transform.position.z);

                ObjRotationX.Add(node.transform.rotation.x);
                ObjRotationY.Add(node.transform.rotation.y);
                ObjRotationZ.Add(node.transform.rotation.z);
                ObjRotationW.Add(node.transform.rotation.w);
            }
        }
        foreach (Trigger_block trigger in GameObject.FindObjectsByType<Trigger_block>(FindObjectsSortMode.InstanceID))
        {
            ifTriggersFired.Add(trigger.Fired);
        }
    }

}