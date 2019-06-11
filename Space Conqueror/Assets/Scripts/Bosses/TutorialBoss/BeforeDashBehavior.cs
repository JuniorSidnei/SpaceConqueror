using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeDashBehavior : StateMachineBehaviour
{
    public float _maxTimer;
    public float _minTimer;
    public int _rand;
    public float _timer;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = Random.Range(_minTimer, _maxTimer);
        Debug.Log("timer: " + _timer);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer -= Time.deltaTime;
       
        if (_timer <= 0)
        {
            _timer = Random.Range(_minTimer, _maxTimer);
            _rand = Random.Range(0, 3);


            if (_rand == 1)
                animator.SetTrigger("GoDash");
            else
                animator.SetTrigger("BeforeDash");
                
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
