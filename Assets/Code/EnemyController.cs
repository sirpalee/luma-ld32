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

    private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private AudioSource[] _audioSources;
    private Animator _animator;

    private float _lastAttackTime;
    private float _lastMoanTime;
    private float _nextMoanTime;
    private bool _isActive;

    public AudioClip[] stepClips;
    public AudioClip[] moaningClips;
    public AudioClip[] hitClips;

    private Coroutine _reviveTimer;
    private GameObject _staticSplat;

    // Use this for initialization
    private void Start() {
        _navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        _audioSources = gameObject.GetComponents<AudioSource>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        _navMeshAgent.isStopped = true;
        _isActive = true;
        _lastAttackTime = Time.time;
        _lastMoanTime = Time.time;
        _nextMoanTime = Random.Range(moanFrequencyMin, moanFrequencyMax);
        _reviveTimer = null;
        _staticSplat = null;
    }

    // Update is called once per frame
    private void Update() {
        if (!_isActive) {
            return;
        }

        var isMoving = false;

        if (Vector3.Distance(GameManager.Instance.playerPosition, transform.position) < attackDistance) {
            _navMeshAgent.isStopped = false;
            var targetPosition = GameManager.Instance.playerPosition;
            _navMeshAgent.SetDestination(targetPosition); // slow when many agents are there
            isMoving = true;
        } else {
            _navMeshAgent.isStopped = true;
        }

        if (isMoving) {
            _animator.SetBool("walk", true);
            if (!_audioSources[0].isPlaying) {
                _audioSources[0].clip = stepClips[Random.Range(0, stepClips.Length)];
                _audioSources[0].Play();
            }

            if (Time.time - _lastMoanTime > _nextMoanTime) {
                Moan();
                _lastMoanTime = Time.time;
                _nextMoanTime = Random.Range(moanFrequencyMin, moanFrequencyMax);
            }
        } else {
            _animator.SetBool("walk", false);
            _audioSources[0].Stop();
        }
    }

    private void Moan() {
        if (!_audioSources[1].isPlaying) {
            _audioSources[1].clip = moaningClips[Random.Range(0, moaningClips.Length)];
            _audioSources[1].Play();
        }
    }

    private void Hit() {
        if (!_audioSources[2].isPlaying) {
            _audioSources[2].clip = hitClips[Random.Range(0, hitClips.Length)];
            _audioSources[2].Play();
        }
    }

    private void OnCollisionStay(Collision collision) {
        if (!_isActive) {
            return;
        }

        var playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null && Time.time - _lastAttackTime > attackFrequency) {
            _lastAttackTime = Time.time;
            --playerController.remainingHealth;
            Hit();
        }
    }

    public void HitByAPie() {
        if (health == -1) {
            _isActive = false;
            if (_reviveTimer != null) {
                StopCoroutine(_reviveTimer);
            }
            if (_staticSplat == null) {
                _staticSplat = (GameObject) Instantiate(Resources.Load("SplatStatic"),
                    new Vector3(transform.position.x, 0.0f, transform.position.z),
                    Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
                _staticSplat.transform.SetParent(transform);
            }

            _navMeshAgent.isStopped = true;
            _animator.SetBool("walk", false);
            StartCoroutine(ReviveTimer());
            Instantiate(Resources.Load("Items/" + spawnItemAtDeath),
                new Vector3(transform.position.x, 0.0f, transform.position.z),
                Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
        } else if (--health < 1) {
            Instantiate(Resources.Load("Items/" + spawnItemAtDeath),
                new Vector3(transform.position.x, 0.0f, transform.position.z),
                Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
            Destroy(gameObject);
        }
    }

    private IEnumerator ReviveTimer() {
        yield return new WaitForSeconds(reviveTime);
        _isActive = true;
        Destroy(_staticSplat);
        _staticSplat = null;
    }
}