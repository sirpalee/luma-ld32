using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class EnemyController : MonoBehaviour {

    public float attackDistance = 8.0f;
    public float attackFrequency = 1.5f;

    public float moanFrequencyMin = 4.0f;
    public float moanFrequencyMax = 12.0f;

    public float reviveTime = 30.0f;

    public string spawnItemAtDeath = "coin";
    public int health = -1;

    private UnityEngine.AI.NavMeshAgent m_navMeshAgent;
    private AudioSource[] m_audioSources;
    private Animator m_animator;

    private float m_lastAttackTime;
    private float m_lastMoanTime;
    private float m_nextMoanTime;
    private bool m_isActive;

    public AudioClip[] stepClips;
    public AudioClip[] moaningClips;
    public AudioClip[] hitClips;

    private Coroutine m_reviveTimer;
    private GameObject m_staticSplat;

    // Use this for initialization
    void Start ()
    {
        m_navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_audioSources = gameObject.GetComponents<AudioSource>();
        m_animator = gameObject.GetComponentInChildren<Animator>();
        m_navMeshAgent.Stop();
        m_isActive = true;
        m_lastAttackTime = Time.time;
        m_lastMoanTime = Time.time;
        m_nextMoanTime = Random.Range(moanFrequencyMin, moanFrequencyMax);
        m_reviveTimer = null;
        m_staticSplat = null;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!m_isActive)
            return;

        bool isMoving = false;

        if (Vector3.Distance(GameManager.Instance.playerPosition, transform.position) < attackDistance)
        {
            m_navMeshAgent.Resume();
            Vector3 targetPosition = GameManager.Instance.playerPosition;
            m_navMeshAgent.SetDestination(targetPosition); // slow when many agents are there
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

            if ((Time.time - m_lastMoanTime) > m_nextMoanTime)
            {
                Moan();
                m_lastMoanTime = Time.time;
                m_nextMoanTime = Random.Range(moanFrequencyMin, moanFrequencyMax);
            }
        }
        else
        {
            m_animator.SetBool("walk", false);
            m_audioSources[0].Stop();
        }
    }

    void Moan()
    {
        if (!m_audioSources[1].isPlaying)
        {
            m_audioSources[1].clip = moaningClips[Random.Range(0, moaningClips.Length)];
            m_audioSources[1].Play();
        }
    }

    void Hit()
    {
        if (!m_audioSources[2].isPlaying)
        {
            m_audioSources[2].clip = hitClips[Random.Range(0, hitClips.Length)];
            m_audioSources[2].Play();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (!m_isActive)
            return;

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if ((Time.time - m_lastAttackTime) > attackFrequency)
            {
                m_lastAttackTime = Time.time;
                --playerController.remainingHealth;
                Hit();
            }
        }
    }

    public void HitByAPie()
    {
        if (health == -1)
        {
            m_isActive = false;
            if (m_reviveTimer != null)
                StopCoroutine(m_reviveTimer);
            if (m_staticSplat == null)
            {
                m_staticSplat = (GameObject)Instantiate(Resources.Load("SplatStatic"),
                                                        new Vector3(transform.position.x, 0.0f, transform.position.z),
                                                        Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
                m_staticSplat.transform.SetParent(transform);
            }
            m_navMeshAgent.Stop();
            m_animator.SetBool("walk", false);
            StartCoroutine(ReviveTimer());
            Instantiate(Resources.Load("Items/" + spawnItemAtDeath),
                        new Vector3(transform.position.x, 0.0f, transform.position.z),
                        Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
        }
        else
        {
            if (--health < 1)
            {
                Instantiate(Resources.Load("Items/" + spawnItemAtDeath),
                            new Vector3(transform.position.x, 0.0f, transform.position.z),
                            Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator ReviveTimer()
    {
        yield return new WaitForSeconds(reviveTime);
        m_isActive = true;
        Destroy(m_staticSplat);
        m_staticSplat = null;
    }
}
