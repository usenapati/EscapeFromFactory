using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{

    public Gun gun;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, holster, fpsCam; 

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            gun.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            gun.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E)) 
            PickUp();

        // Debug
        //Drop if equipped and "Q" is pressed
        //if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(holster);
        transform.localPosition = Vector3.zero; // Needs to be changed
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = false;

        //Enable script
        gun.enabled = true;
        gameObject.SetActive(false);
    }

    public void Drop()
    {
        equipped = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.useGravity = true;
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        

        //Disable script
        gun.enabled = false;
        gameObject.SetActive(true);
        Debug.Log("Dropped weapons");
    }
}
