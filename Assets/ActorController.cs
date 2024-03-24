using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 1.2f;
    public float runMultiplier = 2.8f;
    public float jumpVelocity = 1.2f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;

    private bool lockPlanar = false;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float targetRunMulti = ((pi.run) ? 2.0f : 1.0f);
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"),targetRunMulti,0.4f));
        if (pi.jump)
        {
            anim.SetTrigger("jump");
        }
   
        if (lockPlanar == false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
        }
        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.4f);
            model.transform.forward = targetForward;

        }
        
    }
    private void FixedUpdate()
    {
        //rigid.position += planarVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    //
    /// <summary>
    /// 
    /// 
    /// </summary>
    public void OnJumpEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        //print("on jump enter");
    }

    public void OnJumpExit()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        //print("on jump exit");
    }
}
