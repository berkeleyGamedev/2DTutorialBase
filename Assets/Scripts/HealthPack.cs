using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    #region HealthPack_variables
    [SerializeField]
    [Tooltip("the amount the player heals")]
    private int healamount;
    #endregion

    #region Heal_functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().Heal(healamount);

            Destroy(this.gameObject);
        }
    }
    #endregion
}
