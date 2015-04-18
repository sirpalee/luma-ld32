using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance = null;
    private GameObject closestItemToUse;

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

    public float initialLifeTime = 600;

    // Use this for initialization
    void Start ()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        expectedDeathTime = Time.time + initialLifeTime;
        closestItemToUse = null;
    }
	
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Camera.main == null) // ie the camera has not be done shit with
            return;
        Ray rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 groundPos = rayFromMouse.origin - rayFromMouse.direction * (rayFromMouse.origin.z / rayFromMouse.direction.z);

        Vector3 aimingDirection = groundPos - transform.position;
        transform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, -1.0f, 0.0f), aimingDirection);

        Vector3 speed = new Vector3(0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
            speed.y = Time.fixedDeltaTime;
       else if (Input.GetKey(KeyCode.S))
           speed.y = -Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.A))
           speed.x = -Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.D))
           speed.x = Time.fixedDeltaTime;

        speed = speed * walkSpeed;

        if (speed.sqrMagnitude > 0.001f)
            m_animator.SetBool("walk", true);
        else
            m_animator.SetBool("walk", false);

        transform.position = speed + transform.position;

        // move the camera towards the player
        Vector3 camPos = Camera.main.transform.position;
		Vector3 targetPos = transform.position;
		targetPos.z = camPos.z;

		Camera.main.transform.position = Vector3.MoveTowards(camPos, targetPos, (targetPos - camPos).magnitude / 2.0f);
    }

    // TODO : UGH this was a bad choice... redesign this please!
    void OnTriggerEnter2D(Collider2D collider)
    {
        WendingMachine wendingMachine = collider.gameObject.GetComponent<WendingMachine>();
        if (wendingMachine != null)
        {
            if (closestItemToUse == null)
            {
                closestItemToUse = wendingMachine.gameObject;
                wendingMachine.SetCanvas(true);
            }
            else if (Vector3.Distance(closestItemToUse.transform.position, transform.position) < 
                     Vector3.Distance(collider.transform.position, transform.position))
            {
                ShelfScript shelf = closestItemToUse.GetComponent<ShelfScript>();
                if (shelf != null)
                    shelf.SetCanvas(false);
                else
                {
                    WendingMachine wm = closestItemToUse.GetComponent<WendingMachine>();
                    wm.SetCanvas(false);
                }
                closestItemToUse = wendingMachine.gameObject;
                wendingMachine.SetCanvas(true);
            }
        }
        else
        {
            ShelfScript shelf = collider.gameObject.GetComponent<ShelfScript>();
            if (shelf != null)
            {
                if (closestItemToUse == null)
                {
                    closestItemToUse = shelf.gameObject;
                    shelf.SetCanvas(true);
                }
                else if (Vector3.Distance(closestItemToUse.transform.position, transform.position) < 
                         Vector3.Distance(collider.transform.position, transform.position))
                {
                    ShelfScript sh = closestItemToUse.GetComponent<ShelfScript>();
                    if (sh != null)
                        sh.SetCanvas(false);
                    else
                    {
                        WendingMachine wm = closestItemToUse.GetComponent<WendingMachine>();
                        wm.SetCanvas(false);
                    }
                    closestItemToUse = shelf.gameObject;
                    shelf.SetCanvas(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == closestItemToUse)
        {
            ShelfScript shelf = collider.gameObject.GetComponent<ShelfScript>();
            if (shelf != null)
                shelf.SetCanvas(false);
            else
            {
                WendingMachine wendingMachine = collider.gameObject.GetComponent<WendingMachine>();
                if (wendingMachine != null)
                    wendingMachine.SetCanvas(false);
            }
            closestItemToUse = null;
        }
    }
}
