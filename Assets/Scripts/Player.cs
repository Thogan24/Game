using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController controller;

    public int playerHealth = 20;
    public int playerDamage = 1;
    public GameObject Enemy;
    public GameObject particleEffects;
    public GameObject player;


    // Movement Variable
    public float speed = 30f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        //Creates an invisible sphere below the player in which checks whats below it.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Should be 0 but -2 works better
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); // We multiply by 2 cause the formula is delta y = 1/2g * t^2



        if (transform.GetComponent<Collider>().bounds.Intersects(Enemy.GetComponent<Collider>().bounds))
        {
            //particleEffects.SetActive(true);
            DamagePlayer();
        }
        else
        {
            //particleEffects.SetActive(false);
        }
        if (transform.GetChild(3).GetComponent<Collider>().bounds.Intersects(Enemy.GetComponent<Collider>().bounds))
        {
            particleEffects.SetActive(true);
        }
        else
        {
            particleEffects.SetActive(false);
        }
    }

    void DamagePlayer()
    {
        Debug.Log(playerHealth);
        playerHealth--;
    }


}
