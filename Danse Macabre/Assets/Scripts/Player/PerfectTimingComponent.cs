// ASSIGN TO THE PLAYER'S OBJECT
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PerfectTimingComponent : MonoBehaviour
{
    #region parameters
    private float perfectRadius = 0.2f;
    private float goodRadius = 0.35f;
    private float badRadius = 0.5f;
    #endregion

    #region references
    private Transform _myTransform;
    [SerializeField]
    private LayerMask arrowLayer;
    [SerializeField]
    private ParticleSystem TimingParticleSystem;
    private ParticleSystem.MainModule TimingParticleMain;
    private ParticleSystem.EmissionModule TimingParticleEmitter;
    private ParticleSystem.ColorOverLifetimeModule TimingParticleColorOverLifetime;

    [SerializeField]
    private float PerfectSize = 2.0f;
    [SerializeField]
    private float GreatSize = 1.5f;
    [SerializeField]
    private float GoodSize = 1.0f;
    [SerializeField]
    private float WrongSize = 0.5f;
    [SerializeField]
    private Color PerfectColor = new Color32(255,0,255,255);
    [SerializeField]
    private Color GreatColor = new Color(255,255,0,255);
    [SerializeField]
    private Color GoodColor = new Color(0,255,0,255);
    [SerializeField]
    private Color WrongColor = new Color(255,255,255,255);
    [SerializeField]
    private float StartAlpha = 0.3f; // From 0 to 1, how visible the particle starts as
    #endregion

    #region properties
    private string targetTag;
    #endregion

    private void Start()
    {
        _myTransform = transform;

        TimingParticleMain = TimingParticleSystem.main;
        TimingParticleEmitter = TimingParticleSystem.emission;
        TimingParticleColorOverLifetime = TimingParticleSystem.colorOverLifetime;
        TimingParticleSystem.Play();
        TimingParticleEmitter.enabled = true;
        TimingParticleMain.loop = false;
    }

    #region methods
    /// <summary>
    /// Called everytime there's an action.
    /// This method checks the distance between the player transform.position and the arrow's transform.position to calculate the timing of the action.
    /// </summary>
    public void CheckNearbyArrow(ActionComponent.Action action)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(_myTransform.position, badRadius, arrowLayer); // "Draws" a circle around the player to check if any object in the arrowLayer is within range.
        
        if (hitCollider != null) // This is true if the player is within the bad radius range of an arrow.
        {
            ArrowComponent arrowComponent = hitCollider.gameObject.GetComponent<ArrowComponent>(); // Reference for the arrow script.

            if (!arrowComponent.IsDone()) // If no action is registered in this arrow.
            {
                arrowComponent.ActionDone(); // Set this arrow as done so there's no double input.

                // Maps the player's action to a tag
                if (action == ActionComponent.Action.Stomping) targetTag = "Stomp";   
                else if (action == ActionComponent.Action.Jumping) targetTag = "Jump";
                else if (action == ActionComponent.Action.Dashing || action == ActionComponent.Action.Sliding) targetTag = "DashSlide";

                float distance = Vector2.Distance(_myTransform.position, hitCollider.transform.position); // Calculate the distance between player and arrow.

                if (hitCollider.CompareTag(targetTag)) // Is true if the mapped tag (action) above is the same as the within range arrow.
                {
                
                    if (distance <= perfectRadius)
                    {
                        GameManager.Instance.ArrowTiming("PERFECT");
                        EmitTimingParticle("PERFECT");
                    }
                    else if (distance <= goodRadius)
                    {
                        GameManager.Instance.ArrowTiming("GREAT");
                        EmitTimingParticle("GREAT");
                    }
                    else
                    {
                        GameManager.Instance.ArrowTiming("GOOD");
                        EmitTimingParticle("GOOD");
                    }
                }
                else // If the tag mapped (action) doesn't match the arrow's tag.
                { 
                    GameManager.Instance.ArrowTiming("WRONG"); // if the movement is not correct
                    EmitTimingParticle("WRONG");
                }
            }
        }    
    }

    private void EmitTimingParticle(string action)
    {
        var gradient = new Gradient(); // Creating the gradient for colour over lifetime
        var colors = new GradientColorKey[2]; // Gradient is defined by two colours
        var alphas = new GradientAlphaKey[2]; // Gradient is defined by two alphas
        /*
        if (action == "PERFECT")
        {
            TimingParticleMain.startSize = PerfectSize;
            colors[0] = new GradientColorKey(PerfectColor, 0);
            colors[1] = new GradientColorKey(PerfectColor, 1);
        }
        else if (action == "GREAT")
        {
            TimingParticleMain.startSize = GreatSize;
            colors[0] = new GradientColorKey(GreatColor, 0);
            colors[1] = new GradientColorKey(GreatColor, 1);
        }
        else if (action == "GOOD")
        {
            TimingParticleMain.startSize = GoodSize;
            colors[0] = new GradientColorKey(GoodColor, 0);
            colors[1] = new GradientColorKey(GoodColor, 1);
        }
        else if (action == "WRONG")
        {
            TimingParticleMain.startSize = WrongSize;
            colors[0] = new GradientColorKey(WrongColor, 0);
            colors[1] = new GradientColorKey(WrongColor, 1);
        }
        alphas[0] = new GradientAlphaKey(StartAlpha, 0);
        alphas[0] = new GradientAlphaKey(0, 1);
        gradient.SetKeys(colors, alphas);
        TimingParticleColorOverLifetime.color = gradient;
        */
        TimingParticleSystem.Emit(1);
    }

    public ActionComponent.Action ArrowActionForBot()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(_myTransform.position, badRadius, arrowLayer); // "Draws" a circle around the player to check if any object in the arrowLayer is within range.
        
        if (hitCollider != null) // This is true if the player is within the bad radius range of an arrow.
        {
            string arrowTag = hitCollider.tag;
            //print(arrowTag);
            ActionComponent.Action action;
            if (arrowTag == "Stomp") action = ActionComponent.Action.Stomping;   
            else if (arrowTag == "Jump") action = ActionComponent.Action.Jumping;
            else if (arrowTag == "DashSlide") action = ActionComponent.Action.Sliding;
            else action = ActionComponent.Action.Running;

            float distance = Vector2.Distance(_myTransform.position, hitCollider.transform.position); // Calculate the distance between player and arrow.
            
            if (distance <= perfectRadius)
            {
                return action;
            }
            else return ActionComponent.Action.Null;
        }
        else return ActionComponent.Action.Null;
    }
    #endregion
}