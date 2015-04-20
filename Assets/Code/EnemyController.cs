using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class EnemyController : MonoBehaviour {

    float attackDistance = 8.0f;

    private NavMeshAgent m_navMeshAgent;
    private AudioSource[] m_audioSources;
    private Animator m_animator;

    private Coroutine m_moaningCoroutine;


    public AudioClip[] stepClips;

    // Use this for initialization
    void Start ()
    {
        m_navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        m_audioSources = gameObject.GetComponents<AudioSource>();
        m_animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        bool isMoving = false;

        if (Vector3.Distance(GameManager.Instance.playerPosition, transform.position) < attackDistance)
        {
            m_navMeshAgent.SetDestination(GameManager.Instance.playerPosition); // slow when many agents are there
            isMoving = true;
        }
        else
            m_navMeshAgent.Stop();

        if (isMoving)
        {
            m_animator.SetBool("walk", true);
            if (!m_audioSources[0].isPlaying)
            {
                m_audioSources[0].clip = stepClips[Random.Range(0, stepClips.Length)];
                m_audioSources[0].Play();
            }
        }
        else
        {
            m_animator.SetBool("walk", false);
            m_audioSources[0].Stop();
        }
    }
}
