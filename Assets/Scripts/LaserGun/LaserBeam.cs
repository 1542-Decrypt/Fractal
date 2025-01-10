using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LaserBeam
{

    [SerializeField]
    int Reflections;
    public Vector3 pos, dir;
    public GameObject laserObj, fab;
    Image crosshair;
    public LineRenderer laser;
    LayerMask Lmask, Lmask2;
    ShootLaser sender;
    Color color;
    public List<Vector3> laserIndices = new List<Vector3>();
    List<Vector3> HitObjects = new List<Vector3>();
    bool MustUpdate = false;
    public bool IsTriggerable;
    public int endID = 0;
    bool NeedToAdd = false;
    public LaserBeam(Vector3 pos, Vector3 dir, Material mat, GameObject prefab, LayerMask mask, Image crsh, ShootLaser farther, LayerMask mask2, int Refl, bool Triggers)
    {
        Reflections = Refl;
        this.IsTriggerable = Triggers;
        this.sender = farther;
        this.crosshair = crsh;
        this.Lmask = mask;
        this.Lmask2 = mask2;
        this.fab = prefab;
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = sender.LaserName;
        this.pos = pos;
        this.dir = dir;
        CreateStartObject(fab, pos);

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.015f;
        this.laser.endWidth = 0.015f;
        this.laser.material = mat;
        this.laser.numCapVertices = 8;
        this.laser.numCornerVertices = 8;
        crosshair.color = new Color(1, 1, 1, 0.5f);
        CastRay(pos, dir, laser);
    }
    void CheckColor(Vector3 pos, Vector3 dir)
    {
        bool PullAllStops = false;
        RaycastHit[] hits = Physics.RaycastAll(pos, dir, 30, Lmask2, QueryTriggerInteraction.Collide);
        Array.Sort(hits, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
        foreach (RaycastHit hit in hits)
        {
            int lay = hit.collider.gameObject.layer;
            if (lay == 3)
            {
                Color NewCol = this.laser.material.color;
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetComponent<GlassController>() != null)
                {
                    color = hit.collider.gameObject.GetComponent<GlassController>().GlassColor;
                }
                else
                {
                    color = NewCol;
                }
                if (Color.white == NewCol)
                {
                    this.laser.material.SetColor("_EmissionColor", color);
                    this.laser.material.color = color;
                    crosshair.color = new Color(color.r, color.g, color.b, 0.5f);
                }
                else
                {
                    Color mixed = Color.Lerp(NewCol, color, 0.5f);
                    this.laser.material.color = Color.Lerp(NewCol, color, 0.5f);
                    this.laser.material.SetColor("_EmissionColor", mixed);
                    crosshair.color = new Color(mixed.r, mixed.g, mixed.b, 0.5f);
                }
            }
            else if (lay == 0)
            {
                PullAllStops = true;
            }
            else if (lay == 9 && PullAllStops != true && IsTriggerable)
            {
                if (sender.activeObjects.Count != 0)
                {
                    foreach (Interaction Actor in sender.activeObjects)
                    {
                        if (hit.collider.gameObject.GetComponent<Interaction>() != Actor)
                        {
                            NeedToAdd = true;
                        }
                    }
                    if (NeedToAdd)
                    {
                        hit.collider.gameObject.GetComponent<Interaction>().recievedColor = this.laser.material.color;
                        hit.collider.gameObject.GetComponent<Interaction>().SendSignalTo();
                        sender.activeObjects.Add(hit.collider.gameObject.GetComponent<Interaction>());
                    }
                }
                else
                {
                    hit.collider.gameObject.GetComponent<Interaction>().recievedColor = this.laser.material.color;
                    hit.collider.gameObject.GetComponent<Interaction>().SendSignalTo();
                    sender.activeObjects.Add(hit.collider.gameObject.GetComponent<Interaction>());
                }
            }
        }
    }
    public void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        CheckColor(pos, dir);
        this.dir = dir;
        laserIndices.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 30, Lmask))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }
    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;
        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }
    void CheckHit(RaycastHit hitinfo, Vector3 direction, LineRenderer laser)
    {
        if (hitinfo.collider.gameObject.tag == "Mirror" && Reflections > 0)
        {
            Reflections -= 1;
            Vector3 pos = hitinfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitinfo.normal);
            CastRay(pos, dir, laser);
        }
        else
        {
            laserIndices.Add(hitinfo.point);
            CreateEndObject(fab, hitinfo.point);
            UpdateLaser();
        }
    }
    void CreateEndObject(GameObject fab, Vector3 pos)
    {
        GameObject obj = UnityEngine.Object.Instantiate(fab, pos, Quaternion.Euler(90, 0, 0), laserObj.transform);
        obj.name = laserObj.name + "end" + (endID).ToString();
        endID += 1;
    }
    void CreateStartObject(GameObject fab, Vector3 pos)
    {
        GameObject obj = UnityEngine.Object.Instantiate(fab, pos, Quaternion.Euler(90, 0, 0), laserObj.transform);
        obj.name = laserObj.name + "start";
    }
}
