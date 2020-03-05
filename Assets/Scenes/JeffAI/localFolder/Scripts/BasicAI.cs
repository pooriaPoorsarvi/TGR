using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;
using UnityEngine.Serialization;

namespace JeffAI
{
    public class BasicAI : MonoBehaviour
    {
        [Header("Setting Up Animation")] public bool animationBased = true;
        public Animator animator;
        public float damping = 0.15f;


        [Header("Vision")] public PlayerFinder playerFinder;


        [Header("Movement For Non Animation Movement")]
        public float baseSpeed = 10f;

        public float speedBoost = 10f;


        // number of seconds for which npc remains scared
        [Header("NPC Objects")] public GameObject scaredIndicator;
        public ParticleSystem bloodParticles;

        [Header("Moving Between WayPoints")] [FormerlySerializedAs("loop")]
        public bool doesLoopBeetweenWayPoints = false;

        [FormerlySerializedAs("pingPong")] public bool loopBackInWayPoints = false;

        [Header("Patrolling Properties")] [FormerlySerializedAs("repathDist")]
        public float distanceBeforeStopping;

        public PatrolArea patrolArea;

        private readonly int m_hor = Animator.StringToHash("Horizontal");
        private readonly int m_ver = Animator.StringToHash("Vertical");
        private bool isMoving = false;
        private NavMeshAgent navAgent;
        private String timerText;

        private int curWaypointIndex = 0;
        private Transform curGoal;

        private bool noAction = false;

        // variable that indicates whether npc wants to escape from the level
        private bool wantsToEscape = false;

        public String GetTimerText()
        {
            return timerText;
        }


        public void Hurt()
        {
            bloodParticles.Play();
        }

        public void Die()
        {
            bloodParticles.Play();
            noAction = true;
        }


        // transition back into normal routine after being scared
        private void CalmDown()
        {
            wantsToEscape = false;
            scaredIndicator.active = false;
        }

        float getNonAnimationSpeed()
        {
            return wantsToEscape ? baseSpeed + speedBoost : baseSpeed;
        }

        float getAnimationSpeed()
        {
            return wantsToEscape ? 1f : .707f;
        }

        public void BecomeScared()
        {
            if (wantsToEscape)
            {
                return;
            }

            wantsToEscape = true;
            scaredIndicator.active = true;
        }

        public Transform agent;

        void Update()
        {
            navAgent.SetDestination(curGoal.transform.position);
            if (isMoving)
            {
                float dist = Vector3.Distance(transform.position, curGoal.position);

                CheckRemainderOfPath(dist);
                Move();
            }
        }

        private void CheckRemainderOfPath(float distLeft)
        {
            if (distLeft <= distanceBeforeStopping)
            {
                animator.SetFloat(m_hor, 0, damping, Time.deltaTime);
                animator.SetFloat(m_ver, 0, damping, Time.deltaTime);
                if (!wantsToEscape)
                {
                    NextGoal();
                }
                else
                {
                    // npc that was trying to escape the map has reached its goal, make player lose the game
                    GameplayManager.LoseGame();
                }
            }
        }


        private void Move()
        {
            if (curGoal != null)
            {
                NavMeshPath path = navAgent.path;
                if (path.corners.Length < 2)
                {
                    return;
                }

                Vector3 target = path.corners[1];
                target.y = transform.position.y;
                if (animationBased)
                {
                    Vector3 playerP = transform.InverseTransformPoint(transform.position);
                    Vector3 targetP = transform.InverseTransformPoint(target);
                    animator.SetFloat(m_hor,
                        getAnimationSpeed() * computeAnimatorValue(playerP.x,
                            targetP.x), damping, Time.deltaTime);
                    animator.SetFloat(m_ver,
                        getAnimationSpeed() * computeAnimatorValue(playerP.z,
                            targetP.z), damping, Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, target,
                        getNonAnimationSpeed() * Time.deltaTime);
                }

                isMoving = true;
            }
        }

        public virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (navAgent == null)
            {
                return;
            }

            NavMeshPath path = navAgent.path;
            for (int z = 0; z < path.corners.Length - 1; z++)
                Gizmos.DrawLine(path.corners[z], path.corners[z + 1]);
        }

        private float computeAnimatorValue(float playerTransformMember, float targetTransformMember)
        {
            if (playerTransformMember > targetTransformMember)
            {
                return -1;
            }
            else if (playerTransformMember == targetTransformMember)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private void NextGoal()
        {
            Transform newGoal;
            if (!wantsToEscape && !noAction)
            {
                newGoal = patrolArea.RequestWaypoint(curWaypointIndex);
            }
            else if (!noAction)
            {
                newGoal = patrolArea.RequestRandomWaypoint();
            }
            else
            {
                // quit the function if player grabbed the npc
                return;
            }

            if (newGoal != null)
            {
                curGoal = newGoal;
                curWaypointIndex++;
            }
            else if (doesLoopBeetweenWayPoints && patrolArea.GetWaypointCount() > 0)
            {
                curWaypointIndex = 0;
                NextGoal();
            }
            else if (curWaypointIndex > 0 && loopBackInWayPoints)
            {
                curWaypointIndex--;
                NextGoal();
            }
        }


        public bool PlayerWantsToEscape()
        {
            return wantsToEscape;
        }

        void Start()
        {
            playerFinder.NoticedPlayer += BecomeScared;
            playerFinder.StopScarce += CalmDown;
            navAgent = GetComponent<NavMeshAgent>();
            isMoving = true;
            NextGoal();
        }
    }
}