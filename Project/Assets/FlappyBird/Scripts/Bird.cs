using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;


// public class Bird : MonoBehaviour
// {
    public class Bird : Agent
    {
        [Header("References")]
        [SerializeField] private Rigidbody rb = null;
        [SerializeField] private PipeHandler pipeHandler = null;
        [SerializeField] private Transform bodyTransform = null;

        [Header("Settings")]
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float maxVelocityMagnitude = 5f;

        private Vector3 startingPos;

        public override void Initialize()
        {
            startingPos = transform.position;
        }  

        public override void OnEpisodeBegin()
        {
            transform.position = startingPos;
            rb.velocity = Vector3.zero;

            pipeHandler.ResetPipes();
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            AddReward(0.1f);

            if (Mathf.FloorToInt(actions.DiscreteActions[0]) !=1f) 
            {
                return;
            }

            Jump();
        }

        private void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var discreteActionsOut = actionsOut.DiscreteActions;

            if (!Input.GetKeyDown(KeyCode.Space)) 
            {
                Jump();
            }

           
        }

        private void OnTriggerEnter(Collider other) 
        {
            AddReward(-1.0f);
            EndEpisode();
        }

        private void Update()
        {
            bodyTransform.rotation = Quaternion.LookRotation(rb.velocity + new Vector3(10f, 0f, 0f), transform.up);

                 if (Input.GetKeyDown(KeyCode.Space)) 
                 {
                     Jump();
                 }
           
        

        
     
        }
    } 
// }   
   
