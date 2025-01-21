using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public bool IsMono;
    public string LaserName;
    internal bool TriggersStuff = false;
    public int MaxReflections;
    public Material material;
    LaserBeam beam;
    [SerializeField]public List<Interaction> activeObjects = new List<Interaction>();
    public GameObject prefab;
    public LayerMask mask, mask2, uniqueMask;
    public Image crosshair;
    internal Vector3 bufPosition, bufDirection;
    int RevID=0;
    private void Awake()
    {
        if (IsMono)
        {
            crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        }
        else
        {
            try
            {
                crosshair = GameObject.Find("CrossHalfPrime").GetComponent<Image>();
            }
            catch
            {
                print("stfu");
            }
        }
    }
    public void CastLaser(Transform emitter)
    {
        RevID = 0;
        if (GameObject.Find(LaserName) != null)
        {
            Destroy(GameObject.Find(LaserName + "end" + (beam.endID).ToString()));
            Destroy(GameObject.Find(LaserName));
            Destroy(GameObject.Find(LaserName + "start"));
        }
        if (activeObjects.Count != 0)
        {
            foreach (Interaction Actobj in activeObjects)
            {
                Actobj.RecallSignalFrom();
            }
            activeObjects.Clear();
        }
        beam = new LaserBeam(emitter.position, emitter.forward, material, prefab, mask, crosshair, base.gameObject.GetComponent<ShootLaser>(), mask2, MaxReflections, TriggersStuff);
        bufPosition = emitter.position; bufDirection = emitter.forward;
    }
    public void UpdateLaserCollision()
    {
        RevID = 0;
        if (GameObject.Find(LaserName) != null)
        {
            Destroy(GameObject.Find(LaserName + "end" + (beam.endID).ToString()));
            Destroy(GameObject.Find(LaserName));
            Destroy(GameObject.Find(LaserName + "start"));
        }
        if (activeObjects.Count != 0)
        {
            foreach (Interaction Actobj in activeObjects)
            {
                Actobj.RecallSignalFrom();
            }
            activeObjects.Clear();
        }
        beam = new LaserBeam(bufPosition, bufDirection, material, prefab, mask, crosshair, base.gameObject.GetComponent<ShootLaser>(), mask2, MaxReflections, TriggersStuff);
    }
    private void Update()
    {
        if (beam != null) {
        if (GameObject.Find(LaserName).transform.childCount > 2 && beam.endID != RevID)
        {
          Destroy(GameObject.Find(LaserName + "end" + (RevID).ToString()));
          RevID += 1;
        }
        }
    }
}
