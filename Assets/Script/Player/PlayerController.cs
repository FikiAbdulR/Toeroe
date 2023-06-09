using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector2 move, mouseLook;
    public Vector3 rotationTarget;
    private float gravity = 9.8f;
    private Vector3 moveDirection = Vector3.zero;

    public CharacterController controller;
    public bool isPC;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.instance.isStart == true)
        {
            if (GameplayManager.instance.isEnd == false)
            {
                if (!GameplayManager.instance.isPaused)
                {
                    if (isPC)
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(mouseLook);

                        if (Physics.Raycast(ray, out hit))
                        {
                            rotationTarget = hit.point;
                        }

                        movePlayerWithAim();
                    }
                    else
                    {
                        movePlayer();
                    }

                    anim.SetFloat("MoveX", move.x);
                    anim.SetFloat("MoveY", move.y);
                }
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void movePlayerWithAim()
    {
        if (isPC)
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
        }

        Vector3 movement = new Vector3(move.x, 0f, move.y);

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(movement * speed * Time.deltaTime + moveDirection * Time.deltaTime);
    }
}
