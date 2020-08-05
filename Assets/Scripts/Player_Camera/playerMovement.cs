
using System.Collections;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    public float speed = 3f;
    public float rotSpeed = 3f;
    public float gravity = -9.8f;
    public float jumpSpeed = 5f;
    public float wallOffset = 0.4f;

    public Vector3 playerSpawnPoint = new Vector3(19f, 9f, 0f);

    public Joystick joystick;

    private Vector3 movement;

    private bool isJumping;

    private bool isFreez;

    private Vector3 rayDirectionDown;
    private Vector3 rayDirectionLeft;
    private Vector3 rayDirectionRight;

    void Awake()
    {

        InputEvents.WORLD_ROTATE_EVENT.AddListener(OnWorldRotate);

        transform.position = playerSpawnPoint;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    void OnDestroy()
    {
        InputEvents.WORLD_ROTATE_EVENT.RemoveListener(OnWorldRotate);
    }

    void Start()
    { 

        isJumping = false;
        isFreez = false;
        OnWorldRotate();

    }

    void Update()
    {

        checkCrash();

        if ( IsBodyGrounded(wallOffset) && !isJumping)
        {
            gravity = 0f;

        }
        else if(!isJumping )
        {
            gravity = -9.8f;
        }

        if (!isFreez)
        {
            MovementGravityВependence();
            transform.position += movement;
        }

        transform.rotation = Quaternion.identity;

        

    }

    public void OnWorldRotate()
    {
        WorldRotation.Rotation rotation = Managers.WorldRotation.getRotation();
        if (rotation == WorldRotation.Rotation.RIGHT_WALL)
        {
            rayDirectionDown = Vector3.right;
            rayDirectionLeft = Vector3.down;
            rayDirectionRight = Vector3.up;
        }
        else if (rotation == WorldRotation.Rotation.LEFT_WALL)
        {
            rayDirectionDown = Vector3.left;
            rayDirectionLeft = Vector3.up;
            rayDirectionRight = Vector3.down;
        }
        else if (rotation == WorldRotation.Rotation.ROOF)
        {
            rayDirectionDown = Vector3.up;
            rayDirectionLeft = Vector3.right;
            rayDirectionRight = Vector3.left;
        }
        else
        {
            rayDirectionDown = Vector3.down;
            rayDirectionLeft = Vector3.left;
            rayDirectionRight = Vector3.right;
        }
    }

    private void checkCrash()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, transform.localScale.y / 2 + wallOffset))
        {
            if(hit.transform.GetComponent<dangerBlock_Y_movement>() != null)
            {
                Managers.GameManager.RestartScene();
            }
        }
    }

    private bool IsBodyGrounded(float offset)
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDirectionDown, out hit, transform.localScale.y / 2 + offset))
        {
           
            return true;
        }

        return false;
    }


    private Vector3 MovementGravityВependence()
    {


        float x = joystick.Horizontal * speed * Time.deltaTime;
        float z = joystick.Vertical * speed * Time.deltaTime;
        float y = 0f;


        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDirectionLeft, out hit, transform.localScale.y / 2 + wallOffset) && x < 0)
        {
            x = 0f;
        }
        else if (Physics.Raycast(transform.position, rayDirectionRight, out hit, transform.localScale.y / 2 + wallOffset) && x > 0)
        {
            x = 0f;
        }

        switch (Managers.WorldRotation.getRotation())
        {
            case WorldRotation.Rotation.FLOOR:
                movement = new Vector3(x, y, z);
                movement.y = gravity * Time.deltaTime;
                break;
            case WorldRotation.Rotation.RIGHT_WALL:
                movement = new Vector3(y, x , z);
                movement.x = -gravity  * Time.deltaTime ;
                break;
            case WorldRotation.Rotation.ROOF:
                movement = new Vector3(-x, y, z);
                movement.y = -gravity  * Time.deltaTime;
                break;
            case WorldRotation.Rotation.LEFT_WALL:
                movement = new Vector3(y, -x, z);
                movement.x = gravity  * Time.deltaTime;
                break;
        }

        return movement;

    }

  

    public void OnJump()
    {
        if (!isJumping)
        {
            StartCoroutine(Jump());
        }
        
    }

    public void FreezPlayerkVelocity_Gravity(float freezTime)
    {
        StartCoroutine(Freez(freezTime));
    }

    private IEnumerator Freez(float freezTime)
    {
        isFreez = true;

        yield return new WaitForSeconds(freezTime);

        isFreez = false;

    }

    private IEnumerator Jump()
    {
        isJumping = true;
        gravity = 20f;


        yield return new WaitForSeconds(0.3f);

        if (isFreez)
        {
            gravity = 0f;
        }
        else
        {
            gravity = -9.8f;
        }  

        yield return new WaitForSeconds(0.5f);

        isJumping = false;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rayDirectionDown);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rayDirectionLeft);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, rayDirectionRight);
    }

}
