using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;


public class PlayerMove : MonoBehaviour
{
    float vertical;
    float horizontal;
    [SerializeField]
    float velocidadeCorrendo;
    [SerializeField]
    float velocidadeDeMovimento;
    Rigidbody rb;
    [SerializeField]
    float forcaPulo;
    private Animator animator;

    bool pulo = false;
    public bool verificacao = true;

    bool podeandar = true;

   

    public Transform origin;
    Vector3 directionFw;
    Vector3 directionDw;
    public float radius;
    public float distance;
    public float distanceAlture;
    public LayerMask layerMask;
    private float hitDistance;
    private Vector3 hitDirection;
    private Vector3 hitPoint;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();    
    }
    private void Update()
    {
        if(podeandar)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        // adiciona os valores hash no animator
        int Anim_HashX = Animator.StringToHash("MoveX");
        int Anim_HashZ = Animator.StringToHash("MoveZ");

        // transforma os imputs h e v em valores hash.
        animator.SetFloat(Anim_HashX, horizontal);
        animator.SetFloat(Anim_HashZ, vertical);
    }

    public void FixedUpdate()
    {
        // adiciona um valor padrao de 0 para todos os eixos
        Vector3 velocidade = Vector3.zero;

        // adiciona os valores de input se a velociade atual for difente de 0 nos eixos x e y
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 direcao = (transform.forward * vertical + transform.right * horizontal).normalized;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                velocidade = direcao * velocidadeCorrendo;
                animator.SetBool("Correndo", true);


            }
            else
            {
                velocidade = direcao * velocidadeDeMovimento;
                animator.SetBool("Correndo", false);
            }

            if (Input.GetKey(KeyCode.Space) && vertical  == 1)
            {
                animator.SetBool("Deslizando", true);
            }
            else
            {
                  animator.SetBool("Deslizando",false);
            }
           
            
           
        }

        // impede que a gravidade receba o valor de vector3.zero, definindo a propria gravidade em y
        velocidade.y = rb.velocity.y;
        rb.velocity = velocidade;

        directionDw = transform.up;
        directionFw = origin.transform.forward;

        RaycastHit hitAlture;
        
        if (Physics.SphereCast(origin.position, radius, - directionDw, out hitAlture, distanceAlture))
        {
            hitDistance = hitAlture.distance;
            hitPoint = hitAlture.point;
        }
           
    }
    public void Fim()
    {
        //rb.AddForce(Vector3.forward * 100, ForceMode.Impulse);
        animator.SetBool("Pulo", false);
        rb.useGravity = true;
        podeandar = true;
        
    }

    private void OnDrawGizmosSelected()
    {

        Debug.DrawLine(origin.position, hitPoint);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(hitPoint, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origin.position,0.2f);
    }


}

