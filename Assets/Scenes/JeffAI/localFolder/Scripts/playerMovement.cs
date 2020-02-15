using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JeffAI{

    public class playerMovement : MonoBehaviour
    {
        public float playerSpeed;
        public float jumpFactor;
        public float gravityFactor;
        public Vector3 gravity;
        private CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            Vector3 move = Vector3.zero;
            if (Input.GetKey("space") && controller.isGrounded)
            {
                gravity -= Physics.gravity * gravityFactor * jumpFactor;
                move += gravity;
                controller.Move(move);
            }

            if (controller.isGrounded == false)
            {
                move = Vector3.zero;
                gravity += Physics.gravity * gravityFactor * Time.deltaTime;
                move += gravity;
                controller.Move(move);
                Debug.Log("not ground");
            }
            else {
                gravity = Vector3.zero;
            }

            if (Input.GetKey("w")) {
                move = new Vector3(playerSpeed * Time.deltaTime, 0, playerSpeed * Time.deltaTime);
                controller.Move(move);
                transform.forward = move;
            }
            if (Input.GetKey("a"))
            {
                move = new Vector3(-playerSpeed * Time.deltaTime, 0, playerSpeed * Time.deltaTime);
                controller.Move(move);
                transform.forward = move;
            }
            if (Input.GetKey("d"))
            {
                move = new Vector3(playerSpeed * Time.deltaTime, 0, -playerSpeed * Time.deltaTime);
                controller.Move(move);
                transform.forward = move;
            }
            if (Input.GetKey("s"))
            {
                move = new Vector3(-playerSpeed * Time.deltaTime, 0, -playerSpeed * Time.deltaTime);
                controller.Move(move);
                transform.forward = move;
            }
                

        }

    }

}