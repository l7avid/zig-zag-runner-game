using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Transform rayStart;
    public GameObject crystalEffect;

    private Rigidbody rigidBody;
    private bool isWakingRight = true;
    private Animator animator;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectsByType<GameManager>(FindObjectsSortMode.None)[0];
    }

    private void FixedUpdate()
    {
        if(!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            animator.SetTrigger("isStarting");
        }

        rigidBody.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCharacterDirection();
        }

        RaycastHit hit;

        if(!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            animator.SetTrigger("isFalling");
        }
        else
        {
            animator.SetTrigger("notFallingAnymore");
        }

        //Check if falling to end game
        if(transform.position.y < -5)
        {
            gameManager.EndGame();
        }
    }

    private void SwitchCharacterDirection()
    {
        if(!gameManager.gameStarted)
        {
            return;
        }

        isWakingRight = !isWakingRight;

        if(isWakingRight)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {
            gameManager.IncreaseScore();

            GameObject gameObject = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(gameObject, 2);
            Destroy(other.gameObject);
        }
    }

}
