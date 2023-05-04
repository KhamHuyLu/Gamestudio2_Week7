using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;

    private int coinCounter = 0;
    private string[] coinDescriptions = new string[]
    {
        "Eerie forests surround you, with trees that seem to whisper secrets.",
    "In the foggy swamps, ghostly figures lurk, waiting for their next victim.",
    "Winds howl through the desolate canyons, carrying the faint cries of lost souls.",
    "The ancient graveyard, filled with decaying tombstones, is haunted by vengeful spirits.",
    "The abandoned village has a sinister atmosphere, and the silence is chilling.",
    "Dark caves hide malicious phantoms, eager to prey on the living.",
    "At the edge of the abyss, the restless spirits of the damned reach out to drag you down.",
    "The enchanted forest is bewitching, but ghosts hide in its shadowy depths.",
    "In this mysterious world, danger lurks around every corner, and even the ghosts can kill you."
    };

    public void CollectCoin()
    {
        if (coinCounter < coinDescriptions.Length)
        {
            descriptionText.text = coinDescriptions[coinCounter];
            coinCounter++;

            // Start the coroutine to clear the text after 5 seconds
            StartCoroutine(ClearTextAfterDelay(5f));
        }
    }

    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        descriptionText.text = "";
    }
}