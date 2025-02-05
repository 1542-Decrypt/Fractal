using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class floor_button : MonoBehaviour
{
    public sound_node sound_master;
    public UnityEvent On_Press;
    public UnityEvent On_Release;

    internal Pickup_system pickup_System;
    internal bool pressed;
    internal List<GameObject> gameObjects = new List<GameObject>();
    private void Start()
    {
        pickup_System = FindAnyObjectByType<Pickup_system>();
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<CubeCollisionSound>(out CubeCollisionSound cube))
        {
            if (cube.isCube)
            {
                if (pickup_System.grabbedOBJ != cube.gameObject)
                {
                    Check(0, cube.gameObject);
                }
                else
                {
                    Check(1, cube.gameObject);
                }
            }
            else
            {
                Check(1, cube.gameObject);
            }
        }
    }
    private void Update()
    {
        if (gameObjects.Count > 0)
        {
            Press();
        }
        if (gameObjects.Count == 0)
        {
            Release();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        gameObjects.Remove(collision.gameObject);
    }
    public void Check(int mode, GameObject obj)
    {
        if (!gameObjects.Contains(obj) && mode == 0)
        {
            gameObjects.Add(obj);
        }
        else if (gameObjects.Contains(obj) && mode != 0)
        {
            gameObjects.Remove(obj);
        }
    }
    public void Press()
    {
        if (!pressed)
        {
            pressed = true;
            gameObject.GetComponent<Animation>().Play("f_button_push");
            sound_master.PlayAudio(0);
        }
    }
    public void Release()
    {
        if (pressed)
        {
            pressed = false;
            gameObject.GetComponent<Animation>().Play("f_button_release");
            sound_master.PlayAudio(0);
        }
    }
}
