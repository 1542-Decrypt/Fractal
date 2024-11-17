using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class quit : MonoBehaviour
{
    private float escTimer = 2f;
    private float opacity = 0;
    private bool Done = false;
    public bool EraseProgress;
    public KeyCode KeyToExit;
    void FixedUpdate()
    {
        Color tmp = this.GetComponent<RawImage>().color;
        tmp.a = opacity;
        this.GetComponent<RawImage>().color = tmp;
        if (escTimer > 0 && Input.GetKey(KeyToExit))
        {
            opacity += Time.deltaTime;
            escTimer -= Time.deltaTime;
        }
        //else if (escTimer <= 0)
        //{
        //    Done = true;
        //    if (EraseProgress == true)
        //    {
        //        this.GetComponent<Eraser>().EraseCompletely();
        //        Application.Quit();
        //        print("GameClosed");
        //    }
        //    else {
        //        SceneManager.LoadScene("menu 1");
        //    }
        //    
        //}
        else if (!Input.GetKey(KeyToExit) && !Done)
        {
            Reset();
        }
    }
    private void Reset()
    {
        escTimer = 2f;
        opacity = 0;
    }
}
