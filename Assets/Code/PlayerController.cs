using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerController : MonoBehaviour {
    private static PlayerController instance;

    private PlayerController() {
        instance = this;
    }

    public static PlayerController Instance {
        get { return instance; }
    }

    private Animator m_animator;

    public float walkSpeed = 1.0f;

    [HideInInspector] public float expectedDeathTime;

    [HideInInspector] public int remainingHealth;

    public float initialLifeTime = 600;
    public AudioClip[] footStepClips;

    private AudioSource m_audioSource;
    private Rigidbody m_rigidBody;
    private PlayerInventory m_inventory;

    private void Start() {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        expectedDeathTime = Time.time + initialLifeTime;
        m_audioSource = gameObject.GetComponent<AudioSource>();
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        m_inventory = gameObject.GetComponent<PlayerInventory>();
        remainingHealth = 6;
    }

    private Vector3 GetAimingDirection() {
        if (Camera.main == null) // ie the camera has not be done shit with
            return new Vector3(1.0f, 0.0f, 0.0f);
        var rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        var groundPos = rayFromMouse.origin -
                        rayFromMouse.direction * (rayFromMouse.origin.y / rayFromMouse.direction.y);

        return groundPos - new Vector3(transform.position.x, 0.0f, transform.position.z);
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (Camera.main == null)
            return;

        var aimingDirection = GetAimingDirection();
        transform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, 1.0f), aimingDirection);

        var speed = new Vector3(0.0f, 0.0f, 0.0f);
        speed.z = Input.GetAxis("Vertical") * Time.fixedDeltaTime;
        speed.x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime;

        speed = speed * walkSpeed;

        var isWalking = speed.sqrMagnitude > 0.001f;

        if (isWalking) {
            m_animator.SetBool("walk", true);
        } else {
            m_animator.SetBool("walk", false);
        }

        transform.position = speed + transform.position;
        // move the camera towards the player
        var camPos = Camera.main.transform.position;
        var targetPos = transform.position;
        targetPos.y = camPos.y;

        Camera.main.transform.position = Vector3.MoveTowards(camPos, targetPos, (targetPos - camPos).magnitude / 2.0f);

        if (Input.GetMouseButtonDown(0)) {
            Instantiate(Resources.Load("Sounds/Reference"), transform.position, Quaternion.identity);
        }

        if (isWalking) {
            if (!m_audioSource.isPlaying) {
                var clip = footStepClips[Random.Range(0, footStepClips.Length)];
                m_audioSource.clip = clip;
                m_audioSource.Play();
            }
        } else { m_audioSource.Stop(); }

        GameManager.Instance.playerPosition = transform.position;
        m_rigidBody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        m_rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void Update() {
        var aimingDirection = GetAimingDirection();

        // throwin pies
        if (Input.GetButtonUp("Throw") && m_inventory.TryThrowingPie()) {
            var pie = (GameObject) Instantiate(Resources.Load("Items/PieInAir"),
                transform.position + new Vector3(0.0f, 0.4f, 0.0f), transform.rotation);
            var flyingPie = pie.GetComponent<FlyingPie>();
            flyingPie.direction = aimingDirection.normalized;
            m_animator.SetTrigger("throw");
        }
    }
}