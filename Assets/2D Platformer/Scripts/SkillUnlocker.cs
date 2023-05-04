using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer; // Add this line

public class SkillUnlocker : MonoBehaviour
{
    public string skillToUnlock;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().UnlockSkill(skillToUnlock);
            Destroy(gameObject);
        }
    }
}
