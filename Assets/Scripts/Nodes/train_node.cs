using UnityEngine.Events;
using UnityEngine;

public class train_node : MonoBehaviour
{
    public bool DoNotPlay;
    public UnityEvent Output_OnStart;
    public UnityEvent Output_OnArrive;
    public Transform StartPoint, EndPoint;
    [SerializeField][Range(0f, 20f)]float LerpTime;
    public Transform DynamicObject;
    [SerializeField] bool Started = false;
    CharacterController cc;
    private void Start()
    {
        DynamicObject.position = StartPoint.position;
    }
    public void Disable()
    {
        DoNotPlay = true;
    }
    private void Update()
    {
        if (Started)
        {
            if (DynamicObject.position != EndPoint.position)
            {
                Vector3 pos = Vector3.MoveTowards(DynamicObject.position, EndPoint.position, LerpTime * Time.deltaTime);
                DynamicObject.position = pos;
            }
            else
            {
                Output_OnArrive.Invoke();
                Started = false;
            }
        }
    }
    public void StartMovingTrain()
    {
        if (DoNotPlay)
        {
            return;
        }
        Started = true;
        Output_OnStart.Invoke();
    }
}
