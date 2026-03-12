using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float cooldown;
    NavMeshAgent agent;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gunPoint;
    Animator animator;
    AudioSource[] source;

    private float curTime;
    private bool canShoot = true;
    private int health = 20;
    private bool isDead = false;
    void Start()
    {
     agent = GetComponent<NavMeshAgent>(); 
     animator = GetComponentInChildren<Animator>();
     source = GetComponents<AudioSource>();
     //agent.SetDestination(GameObject.FindWithTag("Player").gameObject.transform.position);
    }
    void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(GameObject.FindWithTag("Player").gameObject.transform.position);
            animator.SetFloat("Blend", agent.speed);
            if (curTime < cooldown && !canShoot) curTime += Time.deltaTime;
            else canShoot = true;
            Debug.DrawRay(gunPoint.transform.position, gunPoint.transform.TransformDirection(Vector3.forward));
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 4f, transform.TransformDirection(Vector3.forward), out hit, 7f) && canShoot)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    transform.LookAt(hit.point);
                    Shoot();
                }
            }
        }
    }

    private void Shoot(){
        animator.SetTrigger("Shoot");
        source[0].Play();
        GameObject laser = Instantiate(bullet, gunPoint.transform);
        laser.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 30f, ForceMode.Impulse);
        Destroy(laser, 1f);
        canShoot = false;
        curTime = 0;
    }

    public void getDamaged()
    {
            health -= 10;
            if (health <= 0 && !isDead)
            {
                Die();
            }
    }

    private void Die()
    {
        Destroy(gameObject, 3f);
        animator.SetFloat("Blend", 0);
        animator.SetLayerWeight(1, 0);
        animator.SetBool("Dead", true);
        isDead = true;
        agent.SetDestination(transform.position);
        FPSceneController.instance.enemiesCount -= 1;
        FPSceneController.instance.BiggerText();
        source[1].Play();
        transform.Rotate(Vector3.right * 90);
        /*if (FPSceneController.instance.enemiesCount == 0)
        {
            FPSceneController.instance.EndLevel();
        }*/
    }
}
