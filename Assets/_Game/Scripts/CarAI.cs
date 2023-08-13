
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._Game.Scripts
{
    public class CarAI : MonoBehaviour
    {
        public NavMeshAgent navmeshAgent;
        public CarPath path;

        public int currentPathIndex;
        public bool canMove = true;
        public float minCarHitSpeed;
        

        void FixedUpdate()
        {
            if (!canMove) return;

            if (path.GetCurrentPathTransform(currentPathIndex) != null)
            {
                navmeshAgent.SetDestination(path.GetCurrentPathTransform(currentPathIndex).position);
            }
            if (GetPathAPlayerDistance() < 1f)
            {
                NextPath();
            }

        }
        private float GetPathAPlayerDistance()
        {
            float distance = Vector3.Distance(transform.position, path.GetCurrentPathTransform
                (currentPathIndex).position);
            return distance;
        }
        public void NextPath()
        {
            if (path.nodes.Count - 1 == currentPathIndex)
            {
                currentPathIndex = 0;
                return;
            }
            else
            {
                currentPathIndex++;
            }
        }


    }
}