using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompanionMovement : MonoBehaviour
{
    [Header("Companion Stats")]
    public float player_Speed = 8f;
    public float player_RunningSpeed = 15f;
    public float player_Stamina = 20f;
    public float player_RunningEnergy = 2f;
    public float player_ShortDash;
    public Slider player_StaminaBar;

    [Header("Mounting")]
    public Companion_Commands cc;
    public CompanionManager cMan;
    public GameObject playerChar;
    public CompanionScript cs;
    public PlayerMovement pm;
    //Private Variables
    Rigidbody rb;
    bool isDashing = false;
    public bool canDismount;
    // Use this for initialization
    void Start()
    {
        canDismount = true;
        rb = GetComponent<Rigidbody>();
        cc = gameObject.GetComponent<Companion_Commands>();

    }

    #region Enemy Movement, Direction facing, Stamina Bar
    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(xAxis, 0, zAxis);

        if (xAxis + zAxis != 0) //If there is no movement, the rotation of the character will not revert back to 0 but instead stay at the currect rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMovement), 0.3f); //Turns to the direction of the character movement

        //Running
        if (Input.GetButton("Run") && player_Stamina <= 20f)
        {
            isDashing = true;
        }

        //Dashing is True
        if (isDashing)
        {
            player_Speed = player_RunningSpeed;
            player_ShortDash -= 1 * Time.deltaTime; //Timer for short dash

            player_Stamina -= player_RunningEnergy * Time.deltaTime;

            if (player_ShortDash < 0)
                isDashing = false;
        }

        //Dashing is False
        if (!isDashing)
        {
            player_Speed = 8f; //Set Player speed back to normal
            player_ShortDash = 3f; //Reset Dash time
            player_Stamina += 1 * Time.deltaTime; //Recharge stamina bar

            if (player_Stamina > 20f)
            {
                player_Stamina = 20f;
            }
        }

        //Stamina reaches 0
        if (player_Stamina < 0)
        {
            player_Stamina = 0f;
            player_Speed = 8f;
            isDashing = false;
        }

        
        if (Input.GetButtonDown("Dismount") && canDismount)
        {

            cMan.enableCompanion = false;
            cMan.enablePlayer = true;

            Dismount();
        }

        //Walking
        transform.Translate(playerMovement * player_Speed * Time.deltaTime, Space.World);

        //Stamina Bar
        player_StaminaBar.value = player_Stamina;

        
        }
    void Dismount()
    {
        playerChar.transform.parent = null;
        playerChar.GetComponent<Rigidbody>().useGravity = true;
        playerChar.GetComponent<Rigidbody>().isKinematic = false;
        cs.gameObject.GetComponent<CompanionScript>().enabled = true;
    }
        
    #endregion

    
}


