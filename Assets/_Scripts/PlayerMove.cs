using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    private float velocity = 5f;
    private float force = 225f;
    public static float PosX;
    public static int money = 0;
    public static int id;
    public static string name = "";
    public static string url = "";
    private bool isGround = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private Animator anim;

    
    
    [SerializeField] private Text txt;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
  
        txt.text = money.ToString();
        transform.position = new Vector2(PosX,0);
    }

    
    void Update()
    {
        txt.text = money.ToString();
        Move();
        Jump();
    }

    public void Move(){
        float X = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(X*Time.deltaTime*velocity,0));

        if(X<0){
            sprite.flipX = true;
            anim.SetBool("walk",true);
        }
        else if(X>0){
            sprite.flipX = false;
            anim.SetBool("walk",true);
        }
        else{
            anim.SetBool("walk",false);

        }
    }
    public void Jump(){
        if(Input.GetButtonDown("Jump")&&isGround){
            rigid.AddForce(new Vector2(0,force));
            anim.SetTrigger("jump");
        }
    }

    public void SetCoinMoney(int newValue,float newValue2,string newValue3,string newValue4,int newValue5){
        money = newValue;  
        PosX = newValue2;
        name = newValue3;
        url = newValue4;
        id = newValue5;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Dolar")
        {
            Destroy(collision.gameObject);
            money+=10;
            
        }
    }
}
