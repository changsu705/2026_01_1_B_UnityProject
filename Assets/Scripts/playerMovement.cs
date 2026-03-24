using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Player Movement Velocity")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Player Input Componante")]
    public Rigidbody rb;

    [Header("Ckeck Velocity")]
    public bool isGrounded;
    public int CoinCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //속도 값으로 직접 이동
        rb.velocity = new Vector3(moveHorizontal * speed,rb.velocity.y, moveVertical * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CoinCount++;
            Destroy(other.gameObject);
        }
    }



}
