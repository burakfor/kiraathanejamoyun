using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts
{
    public class CarPath : MonoBehaviour
    {

        public List<Transform> nodes;
        public Color lineColor = Color.red;
        public GameObject[] carPrefabs;
        public int spawnRate;
        public int maxCount;

        private void OnDrawGizmos()
        {
            Gizmos.color = lineColor;
            Transform[] pathTransforms = GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();
            for (int i = 0; i < pathTransforms.Length; i++)
            {
                if (pathTransforms[i] != transform)
                    nodes.Add(pathTransforms[i]);
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                Vector3 currentNode = nodes[i].position;
                Vector3 previousNode = Vector3.zero;
                if (i > 0)
                {
                    previousNode = nodes[i - 1].position;
                }else if (i == 0 && nodes.Count > 1)
                {
                    previousNode = nodes[nodes.Count-1].position;
                }
                Gizmos.DrawLine(previousNode,currentNode);
                Gizmos.DrawWireSphere(currentNode, 0.3f);
            }
        }

        public Transform GetCurrentPathTransform(int currentPathIndex)
        {
            return nodes[currentPathIndex];
        }
        private void Start()
        {
            InvokeRepeating(nameof(SpawnCar), spawnRate, spawnRate);
        }
        private int GetCurrentCarAICount()
        {
            return FindObjectsOfType<CarAI>().Length;
        }
        private GameObject RandomCar()
        {
            int r = Random.Range(0,carPrefabs.Length);
            return carPrefabs[r];
        }
        private void SpawnCar()
        {
            if (GetCurrentCarAICount() <= maxCount)
            {
                var obj = Instantiate(RandomCar());
                int r = UnityEngine.Random.Range(0, nodes.Count);
                obj.transform.position = nodes[r].position;
                CarAI carAI = obj.GetComponent<CarAI>();
                carAI.currentPathIndex = r;
                carAI.path = this;
            }

        }
    }
}