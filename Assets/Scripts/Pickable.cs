using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpringJoint))]
public class Pickable : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody;

    private void Awake()
    {
        if (!rigidbody && !TryGetComponent<Rigidbody>(out rigidbody))
        {
            Debug.LogError("This component requires a Rigidbody!", this);
        }
    }

    public void MarkActive(bool active)
    {
        // Your customerhod for marking an object the currently active one
        // e.g. change its color, add outline etc
    }
}
