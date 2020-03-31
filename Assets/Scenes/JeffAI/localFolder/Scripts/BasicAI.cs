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

        public LevelFinisher levelFinisher;
        
        [Header("Setting Up Animation")] public bool animationBased = true;
        public Animator animator;
        public float damping = 0.05f;


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


        public float waitTime;
        private IEnumerator waitTimer;
        public float scaredMinPeriod;
        private IEnumerator scaredTimer;
        public bool isFreaky = false;

        public GameObject sampleVisionRadiusSphere;
        private GameObject curVisionRadiusSphere;
        private bool visionSetup = false;
        public GameObject npcModel;
        public float visionOffsetY;

        public GameObject player;
        public float showVisionDistance;

        void Awake()
        {
            curVisionRadiusSphere = (GameObject) Instantiate(sampleVisionRadiusSphere,
                npcModel.transform.position,
                Quaternion.identity);
            curVisionRadiusSphere.transform.parent = transform.parent;

            curVisionRadiusSphere.transform.position = new Vector3(npcModel.transform.position.x,
                transform.position.y + visionOffsetY, npcModel.transform.position.z);
            curVisionRadiusSphere.transform.localScale = sampleVisionRadiusSphere.transform.localScale;
            curVisionRadiusSphere.transform.rotation = new Quaternion(1f, sampleVisionRadiusSphere.transform.rotation.x,
                npcModel.transform.rotation.y, sampleVisionRadiusSphere.transform.rotation.z);

            visionSetup = true;
        }

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
            isFreaky = false;
            wantsToEscape = false;
            scaredIndicator.active = false;
            NextGoal();
        }

        float getNonAnimationSpeed()
        {
            return wantsToEscape ? baseSpeed + speedBoost : baseSpeed;
        }

        float getAnimationSpeed()
        {
            return wantsToEscape ? 1f : .707f;
        }


        public bool IsFreaky()
        {
            return isFreaky;
        }


        public void BecomeScared()
        {
            if (wantsToEscape)
            {
                return;
            }
            wantsToEscape = true;
            scaredIndicator.active = true;
            NextGoal();
        }
        
        // public void BecomeScared()
        // {
        //     if (isFreaky)
        //     {
        //         return;
        //     }
        //
        //     isFreaky = true;
        //     scaredIndicator.active = true;
        //     NextGoal();
        //     
        //     
        //     // gameObject.GetComponent<NavMeshAgent>().speed += speedBoost;
        //     // if (scaredTimer != null)
        //     // {
        //     //     StopCoroutine(scaredTimer);
        //     // }
        //     //
        //     // // start scared timer
        //     // scaredTimer = WaitAndBecomeUnscared(scaredMinPeriod);
        //     // StartCoroutine(scaredTimer);
        //     //
        //     //
        //     // if (waitTimer != null)
        //     // {
        //     //     StopCoroutine(waitTimer);
        //     //     NextGoal();
        //     // }
        // }

        public Transform agent;


        IEnumerator WaitAndBecomeUnscared(float mins)
        {
            float counter = mins;

            int hours = (int) (mins / 60);
            int minutes = (int) (mins - hours * 60);
            float secs = (mins - (int) mins) * 60;

            Debug.Log("hours " + hours);
            Debug.Log("minutes " + minutes);
            Debug.Log("secs " + secs);


            String hourText = "";
            String minText = "";
            String secText = "";


            while (true)
            {
                if (secs > 0)
                {
                    secs--;
                }
                else if (minutes > 0)
                {
                    secs = 59;
                    minutes--;
                }
                else if (hours > 0)
                {
                    minutes += 59;
                    hours--;
                    secs = 59;
                }

                if (hours >= 10)
                {
                    hourText = hours.ToString();
                }
                else
                {
                    hourText = "0" + hours.ToString();
                }

                if (minutes >= 10)
                {
                    minText = minutes.ToString();
                }
                else
                {
                    minText = "0" + minutes.ToString();
                }

                if (secs >= 10)
                {
                    secText = secs.ToString();
                }
                else
                {
                    secText = "0" + secs.ToString();
                }

                //timerText = hourText + ":" + minText + ":" + secText; 
                timerText = minText + ":" + secText;

                yield return new WaitForSeconds(1f);

                if (hours == 0 && minutes == 0 && secs == 0)
                {
                    CalmDown();
                    break;
                }
            }
        }


        void Update()
        {
            if (visionSetup)
            {
                if (!isFreaky || Vector3.Distance(transform.position, player.transform.position) > showVisionDistance)
                {
                    curVisionRadiusSphere.active = false;
                }
                else
                {
                    curVisionRadiusSphere.active = true;
                    curVisionRadiusSphere.transform.position = new Vector3(npcModel.transform.position.x,
                        transform.position.y + visionOffsetY, npcModel.transform.position.z);
                    curVisionRadiusSphere.transform.rotation = new Quaternion(1f,
                        sampleVisionRadiusSphere.transform.rotation.x, npcModel.transform.rotation.y,
                        sampleVisionRadiusSphere.transform.rotation.z);
                }
            }
        }

        void FixedUpdate()
        {
            animator.speed = 80f * Time.deltaTime;
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
                    levelFinisher.FinishGame(false);
                }
            }
        }

        private NavMeshPath path;
        IEnumerator tempDisableNavMesh(float secondsOff, float secondsOn)
        {
            navAgent.SetDestination(curGoal.transform.position);
            path = navAgent.path;
            yield return new WaitForSeconds(secondsOn);
            navAgent.enabled = false;
            yield return new WaitForSeconds(secondsOff);
            navAgent.enabled = true;
        }

        private void Move()
        {
            if (curGoal != null)
            {
                if (navAgent.enabled)
                {
                    StartCoroutine(tempDisableNavMesh(.5f*Time.deltaTime * 60, .02f*Time.deltaTime * 60));
                }
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

            // NavMeshPath path = navAgent.path;
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

        public bool SetNoAction(bool noAction)
        {
            return this.noAction = noAction;
        }

        // private void NextGoal()
        // {
        //     Transform newGoal;
        //     if (!wantsToEscape && !noAction)
        //     {
        //         newGoal = patrolArea.RequestWaypoint(curWaypointIndex);
        //     }
        //     else if (!noAction)
        //     {
        //         newGoal = patrolArea.RequestEscapeWaypoint();
        //     }
        //     else
        //     {
        //         // quit the function if player grabbed the npc
        //         return;
        //     }
        //
        //     if (newGoal != null)
        //     {
        //         curGoal = newGoal;
        //         curWaypointIndex++;
        //     }
        //     else if (doesLoopBeetweenWayPoints && patrolArea.GetWaypointCount() > 0)
        //     {
        //         curWaypointIndex = 0;
        //         NextGoal();
        //     }
        //     else if (curWaypointIndex > 0 && loopBackInWayPoints)
        //     {
        //         curWaypointIndex--;
        //         NextGoal();
        //     }
        // }
        private void NextGoal()
        {
            Transform newGoal;
            if (!wantsToEscape && !noAction)
            {
                newGoal = patrolArea.RequestWaypoint(curWaypointIndex);
            }
            else if (!noAction)
            {
                newGoal = patrolArea.RequestEscapeWaypoint();
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