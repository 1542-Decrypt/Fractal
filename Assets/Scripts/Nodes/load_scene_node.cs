using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class load_scene_node : MonoBehaviour
{
    public Loading_Screen screen;
    public void Load()
    {
        screen.On_Loading.Invoke();
    }
}
