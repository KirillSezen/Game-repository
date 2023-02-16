using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Gun : MonoBehaviour
{
    public float gunDamage = 10f;
    public float gunRange = 100f;

    public int maxAmmo = 20;
    public int currentAmmo;
    public int allAmmo = 90;

    public float reloadTime = 1f;
    public bool isreloading = false;

    private AudioSource gunShot;
    public Camera fpsCam;

    public Text ammo;
    public Animator animator;
    public ParticleSystem muzzleFlash;
    public GameObject bulletImpact;

    
    private bool hasGunFired = false;
    [SerializeField] float gunFireDelay;

    private void Start()
    {
        ammo.text = currentAmmo + "/" + allAmmo;
        currentAmmo = maxAmmo;
        gunShot = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (isreloading)
            return;
        ammo.text = currentAmmo + "/" + allAmmo;

        if (currentAmmo <= 0 || Input.GetKey("r"))
        {
            if (allAmmo == 0) return;
            animator.SetBool("reload", true);
            StartCoroutine(Reload());

            return;
        }

        if (Input.GetButton("Fire1"))
        {
            if (hasGunFired == false)
            {
                StartCoroutine(FireGun());
                
            }
        }
    }

     IEnumerator Reload()
    {
        if (allAmmo < maxAmmo)
        {
            currentAmmo = allAmmo + currentAmmo;
            allAmmo = 0;
        }
        else
        {
            allAmmo -= (maxAmmo - currentAmmo);
            currentAmmo = maxAmmo;
        }

        isreloading = true;
        Debug.Log("reld");

        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("reload", false);

        isreloading = false;
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunRange))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(gunDamage);
                
            }


            GameObject impacts = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impacts, 2f);
        }
    }

    IEnumerator FireGun()
    {
        hasGunFired = true;
        gunShot.Play();
        Shoot();
        yield return new WaitForSeconds(gunFireDelay);
        hasGunFired = false; 
        muzzleFlash.Stop();
    }
}
