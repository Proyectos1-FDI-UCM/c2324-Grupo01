using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    #region references
    private ActionComponent actionComponent;
    private PerfectTimingComponent perfectTimingComponent;
    #endregion

    private void Start()
    {
        actionComponent = GetComponent<ActionComponent>();
        perfectTimingComponent = GetComponent<PerfectTimingComponent>();
    }
    private void Update()
    {
        if (perfectTimingComponent.ArrowActionForBot() != ActionComponent.Action.Null)
        {
            PerformAction(perfectTimingComponent.ArrowActionForBot());
        }
    }

    public void PerformAction(ActionComponent.Action action)
    {
        if (action == ActionComponent.Action.Jumping) actionComponent.Jump();
        else if (action == ActionComponent.Action.Stomping) actionComponent.Stomp();
        else if (action == ActionComponent.Action.Dashing || action == ActionComponent.Action.Sliding) actionComponent.SlideDash();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != null && other.gameObject.CompareTag("BotHelper"))
        {
            if (actionComponent.currentAction == ActionComponent.Action.Dashing){
                actionComponent.BotCanDash(true);
                actionComponent.SlideDash();
            }
            else if (actionComponent.currentAction == ActionComponent.Action.Sliding)
            {
                actionComponent.SlideStop();
            } 
        }
    }
}
