using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    bool canSway = true;

    public float Amount = 0.1f;
    public float maxAmount = 0.1f;
    public float SmoothAmount = 2;
    public Transform parent;

    private Vector3 initialPositon;

    // Start is called before the first frame update
    void Start()
    {
        initialPositon = transform.localPosition;
        canSway = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSway == true && base.transform.parent == parent)
        {
            float movementX = -Input.GetAxis("Mouse X") * Amount;
            float movementY = -Input.GetAxis("Mouse Y") * Amount;

            movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
            movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

            Vector3 finalPosition = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPositon, Time.deltaTime * SmoothAmount);
        }

    }
}
