using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent navmeshAgent;
    public CapsuleCollider capsuleCollider;
    public PedestrianPath path;

    public int currentPathIndex;
    public bool canMove = true;
    public float minCarHitSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        if (!canMove) return;

        animator.SetBool("isMove", navmeshAgent.velocity.normalized.magnitude > 0.1f);
        if (path.GetCurrentPathTransform(currentPathIndex)!=null)
        {
            navmeshAgent.SetDestination(path.GetCurrentPathTransform(currentPathIndex).position);
        }
        if (GetPathAPlayerDistance()<.2f)
        {
            NextPath();
        }

    }
    private float GetPathAPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, path.GetCurrentPathTransform
            (currentPathIndex).position) ;
        return distance;
    }
    public void NextPath()
    {
        if (path.paths.Count - 1 == currentPathIndex)
        {
            currentPathIndex = 0;
            return;
        }
        else
        {
            currentPathIndex++;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        var carController = collision.gameObject.GetComponent<CarController>();
        if (carController != null)
        {
            if(carController.GetCarSpeed()< minCarHitSpeed) return;

            canMove = false;
            animator.enabled = false;
            capsuleCollider.enabled = false;
            navmeshAgent.enabled = false;
            Destroy(gameObject,5f);
        }
    }

}
