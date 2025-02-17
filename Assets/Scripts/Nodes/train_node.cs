using UnityEngine.Events;
using UnityEngine;

public class train_node : MonoBehaviour
{
    internal bool DoNotPlay;
    [Tooltip("Activates once train starts")]
    public UnityEvent Output_OnStart;
    [Tooltip("Activates once game arrives")]
    public UnityEvent Output_OnArrive;
    [Tooltip("Start and end points of the train")]
    public Transform StartPoint, EndPoint;
    [Tooltip("Time of moving between one point to another")]
    [SerializeField][Range(0f, 20f)] float LerpTime;
    [Tooltip("Train object")]
    public Transform DynamicObject;
    [Tooltip("Sets if train is started. Not advised to use manually.")]
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
