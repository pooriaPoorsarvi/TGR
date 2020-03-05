using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _tmp_move : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform dest;
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dest != null)
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            bool res = navMeshAgent.CalculatePath(dest.position, navMeshPath);
            for (int i = 0; i < navMeshPath.corners.Length - 1; i++)
                Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i + 1], Color.red);
            // transform.Translate();
        }
    }
}
