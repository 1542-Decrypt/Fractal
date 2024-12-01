using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/MouseLook")]
public class MouseLook : MonoBehaviour
{
    private Camera _camera;
    public GameObject Capsule;
    public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY = 2 };
    public RotationAxes axes = RotationAxes.MouseXandY;
    public float sensitivityX = 2F;
    public float sensitivityY = 2F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -360F;
    public float maximumY = 360F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;
    public float intensity = 10f;
    Vector3 startLocalPosition;
    // Start is called before the first frame update
    //void Start()
    //{
    //    startLocalPosition = transform.localPosition;
    //    _camera = GetComponent<Camera>();

    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;

    //    if(GetComponent<Rigidbody>())
    //    {
    //        GetComponent<Rigidbody>().freezeRotation = true;
    //    }
    //    originalRotation = transform.localRotation;
    //}

    //public static float ClampAngle (float angle, float min, float max)
    //{
    //    if (angle < -360F)
    //        angle += 360F;
    //    if (angle > 360F)
    //        angle -= 360F;
    //    return Mathf.Clamp(angle, min, max);
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    //if (CharacterControl.walk == true)
    //    //{
    //    //    if (axes == RotationAxes.MouseXandY)
    //    //    {
    //    //        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
    //    //        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

    //    //        rotationX = ClampAngle(rotationX, minimumX, maximumX);
    //    //        rotationY = ClampAngle(rotationY, minimumY, maximumY);
    //    //        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
    //    //        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
    //    //        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
    //    //    }
    //    //    else if (axes == RotationAxes.MouseX)
    //    //    {
    //    //        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
    //    //        rotationX = ClampAngle(rotationX, minimumX, maximumX);
    //    //        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
    //    //        transform.localRotation = originalRotation * xQuaternion;
    //    //    }
    //    //    else if (axes == RotationAxes.MouseY)
    //    //    {
    //    //        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
    //    //        rotationY = ClampAngle(rotationY, minimumY, maximumY);
    //    //        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
    //    //        transform.localRotation = originalRotation * yQuaternion;
    //    //    }
    //    }
    //    else
    //    {
    //        //transform.localRotation = originalRotation;
    //        rotationX = 0F;
    //        rotationY = 0F;
    //        transform.localRotation = new Quaternion(Mathf.Lerp(transform.localRotation.x, originalRotation.x, Time.deltaTime * intensity),
    //            Mathf.Lerp(transform.localRotation.y, originalRotation.y, Time.deltaTime * intensity),
    //            Mathf.Lerp(transform.localRotation.z, originalRotation.z, Time.deltaTime * intensity),
    //            Mathf.Lerp(transform.localRotation.w, originalRotation.w, Time.deltaTime * intensity));
    //    }
    //}
}
