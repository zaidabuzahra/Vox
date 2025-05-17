using System.Collections;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class PowerUpdater : MonoBehaviour
{
    public TMP_Text textDisplay;
    public float typingSpeed = 0.05f;

    private string originalText;
    private string powerPrefix = "[ POWER ]: ";

    private void Start()
    {
        StartCoroutine(WaitAndStartPowerUpdate());
    }

    private IEnumerator WaitAndStartPowerUpdate()
    {
        yield return new WaitForSeconds(3f); // Adjust to match your typing duration
        originalText = textDisplay.text;

        if (!originalText.Contains(powerPrefix))
        {
            // Append power line if it's not already there
            originalText += "\n" + powerPrefix + "0%";
        }

        yield return StartCoroutine(UpdatePower());
    }

    private IEnumerator UpdatePower()
    {
        for (int i = 1; i <= 100; i++)
        {
            yield return StartCoroutine(TypeOnlyNumber(i));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator TypeOnlyNumber(int value)
    {
        string numberStr = value.ToString() + "%";
        string baseText = GetTextWithoutPowerLine();
        string current = "";

        foreach (char c in numberStr)
        {
            current += c;
            textDisplay.text = baseText + powerPrefix + current;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private string GetTextWithoutPowerLine()
    {
        string[] lines = originalText.Split('\n');
        string rebuilt = "";

        foreach (string line in lines)
        {
            if (!line.StartsWith(powerPrefix))
                rebuilt += line + "\n";
        }

        return rebuilt;
    }
}
