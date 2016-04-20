using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float speedToTriggerFallingEffect = 5f;  // This will set the speed necessary to trigger the falling sound effect
    public int jumpToPlaySound = 3;

    int jumpCounter = 0;
    private Vector3 moveDirection = Vector3.zero;
    AudioSource playerAudioSource;
    PlayerController pc;


    // Use this for initialization
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                jumpCounter++;
                if (jumpCounter == jumpToPlaySound)
                {
                    playerAudioSource.Stop();
                    playerAudioSource.clip = pc.audioClips[1];
                    playerAudioSource.Play();
                    jumpCounter = 0;
                }
            }
        }
        else
        {
            moveDirection.x = Input.GetAxis("Horizontal") * speed;
            moveDirection.z = Input.GetAxis("Vertical") * speed;
            moveDirection = transform.TransformDirection(moveDirection);
            controller.Move(moveDirection * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
