using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scene_node : MonoBehaviour
{
    public string[] sceneNames;
    public bool LoadOnAwake;
    [Tooltip("If scene should load as new or load on top of existing one. WARNING: IF NOT ENABLED, DO NOT ADD MORE THAN ONE SCENE.")]
    public bool LoadOnTop;
    private void Awake()
    {
        if (LoadOnAwake)
        {
            Load();
        }
    }
    public void Load()
    {
        if (!LoadOnTop && sceneNames.Length > 1) { 
            Debug.LogError("More than 1 scene cannot be loaded separately"); 
            return;
        }
        foreach (string name in sceneNames)
        {
            if (LoadOnTop)
                SceneManager.LoadScene(name, LoadSceneMode.Additive);
            else
                SceneManager.LoadScene(name, LoadSceneMode.Single);
        }
    }
}
