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
    [Space]
    public float jumpHeight;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //Private Variables
    Rigidbody rb;
    bool isDashing = false;
    bool isJumping = true;
    float jumpTimer = 2f;
    #endregion

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();	
	}

  
    // Update is called once per frame
    void Update ()
    {
        #region Enemy Movement, Direction facing, Stamina Bar
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        
        Vector3 playerMovement = new Vector3(xAxis, 0, zAxis);
       
        playerMovement = Camera.main.transform.TransformDirection(playerMovement);
        playerMovement.y = 0.0f;

        if (xAxis + zAxis != 0) //If there is no movement, the rotation of the character will not revert back to 0 but instead stay at the currect rotation
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMovement), 0.3f); //Turns to the direction of the character movement
            UpdateAnimator();
        }
        else if (xAxis + zAxis == 0)
        {
            GetComponent<Animator>().SetFloat("forwardSpeed", 0);

        }
        

        //Running
        if ((Input.GetButton("Run") || Input.GetKey(KeyCode.LeftShift) && player_Stamina <= 20f) && (xAxis + zAxis != 0))
        {
            isDashing = true;
        }
        else if ((Input.GetButtonUp("Run") || Input.GetKeyUp(KeyCode.LeftShift)) || (xAxis + zAxis == 0))
        {
            isDashing = false;
            GetComponent<Animator>().SetFloat("forwardSpeed", 0);
        }

        //Dashing is True
        if (isDashing)
        {
            player_Speed = player_RunningSpeed;
           /* player_ShortDash -= 1 * Time.deltaTime; //Timer for short dash

            player_CurrentStamina -= player_RunningEnergy * Time.deltaTime;

            if(player_ShortDash < 0)
                isDashing = false;*/
            UpdateAnimator();
        }

        //Dashing is False
        else if (!isDashing)
        {
            player_Speed = player_SetSpeed; //Set Player speed back to normal
           /* player_ShortDash = 3f; //Reset Dash time
            player_CurrentStamina += 1 * Time.deltaTime; //Recharge stamina bar

            if(player_CurrentStamina > 20f)
            {
                player_CurrentStamina = 20f;
            }*/
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

        #endregion

        #region Jumping and altering the mass

        if(Input.GetButtonDown("Jump") && isJumping)
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
