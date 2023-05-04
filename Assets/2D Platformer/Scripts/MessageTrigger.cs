using System.Collections;
using UnityEngine;
using TMPro;

public class MessageTrigger : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public string message;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            messageText.text = message;
            StartCoroutine(ClearTextAfterDelay(5f));
        }
    }

    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
}
