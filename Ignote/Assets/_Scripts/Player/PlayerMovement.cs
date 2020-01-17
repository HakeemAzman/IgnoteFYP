using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PlayerMovement : MonoBehaviour
{
    #region Public and Private Variables
    [Header("Player Stats")]
    public Animator emilyAnim;
    public float player_Speed;
    public float player_SetSpeed;
    public float player_RunningSpeed;
    public float player_Stamina = 20f;
    public float player_CurrentStamina = 20f;
    public float player_RunningEnergy = 2f;
    public float player_ShortDash;
    public bool playerCanMove = true;
    public bool isCompanion;
    [Space]

    public float jumpHeight;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool isMoving;
    [Space]
    public NarrationStage1 nsScript;
    //Private Variables
    Rigidbody rb;
    GameObject player;
    bool isDashing = false;
    bool isJumping = true;
    float jumpTimer = 2f;
    #endregion

    // Use this for initialization
    void Start ()
    {
        emilyAnim = emilyAnim.GetComponent<Animator>();
        player = this.gameObject;
        nsScript = GetComponent<NarrationStage1>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
	}

    //Update is called once per frame
    void Update ()
    {
        #region Enemy Movement, Direction facing, Can Move
        if (playerCanMove)
        {
            float xAxis = Input.GetAxis("Horizontal");
            float zAxis = Input.GetAxis("Vertical");

            Vector3 playerMovement = new Vector3(xAxis, 0, zAxis);

            playerMovement = Camera.main.transform.TransformDirection(playerMovement);
            playerMovement.y = 0.0f;

            if (xAxis + zAxis != 0) //If there is no movement, the rotation of the character will not revert back to 0 but instead stay at the currect rotation
            {
                isMoving = true;
                player_Speed = player_SetSpeed;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMovement), 0.3f); //Turns to the direction of the character movement
             
            }
            else if (xAxis + zAxis == 0)
            {
                isMoving = false;
                player_Speed = 0;
                // emilyAnim.GetComponent<Animator>().SetFloat("forwardSpeed", 0);
            }


            //Running
            if ((Input.GetButton("Run") && nsScript.canMove || Input.GetKey(KeyCode.LeftShift) && (xAxis + zAxis != 0)))
            {
                isDashing = true;
            }
            else if ((Input.GetButtonUp("Run") && nsScript.canMove || Input.GetKeyUp(KeyCode.LeftShift)) || (xAxis + zAxis == 0))
            {
                isDashing = false;
                emilyAnim.GetComponent<Animator>().SetFloat("forwardSpeed", 0);
            }

            //Dashing is True
            if (isDashing)
            {
                player_Speed = player_RunningSpeed;
                UpdateAnimator();
            }

            //Dashing is False
            else if (!isDashing)
            {
               //player_Speed = player_SetSpeed; //Set Player speed back to normal
            }

            ////Stamina reaches 0
            //if (player_CurrentStamina <= 0)
            //{
            //    player_CurrentStamina = 0f;
            //    player_Speed = player_SetSpeed;
            //    isDashing = false;

            //    UpdateAnimator();
            //}

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }

            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            //Walking
            transform.Translate(playerMovement * player_Speed * Time.deltaTime, Space.World);
            UpdateAnimator();
        }
        #endregion

        #region Jumping and altering the mass

        if (Input.GetButtonDown("Jump") && isJumping)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpHeight;
            //rb.AddForce(Vector3.up * jumpHeight);
            isJumping = false;
        }

        if(!isJumping)
        {
            jumpTimer -= 1f * Time.deltaTime;

            if (jumpTimer <= 1f)
                rb.mass = 3f;

            if(jumpTimer <= 0)
            {
                rb.mass = 1f;
                jumpTimer = 1f;
                isJumping = true;
            }
        }
        #endregion

        if(Input.GetButton("Interact"))
            emilyAnim.Play("EmilyPushing");

        if(Input.GetButtonUp("Interact"))
            emilyAnim.Play("EmilyIdle");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Crate"))
        {
            player_RunningSpeed = 2f;
            player_SetSpeed = 2f;
            player_Speed = 2f;
            nsScript.canMove = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interact") && other.CompareTag("Crate"))
        {
            other.gameObject.transform.parent = this.gameObject.transform;

            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            nsScript.canMove = false;
            player_RunningSpeed = 2f;
            player_SetSpeed = 2f;
        }

        if(Input.GetButtonUp("Interact") && other.CompareTag("Crate") || Input.GetButtonUp("Interact"))
        {
            other.gameObject.transform.parent = null;

            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            nsScript.canMove = true;
            player_RunningSpeed = 12f;
            player_SetSpeed = 7f;
        }

        if(other.gameObject.tag == "Companion" || other.gameObject.tag == "Ballista")
        {
            isCompanion = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crate"))
        {
            other.gameObject.transform.parent = null;

            nsScript.canMove = true;
            player_RunningSpeed = 12f;
            player_SetSpeed = 7f;
        }

        if (other.gameObject.tag == "Companion")
        {
            isCompanion = false;
        }

    }

    private void UpdateAnimator()
    {
        emilyAnim.GetComponent<Animator>().SetFloat("forwardSpeed", player_Speed);
    }
}
