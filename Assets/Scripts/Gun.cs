using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 2f;

    public int maxAmmo = 10;
    private int currentAmmo = -1;
    // Could add magazine
    public float reloadTime = 1f;
    private bool isReloading = false;
    public int id;
    public bool equipped = false;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;
    public AudioManager audioManager;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        //animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                audioManager.PlayGunTrigger();
            }
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        // Play sound
        audioManager.PlayGunLoad();
        yield return new WaitForSeconds(reloadTime);
        // Animation
        currentAmmo = maxAmmo;
        audioManager.PlayGunHammer();
        Debug.Log("Reloading...Done");
        isReloading = false;
    }

    void Shoot()
    {
        // Play Muzzle Flash Effect
        muzzleFlash.Play();

        // Play sound
        audioManager.PlayGunShot();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            currentAmmo--;

            // Play Impact Effect (Bullet Force)
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }
}
