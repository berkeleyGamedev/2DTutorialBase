using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    #region healthPack_variables
    private PlayerController playerScript;
    private GameObject player;

    [SerializeField]
    [Tooltip("Assign the healing value of the health pack!")]
    private int healAmount;
   
    #endregion

    #region sound_variables
    [SerializeField]
    [Tooltip("Assign the sound clip here")]
    private AudioClip healSound;

    [SerializeField]
    [Tooltip("AudioSource to be dragged in")]
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("Adjust the volume as needed")]
    private float volume;

    #endregion


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Player"))
        {
            coll.transform.GetComponent<PlayerController>().Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
}
