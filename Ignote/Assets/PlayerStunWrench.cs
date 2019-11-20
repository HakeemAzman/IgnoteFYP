using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunWrench : MonoBehaviour
{
    public GameObject wrench;
    public float coolDown = 5;
    public bool isAttack;
    public AutoBallista AB;
    // Start is called before the first frame update
    void Start()
    {
        AB.gameObject.GetComponent<AutoBallista>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && !isAttack)
        {
            wrench.SetActive(true);
            isAttack = true;
        }

            if(isAttack)
            {
                coolDown -= 1f * Time.deltaTime;
                if(coolDown <= 4.5f)
                {
                   wrench.SetActive(false);
                }
                if (coolDown <= 0)
                {
                isAttack = false;
                coolDown = 5;
                }
        }
      
    }


}
