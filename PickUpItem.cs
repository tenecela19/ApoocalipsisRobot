using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    #region Singleton
    private static PickUpItem _instance;

    public static PickUpItem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PickUpItem>();
            }

            return _instance;
        }
    }


    #endregion
    public Mesh WeaponMesh;
    public Material WeaponMaterial;
    public Weapons NewWeapon;
    [HideInInspector]
    public GameObject Weapon;
    public void Start()
    {
        GameController.Instance.HasWeapon = false;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
           GameObject player =  GameObject.FindGameObjectWithTag("Player") ;
            Weapon = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
             GameController.Instance.HasWeapon = true;
            Weapons_Controller.Instance._meshrender.enabled = true;
            Weapons_Controller.Instance.weapons = NewWeapon;
            Weapons_Controller.Instance._meshfilter.mesh = WeaponMesh;
            Weapons_Controller.Instance._meshrender.material = WeaponMaterial;
            
            Destroy(gameObject);
        }
    }
}
