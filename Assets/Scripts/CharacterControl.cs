using System.Threading.Tasks;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float sprintMult;
    float SprintMultip;
    public float jumpforce;
    public float speed = 4.0f;
    public float gravity = 20.0f;
    Vector3 moveDir = Vector3.zero;
    Vector3 velocity;
    CharacterController controller;
    CheckGround groundCheck;
    bool Iscrouched = false;
    bool Isrunning = false;
    [SerializeField] public static bool walk = true;
    public static bool signal = false;
    public bool canCrouch;
    public FootSteps sounds;
    Transform ControllerObj;
    [SerializeField]bool doesUseStamina, canRun, ableToCrouch;
    Vector3 startLocalPosition;
    // Start is called before the first frame update
    void Start()
    {
        SprintMultip = speed * sprintMult;
        controller = GetComponent<CharacterController>();
        groundCheck = GetComponent<CheckGround>();
        ControllerObj = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (walk)
        {
            if (controller.isGrounded)
            {
                moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                moveDir = transform.TransformDirection(moveDir);
                moveDir *= speed;
                if (Input.GetButtonDown("Jump"))
                {
                    print("pressed");
                    moveDir.y = Mathf.Sqrt(jumpforce * -2f * (gravity * -1));
                }

                /*
                if (Input.GetKey(KeyCode.LeftShift) && Iscrouched == false && canRun)
                {
                    canCrouch = false;
                    speed = SprintMultip;
                }
                else
                {
                    canCrouch = true;
                    speed = 4.0f;
                }
                if (canCrouch == true && ableToCrouch)
                {
                    if (Input.GetKey(KeyCode.LeftControl) && Iscrouched == false)
                    {
                        Crouch();
                    }
                    else if (Input.GetKey(KeyCode.LeftControl) == false && Iscrouched == true)
                    {
                        Uncrouch();
                    }
                }
                if (Iscrouched == true)
                {
                    sounds.Disabled = true;
                }
                else if (controller.isGrounded != true)
                {
                    sounds.Disabled = true;
                }
                else if (controller.isGrounded == true || Iscrouched == false)
                {
                    sounds.Disabled = false;
                }
                */
            }
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }     
    }
    async void CoolDown()
    {
        await Task.Delay(500);
        canCrouch = true;
    }
    void Uncrouch()
    {
        controller.height = 2;
        speed = 4.0f;
        Iscrouched = false;
        canCrouch = false;
        CoolDown();
    }
    void Crouch()
    {
        controller.height = 1;
        Vector3 th = ControllerObj.position;
        ControllerObj.position = new Vector3(th.x, th.y - 0.27f, th.z);
        speed = 2.0f;
        Iscrouched = true;
    }
}
