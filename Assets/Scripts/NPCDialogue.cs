using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text dialogueBox;
    public AudioSource audioSource;
    public string[] lines;

    int currentLine;

    private void Start() {
        dialogueBox.text = "Click me to talk!";
        currentLine = 0;
    }

    public void Speak() {

        if (currentLine >= lines.Length) {
            //reset
            currentLine = 0;
            dialogueBox.text = "";

        } else if (lines.Length > 0) {
            //show in text above head
            dialogueBox.text = lines[currentLine];

            //increment current line
            currentLine += 1;

            //play animal sound
            audioSource.Play();
        }
    }
}
