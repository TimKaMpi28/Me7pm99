using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected NavMeshAgent agent;

    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject gunPoint;
    protected Animator animator;
    protected AudioSource[] source;

    protected float curTime;
    protected bool canShoot = true;
    [SerializeField] protected int health = 20;
    protected bool isDead = false;
    virtual protected void Start()
    {
     agent = GetComponent<NavMeshAgent>(); 
     animator = GetComponentInChildren<Animator>();
     source = GetComponents<AudioSource>();
     //agent.SetDestination(GameObject.FindWithTag("Player").gameObject.transform.position);
    }
    protected virtual void Update()
    {
        if (!isDead)
        {
            var player = GameObject.FindWithTag("Player");
            agent.SetDestination(player.gameObject.transform.position);
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
        Debug.Log("damaged enemy");
            health -= 10;
            if (health <= 0 && !isDead)
            {
                Die();
            }
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 3f);
        animator.SetFloat("Blend", 0);
        animator.SetLayerWeight(1, 0);
        animator.SetBool("Dead", true);
        isDead = true;
        agent.SetDestination(transform.position);
        FPSceneController.instance.EnemyKilled();
        source[1].Play();
        transform.Rotate(Vector3.right * 90);
        /*if (FPSceneController.instance.enemiesCount == 0)
        {
            FPSceneController.instance.EndLevel();
        }*/
    }
}
