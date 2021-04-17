using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public Transform respawnPoint;
    public Transform holster;
    public List<PickUpController> pickUps;
    public bool respawn = false;
    public PlayerMovement pm;
    //public MouseLook ml;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (respawn)
        {
            transform.position = respawnPoint.position;
            pm.enabled = true;
            respawn = false;
            health = 100;

        }
        foreach (Transform weapon in holster)
        {
            if (weapon.GetComponent<Gun>().id != 0 && !weapon.GetComponent<Gun>().equipped)
            {
                pickUps.Add(weapon.GetComponent<PickUpController>());
                weapon.GetComponent<Gun>().equipped = true;
            }
        }
        if (health <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        respawn = true;
        foreach (PickUpController pickUp in pickUps)
        {
            pickUp.Drop();
        }
        //Destroy(gameObject);
        pm.enabled = false;
    }
}
