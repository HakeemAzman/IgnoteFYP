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

        if(other.name == "BlackBorderCollider")
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
}
