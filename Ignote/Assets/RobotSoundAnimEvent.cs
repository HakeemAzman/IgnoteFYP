using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RobotSoundAnimEvent : MonoBehaviour
{
    public AudioClip PowerOn;
    public AudioClip PowerDown;
    public AudioClip RobotSmash;
    public AudioClip RobotWalk;
    public AudioSource aS;


    void RobotWalking()
    {
        aS.PlayOneShot(RobotWalk);
    }

    void RobotSmashing()
    {
        aS.PlayOneShot(RobotSmash);
    }
    
    void RobotStun()
    {
        aS.PlayOneShot(PowerDown,3);
    }

    void RobotRecover()
    {
        aS.PlayOneShot(PowerOn,3);
    }
}
