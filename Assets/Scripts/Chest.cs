using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region Gameobject_variables
    [SerializeField]
    [Tooltip("health pack")]
    private GameObject healthpack;

    #endregion


    #region Check_functions

    IEnumerator DestroyChest()
    {
        yield return new WaitForSeconds(.3f);
        Instantiate(healthpack, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void Interact()
    {
        StartCoroutine("DestroyChest");
    }
    #endregion


}
