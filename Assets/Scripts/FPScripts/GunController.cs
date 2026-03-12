using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject bullet;

    [SerializeField] float fireRate;

    [SerializeField] AudioClip shootSound;

    [SerializeField] ParticleSystem ps;

    [SerializeField] int maxAmmo;
    private int currentAmmo;
    public AudioSource source;
    private float fireTime;
    private bool hasShot = false;

    private void Awake()
    {
     currentAmmo = maxAmmo;   
    }
    public void Update(){
        if (hasShot){
            fireTime += Time.deltaTime;
            if (fireTime >= fireRate) hasShot = false;
        }
    }
    public void Shoot(){
        if (!hasShot && currentAmmo > 0){
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Vector3 pos = hit.point;
                GameObject bul = Instantiate(bullet, pos, Quaternion.identity);
                Destroy(bul, 2f);
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyScript>().getDamaged();
                }
            }
            hasShot = true;
            fireTime = 0f;
            source.clip = shootSound;
            source.Play();
            ps.Play();
            currentAmmo--;
            AmmoController.instance.setAmmo(currentAmmo);
        }
    }

    public void Reload(){
        currentAmmo = maxAmmo;
        AmmoController.instance.setAmmo(currentAmmo);
    }

    public int GetAmmo(){
        return currentAmmo;
    }

    public int GetMaxAmmo(){
        return maxAmmo;
    }
}
