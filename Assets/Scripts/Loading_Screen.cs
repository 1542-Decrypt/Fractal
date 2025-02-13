using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using System.IO;
using UnityEngine.Events;
public class Loading_Screen : MonoBehaviour
{
    public TextMeshProUGUI Tip;
    public GameObject LoadingScreen;
    public Slider Slider;
    public Image LoadingIcon;
    public Sprite[] spritearray;
    private AsyncOperation operation;
    public UnityEvent On_Start_Loading_NoFade;
    public UnityEvent On_Start_Loading;
    public UnityEvent On_Finish_Loading;
    string[] lines;


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWScreen(sceneName));
    }
    public IEnumerator LoadSceneWScreen(string SceneName)
    {
        operation = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
        operation.allowSceneActivation = false;
        float progress = 0;
        while (!operation.isDone)
        {
            progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
            Debug.Log(operation.progress);
            if (operation.progress >= 0.9f)
            {
                Slider.value = 1;
                On_Finish_Loading.Invoke();
            }
            yield return null;
        }
    }
    public void BootLoading()
    {
        LoadingScreen.SetActive(true);
        lines = File.ReadAllLines(Application.persistentDataPath + "/tip_lines.txt");
        var randomIndex = Random.Range(0, lines.Length);
        Tip.text = lines[randomIndex];
    }
    public void ActivateScene()
    {
        operation.allowSceneActivation = true;
    }
}
