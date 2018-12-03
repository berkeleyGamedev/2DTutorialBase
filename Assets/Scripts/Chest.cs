using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    #region gameObject_variables

    [SerializeField]
    [Tooltip("The healthpack that the chest drops")]
    private GameObject healthPack;

    private Transform chestTransform;

    private bool opened;
    #endregion

    #region sound_variables
    [SerializeField]
    [Tooltip("Assign the sound clip here")]
    private AudioClip chestOpen;

    [SerializeField]
    [Tooltip("AudioSource to be dragged in")]
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("Adjust the volume as needed")]
    private float volume;


    #endregion

    #region helper_functions
    public void Interact() 
    {
        StartCoroutine(DeleteChest());
    }

    IEnumerator DeleteChest() 
    {
        audioSource.volume = volume;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        Instantiate(healthPack, transform.position, transform.rotation);
        Destroy(gameObject); 
    }
    #endregion


    //Called once on creation
    void Awake () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = chestOpen;
	}
}
