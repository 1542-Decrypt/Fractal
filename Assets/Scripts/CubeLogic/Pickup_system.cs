using System;
using UnityEngine;

public class Pickup_system : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, pickupForce = 150f, scrollSpeed=500f;
    [SerializeField] public Transform objectHolder;
    [SerializeField] LaserGunLogic gun;
    [SerializeField] CharacterController Character;
    [SerializeField] sound_node SoundMaster;
    [SerializeField] int NoInteractSndID = 19;

    Rigidbody grabbedRB;
    internal GameObject grabbedOBJ;
    Transform Cached_pos;

    private void Awake()
    {
        Cached_pos = objectHolder.transform;
    }
    void Update()
    {
        //objectHolder.transform.position = objectHolder.transform.position + cam.transform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
        if (grabbedOBJ)
        {
            RaycastHit[] hits = Physics.RaycastAll(PointRay.HoldRay, 1f);
            foreach (RaycastHit hit in hits)
            {
                print(hit.transform.name);
                if (hit.transform.gameObject.CompareTag("Cubefix"))
                {
                    print("drop the cube");
                    Ungrab();
                }
            }
            MoveObj();
            if (Input.GetMouseButton(0) && !Pause.Paused)
            {
                objectHolder.transform.Rotate(0f, 0f, 3f, Space.Self);
            }
            if (Input.GetMouseButton(1) && !Pause.Paused)
                objectHolder.transform.Rotate(0f, 0f, -3f, Space.Self);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedRB)
            {
                Ungrab();
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(MiscStuff.Camray, out hit, maxGrabDistance))
                {
                    if (hit.transform.gameObject.GetComponent<CubeCollisionSound>() != null)
                        if (hit.transform.gameObject.GetComponent<CubeCollisionSound>().IsInteractable)
                            Grab(hit.transform.gameObject);
                        else
                            SoundMaster.PlayAudio(NoInteractSndID);
                }
            }
        }
    }
    void MoveObj()
    {
        grabbedOBJ.transform.localScale = grabbedOBJ.transform.localScale;
        if (Vector3.Distance(grabbedOBJ.transform.position, objectHolder.position) > 0.1f)
        {
            Vector3 moveDirection = (objectHolder.position - grabbedOBJ.transform.position);
            grabbedRB.AddForce(moveDirection * pickupForce);
        }
    }
    public void Ungrab()
    {
        //grabbedRB.isKinematic = false;
        grabbedRB.useGravity = true;
        grabbedRB.linearDamping = 1;
        grabbedRB.constraints = RigidbodyConstraints.None;
        gun.EnabledLaser = true;
        objectHolder.position = Cached_pos.position;
        float velocity = 0;
        if (Character.velocity.x < 1 && Character.velocity.z > 1)
        {
            velocity = Character.velocity.z;
        }
        else if (Character.velocity.x > 1 && Character.velocity.z < 1)
        {
            velocity = Character.velocity.x;
        }
        else if (Character.velocity.x > 1 && Character.velocity.z > 1)
        {
            velocity = (Character.velocity.x + Character.velocity.z) / 2;
        }
        Vector3 direction = cam.transform.forward;
        grabbedRB.AddForce(Character.velocity * 1.5f, ForceMode.VelocityChange);
        grabbedRB.transform.parent = null;
        grabbedOBJ = null;
        grabbedRB = null;
        objectHolder.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public void Grab(GameObject pickedObj)
    {
        if (pickedObj.GetComponent<Rigidbody>())
        {
            gun.EnabledLaser = false;
            grabbedRB = pickedObj.GetComponent<Rigidbody>();
            grabbedRB.useGravity = false;
            grabbedRB.linearDamping = 10;
            grabbedRB.constraints = RigidbodyConstraints.FreezeRotation;

            grabbedOBJ = pickedObj;
            grabbedOBJ.transform.parent = objectHolder;
        }
    }
}

