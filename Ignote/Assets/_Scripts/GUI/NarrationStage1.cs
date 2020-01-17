using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrationStage1 : MonoBehaviour
{
    public Text textBox;
    public Text roomNameTxt;
    public RectTransform topBox;
    public RectTransform bottomBox;
    public Vector2 targetPosBottom;
    public Vector2 targetPosTop;
    [SerializeField] float waitForSecs;
    public Image RBbutton;
    public Image XButton;
    public Image LBbutton;
    bool hasColor;
    bool nameHasColor;
    public bool canMove; //PlayerMovement enable girl's running

    private void Update()
    {
        TextBoxColor();
        NameBoxColor();

        if (canMove) LetterBoxMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Pier Dialogue 1")
        {
            textBox.text = "Emily: What was that..?";
            hasColor = true;
        }

        if(other.name == "Pier Dialogue 2")
        {
            textBox.text = "Emily: Where am I..?";
            hasColor = true;
        }

        if (other.name == "Pier Dialogue 3")
        {
            textBox.text = "Emily: What is this place..?";
            hasColor = true;
        }

        if (other.name == "Pier Dialogue 4")
        {
            textBox.text = "Emily: Oh no.. My plane..";
            hasColor = true;
        }

        //if (other.name == "Pier Dialogue 5")
        //{
        //    textBox.text = "Emily: Is that.. A Robot?.. Why is it locked in a cage?";
        //    hasColor = true;
        //}

        if (other.name == "Tutorial1")
        {
            RBbutton.gameObject.SetActive(false);
            XButton.gameObject.SetActive(true);
            textBox.text = "PRESS          to Repair";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial2")
        {
            textBox.text = "The gate seems to be lowered,maybe there's A WAY around it.";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial3")
        {
            LBbutton.gameObject.SetActive(true);
            textBox.text = "Press            to Push the crates";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial4")
        {
            textBox.text = "Did the friendly Robot just protect me? There's two more enemies ahead, maybe I can SUPERCHARGE him with my STUN WRENCH.";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial5")
        {
            textBox.text = "Dang, glad to have him on my side..";
            hasColor = true;
        }

        if (other.name == "Tutorial6")
        {
            textBox.text = "Is that Ballista SHOOTING at me? Damn, but both the Robot and I CAN'T SEEM to reach it!";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial7")
        {
            textBox.text = "There's seem to be an unmanned ballista, maybe if I HIT IT with my ol' wrench, I can MAKE IT WORK for me.";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial8")
        {
            textBox.text = "All in a day's work, now to catch a breather.";
            hasColor = true;
        }

        if (other.name == "BlackBorderCollider")
        {
            textBox.text = " PRESS          to run";
            RBbutton.gameObject.SetActive(true);
            
            hasColor = true;
            canMove = true;
        }

        if(other.name == "Main Hall")
        {
            roomNameTxt.text = "The Great Hall";
            StartCoroutine(DeleteName());
            //nameHasColor = true;
        }

        if (other.name == "Cathedral")
        {
            roomNameTxt.text = "Cathedral Of The Deep";
            StartCoroutine(DeleteName());
            //nameHasColor = true;
        }

        if (other.name == "Study")
        {
            roomNameTxt.text = "Archduchess' Archives";
            StartCoroutine(DeleteName());
            //nameHasColor = true;
        }

        if (other.name == "Archduchess' Archives")
        {
            //roomNameTxt.text = "Archduchess' Archives";
            //nameHasColor = true;
        }

        if (other.name == "Segregation")
        {
            roomNameTxt.text = "Garden Of Seclusion";
            StartCoroutine(DeleteName());
            //nameHasColor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Textboxes"))
        {
            LBbutton.gameObject.SetActive(false);
            XButton.gameObject.SetActive(false);
            hasColor = false;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Names"))
        {
            //nameHasColor = false;
            Destroy(other.gameObject);
        }
    }

    void TextBoxColor()
    {
        // lerps the textbox to have color if the boolean is set true
        if (hasColor)
        {
            textBox.color = Color.Lerp(textBox.color, Color.white, 1f * Time.deltaTime);
        }
        // lerps it back to no alpha if the boolean is false
        else
        {
            textBox.color = Color.Lerp(textBox.color, Color.clear, 1f * Time.deltaTime);
        }
    }

    void NameBoxColor()
    {
        // lerps the textbox to have color if the boolean is set true
        if (nameHasColor)
        {
            roomNameTxt.color = Color.Lerp(roomNameTxt.color, Color.white, 1f * Time.deltaTime);
        }
        // lerps it back to no alpha if the boolean is false
        else
        {
            roomNameTxt.color = Color.Lerp(roomNameTxt.color, Color.clear, 1f * Time.deltaTime);
        }
    }

    void LetterBoxMove()
    {
        bottomBox.localPosition = Vector2.Lerp(bottomBox.localPosition, targetPosBottom, 0.4f * Time.deltaTime);
        topBox.localPosition = Vector2.Lerp(topBox.localPosition, targetPosTop, 0.4f * Time.deltaTime);
    }

    IEnumerator TimeScale()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(waitForSecs);
        Time.timeScale = 1f;
    }

    IEnumerator DeleteName()
    {
        nameHasColor = true;
        yield return new WaitForSeconds(3.5f);
        nameHasColor = false;
    }
}
