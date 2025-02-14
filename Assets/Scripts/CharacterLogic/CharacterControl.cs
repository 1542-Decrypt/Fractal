using System.Threading.Tasks;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class CharacterControl : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private FootSteps sounds;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    internal bool canMove = true;

    void Awake()
    {
        if (MiscStuff.ForceCrouch != true)
            MiscStuff.PlayerCrouching = false;
        sounds = GameObject.FindAnyObjectByType<FootSteps>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
            sounds.PlayJump();
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if ((Input.GetKey(KeyCode.LeftControl) || MiscStuff.ForceCrouch == true) && canMove)
        {
            if (characterController.height != crouchHeight)
                characterController.height = Mathf.MoveTowards(characterController.height, crouchHeight, Time.deltaTime * 4);
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
            MiscStuff.PlayerCrouching = true;
        }
        else
        {
            MiscStuff.PlayerCrouching = false;
            if (characterController.height != defaultHeight)
                characterController.height = Mathf.MoveTowards(characterController.height, defaultHeight, Time.deltaTime * 4);
            walkSpeed = 4f;
            runSpeed = 4f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}