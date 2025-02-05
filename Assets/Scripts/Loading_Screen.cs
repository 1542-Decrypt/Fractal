using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using System.IO;
public class Loading_Screen : MonoBehaviour
{
    public TextMeshProUGUI Tip;
    public GameObject LoadingScreen;
    public Slider Slider;
    public Image LoadingIcon;
    public Sprite[] spritearray;
    private AsyncOperation operation;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWScreen(sceneName));
    }
    public IEnumerator LoadSceneWScreen(string SceneName)
    {
        LoadingScreen.SetActive(true);
        var lines = File.ReadAllLines(Application.persistentDataPath + "/tip_lines.txt");
        var randomIndex = Random.Range(0, lines.Length);
        Tip.text = lines[randomIndex];
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
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    async private void Update()
    {
        if (operation != null)
        {
            if (!operation.isDone)
            {
                for (int i = 0; i < spritearray.Length; i++)
                {
                    await Task.Delay(300);
                    if (LoadingIcon.sprite != null)
                        LoadingIcon.sprite = spritearray[i];
                }
            }
        }
    }
}
