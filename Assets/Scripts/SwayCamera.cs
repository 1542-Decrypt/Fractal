using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayCamera : MonoBehaviour
{
    public Vector3 bob = new Vector3(.15f, .15f, .15f);
    private float timer = 0;
    public float intensity = 10f;
    public GameObject Obj;
    public string NameAnimation;

    Vector3 startLocalPosition;
    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if ((targetVelocity.x != 0 || targetVelocity.z != 0) && CharacterControl.walk == true) // Если игрок движется
        {
            Obj.GetComponent<Animation>().Play(NameAnimation);
        }   
        else
        {
            Obj.GetComponent<Animation>().Stop(NameAnimation);
            transform.localRotation = new Quaternion(Mathf.Lerp(transform.localRotation.x, originalRotation.x, Time.deltaTime * intensity),
                Mathf.Lerp(transform.localRotation.y, originalRotation.y, Time.deltaTime * intensity),
                Mathf.Lerp(transform.localRotation.z, originalRotation.z, Time.deltaTime * intensity),
                Mathf.Lerp(transform.localRotation.w, originalRotation.w, Time.deltaTime * intensity));
        }
    }
}
