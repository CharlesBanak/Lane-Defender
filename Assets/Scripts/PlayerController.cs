using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;
    private InputAction shoot;

    public GameObject bulletPrefab;
    public AudioClip shootClip;

    private bool isTankMoving;
    public GameObject tank;
    private float moveDirection;

    public TMP_Text scoretext;
    private int score;
    public TMP_Text livestext;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("Move");
        move.started += Move_started;
        move.canceled += Move_canceled;

        restart = playerInput.currentActionMap.FindAction("Restart");
        restart.started += Restart_started;

        quit = playerInput.currentActionMap.FindAction("Quit");
        quit.started += Quit_started;

        shoot = playerInput.currentActionMap.FindAction("Shoot");
        shoot.started += Shoot_started;

        score = 0;
        lives = 3;
        scoretext.gameObject.SetActive(true);
        livestext.gameObject.SetActive(true);

        scoretext.text = "Score: " + score.ToString();
        livestext.text = "Lives: " + lives.ToString();
    }

    private void Shoot_started(InputAction.CallbackContext obj)
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(shootClip, transform.position);
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isTankMoving = false;
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        isTankMoving = true;
    }

    private void FixedUpdate()
    {
        if (isTankMoving)
        {
            //Move the paddle
            tank.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6 * moveDirection);
        }
        else
        {
            //stop the paddle
            tank.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTankMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }
}
