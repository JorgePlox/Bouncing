using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float movementForce = 1.5f;
    public float maxSpeed = 4f;
    private Vector2 pauseVelocity = new Vector2 (0f, 0f);
    private float pauseAngularVelocity = 0f;
    private PhysicsMaterial2D ballMaterial;


    private Rigidbody2D cuerpoPlayer;

    public Animator animator;

    public static PlayerController sharedInstance;

    private Vector3 startPosition;

    //Audio
    public AudioClip sonidoSalto;
    public AudioClip deathSound;
    private AudioSource audioSource;


    //Level Mode

    private int startingLives = 3;
    private int startingCoins = 0;



    private void Awake()
    {
        sharedInstance = this;
        cuerpoPlayer = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        ballMaterial = cuerpoPlayer.sharedMaterial;

        audioSource = GetComponent<AudioSource>();

    }


    private void Start()
    {
        StartGame();
    }

    // Start is called before the first frame update
    public void StartGame()
    {

        cuerpoPlayer.sharedMaterial = ballMaterial;
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isWalking", false);
        pauseAngularVelocity = 0f;
        pauseVelocity = new Vector2(0f, 0f);
        ResumeVelocity();
        this.transform.position = startPosition;


    }

    // Update is called once per frame
    void Update()
    {
        //Solo vamos a dejar que salte si el juego está en ingame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        { 
            //Verificar si se presiona "espacio" para saltar
            if (Input.GetButtonDown("Jump") || VirtualJoystick.sharedInstance.moveJump)
            {
                Jump();
                VirtualJoystick.sharedInstance.moveJump = false;

            }

            //Se le asigna a la animación, en isGrounded, el valor de IsTouchingTheGround
            animator.SetBool("isGrounded", IsTouchingTheGround());
            animator.SetFloat("ballSpeed", cuerpoPlayer.velocity.x);
            animator.SetBool("isWalking", IsMoving());

        }
    }

    //Fixed Update para el movimiento
    private void FixedUpdate()
    {
        //Solo vamos a dejar que se mueva si el juego está en ingame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //Con Botones para android
        #if UNITY_ANDROID
        if (VirtualJoystick.sharedInstance.moveRight)
            {
                if (cuerpoPlayer.velocity.x >= maxSpeed) { }

                else
                    Move("right");
            }

        else if (VirtualJoystick.sharedInstance.moveLeft)
            {
                if (cuerpoPlayer.velocity.x <= -maxSpeed) { }

                else
                    Move("left");
            }


#else
            {
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    if (cuerpoPlayer.velocity.x >= maxSpeed) { }

                    else
                        Move("right");
                }

                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    if (cuerpoPlayer.velocity.x <= -maxSpeed) { }

                    else
                        Move("left");
                }
            }
#endif
        }

    }



    //Salto
    void Jump()
    {
        // F = m*a      ------>    a=F/m
        if (IsTouchingTheGround())
        {
            cuerpoPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (sonidoSalto != null)
            {
                audioSource.PlayOneShot(sonidoSalto);
            }


        }
    }

    //Genera el movimiento del personaje dependiendo de la dirección que se le indique
    void Move(string direction)
    {
        if (direction == "right")
        {
            cuerpoPlayer.AddForce(Vector2.right * movementForce, ForceMode2D.Force);
        }

        if (direction == "left")
        {
            cuerpoPlayer.AddForce(Vector2.left * movementForce, ForceMode2D.Force);
        }
    }

    //variable para detectar la capa del suelo
    public LayerMask groundLayer;
    //Devuelve true si es que está tocando el suelo
    bool IsTouchingTheGround()
    {   //traza un rayo hacia abajo para ver si toca el suelo
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.7f, groundLayer))
            return true;
        else
            return false;
    }

    //Verifica que el personaje se encuentre en movimiento
    bool IsMoving()
    {
        if (cuerpoPlayer.velocity.x >= 0.5f || cuerpoPlayer.velocity.x <= -0.5f)
            return true;

        else
            return false;
    }

    //Matar al jugador
    public void Kill()
    {

        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        //Dependiendo del tipo de juego 
        switch (LevelManager.sharedInstance.currentGameMode)
        {
            case GameMode.levels:
                //perder una vida
                LossLife();
                break;

            case GameMode.infinite:
                //guardar mejor tiempo
                if (GameManager.sharedInstance.timeElapsed > PlayerPrefs.GetFloat("BestTimeInfinite", 0))
                { 
                    PlayerPrefs.SetFloat("BestTimeInfinite", GameManager.sharedInstance.timeElapsed);
                }
                break;
        }

        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);

    }

    //Consigue la velocidad y luego pausa el movimiento
    public void PauseVelocity()
    {
        pauseVelocity = cuerpoPlayer.velocity;
        pauseAngularVelocity = cuerpoPlayer.angularVelocity;

        cuerpoPlayer.velocity = new Vector3(0f, 0f, 0f);
        cuerpoPlayer.angularVelocity = 0f;

        cuerpoPlayer.isKinematic = true;

    }

    //Ocupa la velocidad gardada en PauseVelocity para integrarsela nuevamente al player y permite que el objeto se mueva
    public void ResumeVelocity()
    {

        cuerpoPlayer.isKinematic = false;
        cuerpoPlayer.velocity = pauseVelocity;
        cuerpoPlayer.angularVelocity = pauseAngularVelocity;


    }

    //elimina la velocidad en x en la muerte
    public void DeathVelocity()
    {

        cuerpoPlayer.sharedMaterial = null;
        cuerpoPlayer.velocity = new Vector2 (0f, cuerpoPlayer.velocity.y);
        cuerpoPlayer.angularVelocity = 0f;


    }

    //Actualiza la cantidad de monedas del jugador
    public void addCoins(int newCoins)
    {

        int actualCoins = PlayerPrefs.GetInt("PlayerCoins", startingCoins);
        int totalCoins = newCoins + actualCoins;

        if (totalCoins >= 100)
        {
            totalCoins -= 100;
            GetLife(1);
        }

        PlayerPrefs.SetInt("PlayerCoins",totalCoins);
    }
    //Agrega vidas al jugador
    public void GetLife(int newLives)
    {
        int actualLives = PlayerPrefs.GetInt("PlayerLives", startingLives);
        int totalLives = actualLives + newLives;
        PlayerPrefs.SetInt("PlayerLives", totalLives);
    }

    //Hace perder una vida al jugador
    public void LossLife()
    {
        int actualLives = PlayerPrefs.GetInt("PlayerLives", startingLives);
        int totalLives = actualLives -  1;

        if (totalLives <= 0)
        {
            totalLives = startingLives;
            LevelManager.sharedInstance.ChangeScene("MainMenu");
            LevelManager.sharedInstance.restartZone();
            PlayerPrefs.SetInt("PlayerCoins", startingCoins);


        }

        PlayerPrefs.SetInt("PlayerLives", totalLives);

        

    }


    public int GetLivesNumber()
    { 
    return PlayerPrefs.GetInt("PlayerLives", startingLives);
    }


    //Falta:
    //No poder jugar con cero vidas
}
