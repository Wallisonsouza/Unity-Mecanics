using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Teste : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider capsule;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    Transform ossom;
    [SerializeField]
    Transform teste;
    // Start is called before the first frame update
    void Start()
    {
        teste.GetComponent<Transform>();
        capsule = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
