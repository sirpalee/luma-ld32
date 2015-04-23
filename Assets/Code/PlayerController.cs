using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(PlayerInventory))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController instance = null;

    PlayerController()
    {
        instance = this;
    }

    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }

    private Animator m_animator;

    public float walkSpeed = 1.0f;

    [HideInInspector]
    public float expectedDeathTime;

    [HideInInspector]
    public int remainingHealth;

    public float initialLifeTime = 600;
    public AudioClip[] footStepClips;

    private AudioSource m_audioSource;
    private Rigidbody m_rigidBody;
    private PlayerInventory m_inventory;

    // Use this for initialization
    void Start ()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        expectedDeathTime = Time.time + initialLifeTime;
        m_audioSource = gameObject.GetComponent<AudioSource>();
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        m_inventory = gameObject.GetComponent<PlayerInventory>();
        remainingHealth = 6;
    }

    Vector3 GetAimingDirection()
    {
        if (Camera.main == null) // ie the camera has not be done shit with
            return new Vector3(1.0f, 0.0f, 0.0f);
        Ray rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 groundPos = rayFromMouse.origin - rayFromMouse.direction * (rayFromMouse.origin.y / rayFromMouse.direction.y);

        return groundPos - new Vector3(transform.position.x, 0.0f, transform.position.z);

    }
	
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Camera.main == null)
            return;

        Vector3 aimingDirection = GetAimingDirection();
        transform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, 1.0f), aimingDirection);

        Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
            speed.z = Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.S))
           speed.z = -Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.A))
           speed.x = -Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.D))
           speed.x = Time.fixedDeltaTime;

        speed = speed * walkSpeed;

        bool isWalking = speed.sqrMagnitude > 0.001f;

        if (isWalking)
            m_animator.SetBool("walk", true);
        else
            m_animator.SetBool("walk", false);

        transform.position = speed + transform.position;
        // move the camera towards the player
        Vector3 camPos = Camera.main.transform.position;
		Vector3 targetPos = transform.position;
		targetPos.y = camPos.y;

		Camera.main.transform.position = Vector3.MoveTowards(camPos, targetPos, (targetPos - camPos).magnitude / 2.0f);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Instantiate(Resources.Load("Sounds/Reference"), transform.position, Quaternion.identity);
        }

        if (isWalking)
        {
            if (!m_audioSource.isPlaying)
            {
                AudioClip clip = footStepClips[Random.Range(0, footStepClips.Length)];
                m_audioSource.clip = clip;
                m_audioSource.Play();
            }
        }
        else m_audioSource.Stop();

        GameManager.Instance.playerPosition = transform.position;
        m_rigidBody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        m_rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        Vector3 aimingDirection = GetAimingDirection();

        // throwin pies
        if (Input.GetMouseButtonDown(1) && m_inventory.TryThrowingPie())
        {
            GameObject pie = (GameObject)Instantiate(Resources.Load("Items/PieInAir"), transform.position + new Vector3(0.0f, 0.4f, 0.0f), transform.rotation);
            FlyingPie flyingPie = pie.GetComponent<FlyingPie>();
            flyingPie.direction = aimingDirection.normalized;
            m_animator.SetTrigger("throw");
        }
    }
}
