using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //아래 예쁘게 수정 / 추가 가능하면 ㄱㄱ(변수나 함수)
    [SerializeField]private CharacterController controller;

    //<MovementValue>
    //여따가 점프중 이동속도, 웅크리기 이동속도, 달리기 이동속도 같은거 더 넣으면 좋을듯(넣을지 말지는 나중에 정하고)
    [SerializeField]private float jumpHeight = 1f;
    [SerializeField]private float speed = 6f;

    //<GravityValue>
    [SerializeField]private float gravity = -9.81f;
    [SerializeField]private Vector3 groundCheck=new Vector3(0f, -1f, 0f); //오브젝트 있는거 지저분해서 바꿈(TransForm => Vector3)
    [SerializeField]private float groundDistance = 0.4f;
    [SerializeField]private LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        if (controller == null)
            Debug.LogError("PlayerMovement.cs에 있는 controller에 암것도 없음(CustomError)");

        if (groundCheck.y > 0 || groundCheck.x != 0 || groundCheck.z != 0)
            Debug.LogError("PlayerMovemnet.cs에 있는 groundCheck[Vector3]에 이상한 값이 들어있을 수도 있음, 확인하세요(CustomError)");

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheck, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
