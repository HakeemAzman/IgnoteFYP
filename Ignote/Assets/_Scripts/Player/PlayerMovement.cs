using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Public and Private Variables
    [Header("Player Stats")]
    public float player_Speed;
    public float player_SetSpeed;
    public float player_RunningSpeed;
    public float player_Stamina = 20f;
    public float player_CurrentStamina = 20f;
    public float player_RunningEnergy = 2f;
    public float player_ShortDash;
    public bool playerCanMove = true;
    [Space]
    public float jumpHeight;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool isMoving;
    [Space]
    public NarrationStage1 nsScript;

    //Private Variables
    Rigidbody rb;
    bool isDashing = false;
    bool isJumping = true;
    float jumpTimer = 2f;
    #endregion

    // Use this for initialization
    void Start ()
    {
        nsScript = GetComponent<NarrationStage1>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
	}
  
    // Update is called once per frame
    void Update ()
    {
        #region Enemy Movement, Direction facing, Can Move
        if(playerCanMove)
        {
            float xAxis = Input.GetAxis("Horizontal");
            float zAxis = Input.GetAxis("Vertical");

            Vector3 playerMovement = new Vector3(xAxis, 0, zAxis);

            playerMovement = Camera.main.transform.TransformDirection(playerMovement);
            playerMovement.y = 0.0f;

            if (xAxis + zAxis != 0) //If there is no movement, the rotation of the character will not revert back to 0 but instead stay at the currect rotation
            {
                isMoving = true;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMovement), 0.3f); //Turns to the direction of the character movement
                UpdateAnimator();
            }
            else if (xAxis + zAxis == 0)
            {
                isMoving = false;
                GetComponent<Animator>().SetFloat("forwardSpeed", 0);
            }


            //Running
            if ((Input.GetButton("Run") && nsScript.canMove || Input.GetKey(KeyCode.LeftShift) && player_Stamina <= 20f) && (xAxis + zAxis != 0))
            {
                isDashing = true;
            }
            else if ((Input.GetButtonUp("Run") && nsScript.canMove || Input.GetKeyUp(KeyCode.LeftShift)) || (xAxis + zAxis == 0))
            {
                isDashing = false;
                GetComponent<Animator>().SetFloat("forwardSpeed", 0);
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
                player_Speed = player_SetSpeed; //Set Player speed back to normal
            }

            //Stamina reaches 0
            if (player_CurrentStamina <= 0)
            {
                player_CurrentStamina = 0f;
                player_Speed = player_SetSpeed;
                isDashing = false;

                UpdateAnimator();
            }

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
    }

    private void UpdateAnimator()
    {
        GetComponent<Animator>().SetFloat("forwardSpeed", player_Speed);
    }
}
