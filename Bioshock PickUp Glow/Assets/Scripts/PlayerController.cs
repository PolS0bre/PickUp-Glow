using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private float walkSpeed = 6f;
    private float runSpeed = 10f;
    private float jumpPower = 7f;
    private float gravity = 10f;

    private float lookSpeed = 2f;
    private float lookXlimit = 45f;
    public string selectedObj;

    Vector3 MoveDirection = Vector3.zero;
    float rotationX = 0f;
    RaycastHit hit;

    public bool canMove = true;
    public float range = 3.5f;

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController= GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = MoveDirection.y;
        MoveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        
        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            MoveDirection.y = jumpPower;
        }
        else
        {
            MoveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            MoveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation

        characterController.Move(MoveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXlimit, lookXlimit);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion


        #region Handles Pointer

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if(hit.collider.tag == "PickUp")
            {
                selectedObj = hit.collider.name;
            }
        }
        else
        {
            selectedObj = null;
        }

        #endregion
    }
}