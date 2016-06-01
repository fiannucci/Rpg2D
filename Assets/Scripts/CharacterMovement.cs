using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    private Animator anim;
    private GameObject playerSprite;
    private bool facingRight;
    public float speed = 4.0f;
	
	void Awake ()
    {
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        playerSprite = transform.Find("PlayerSprite").gameObject;
        anim = playerSprite.GetComponent<Animator>();
	}	
	
	void Update ()
    {
        float movePlayerVector = Input.GetAxis("Horizontal");
        playerRigidBody2D.velocity = new Vector2(movePlayerVector * speed, playerRigidBody2D.velocity.y);

        if ((movePlayerVector > 0 && !facingRight) || (movePlayerVector < 0 && facingRight)) 
            Flip();

        anim.SetFloat("speed", Mathf.Abs(movePlayerVector));
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 TheScale = playerSprite.transform.localScale;
        TheScale.x *= -1;
        playerSprite.transform.localScale = TheScale; 
    }
}
