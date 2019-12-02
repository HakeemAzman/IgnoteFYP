using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrationStage1 : MonoBehaviour
{
    public Text textBox;
    public RectTransform topBox;
    public RectTransform bottomBox;
    public Vector2 targetPosBottom;
    public Vector2 targetPosTop;
    [SerializeField] float waitForSecs;

    bool hasColor;
    bool canMove;

    private void Update()
    {
        TextBoxColor();

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

        if (other.name == "Pier Dialogue 5")
        {
            textBox.text = "Emily: Is that.. A Robot?.. Why is it locked in a cage?";
            hasColor = true;
        }

        if (other.name == "Tutorial1")
        {
            textBox.text = "It looks like I can call the Robot to me or make it stay by pressing A.";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial2")
        {
            textBox.text = "The gate seems to be lowered,maybe there's a way around it.";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial3")
        {
            textBox.text = "Another Robot? It doesn't look very friendly at all, I can stun him with my stun wrench by pressing X.";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial4")
        {
            textBox.text = "Did the friendly Robot just protect me? There's two more enemies ahead, maybe I can supercharge him with my stun wrench.";
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
            textBox.text = "Is that Ballista shooting at me? Damn, but both the Robot and I can't seem to reach it!";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial7")
        {
            textBox.text = "There's seem to be an unmanned ballista,maybe if I hit it my ol' wrench, I can make it work for me.";
            hasColor = true;
            StartCoroutine(TimeScale());
        }

        if (other.name == "Tutorial8")
        {
            textBox.text = "All in a day's work,now to catch a breather.";
            hasColor = true;
        }

        if (other.name == "BlackBorderCollider")
        {
            canMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Textboxes"))
        {
            hasColor = false;
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

    void LetterBoxMove()
    {
        bottomBox.localPosition = Vector2.Lerp(bottomBox.localPosition, targetPosBottom, 0.4f * Time.deltaTime);
        topBox.localPosition = Vector2.Lerp(topBox.localPosition, targetPosTop, 0.4f * Time.deltaTime);
    }

    IEnumerator TimeScale()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(waitForSecs);
        Time.timeScale = 1f;
    }
}
