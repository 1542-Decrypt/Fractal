using UnityEngine.SceneManagement;
using UnityEngine;

public class next_stage_node : MonoBehaviour
{
    public string NextStage;
    public void Transit()
    {
        SceneManager.LoadScene(NextStage);
    }
}
