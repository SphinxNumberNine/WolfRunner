using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour {

    private float speed = 0.04f;
    public float temp;
    private Rigidbody rb;
    private int nextX = 55; //x value of next spawned prefab
    Animator anim;
    bool moveLeft = false;
    bool moveRight = false;
    int moveCounter = 25;
    bool isFalling = false;
    public int currentTile = 0;
    public int temp1 = 0;
    int tilesAdded = 0;
    public int coinCount = 0;
    public int score = 0;

    public GameObject oneCenterRock;
    public GameObject oneJumpRock;
    public GameObject twoJumpRocks;
    public GameObject twoRocks;
    public GameObject coin;

    List<GameObject> tiles;
    Queue<GameObject> activeTiles;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        tiles = new List<GameObject>
        {
            oneCenterRock,
            oneJumpRock,
            twoJumpRocks,
            twoRocks
        };
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentTile = (int) transform.position.x / 10;
        if(temp1 != currentTile)
        {
            temp1 = currentTile;
            makeTile();
        }
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.z < 0.5)
        {
            //rb.AddForce(new Vector3(0, 0, 5), ForceMode.VelocityChange);
            //Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            //transform.position = newPos;
            moveLeft = true;
            moveRight = false;
            moveCounter = 25;
        }
        else if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.z > -0.5)
        {
            //Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            //transform.position = newPos;
            moveRight = true;
            moveLeft = false;
            moveCounter = 25;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isFalling == false)
        {
            rb.AddForce(0, 4, 0, ForceMode.VelocityChange);
            isFalling = true;
        }
        else if(Input.GetKeyDown(KeyCode.S) && isFalling)
        {
            Vector3 newVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity = newVelocity;
            Vector3 newPos = new Vector3(transform.position.x, 0, transform.position.z);
            transform.position = newPos;
            //isFalling = true;
        }


        if(moveRight && moveCounter > 0)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.04f);
            transform.position = newPos;
            moveCounter--;
        }
        else if(moveLeft && moveCounter > 0)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.04f);
            transform.position = newPos;
            moveCounter--;
        }
        else
        {
            moveRight = false;
            moveLeft = false;
            moveCounter = 0;
        }
        anim.SetFloat("speed", speed);
        temp = Input.mousePosition.z;
        Vector3 newPosition = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        transform.position = newPosition;

        if(this.transform.rotation.x > 0 || this.transform.rotation.z > 0)
        {
            Quaternion quaternion = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
            transform.rotation = quaternion;
        }
        
	}

    private void OnCollisionEnter(Collision collision)
    {    
        if(collision.collider.CompareTag("Finish"))
        {
            Debug.Log("Collision");
            this.speed = 0f;
        }
        else
        {
            isFalling = false;
        }
    }

    public void makeTile()
    {
        System.Random random = new System.Random();
        int randInt = random.Next(0, tiles.Count);
        int randInt2 = random.Next(0, 4);
        GameObject obj = tiles[randInt];
        obj.transform.position = new Vector3(nextX, 0, 0);
        obj.transform.rotation = new Quaternion(0, 0, 0, 0);
        Instantiate(obj);
        /*activeTiles.Enqueue(obj);
        if(activeTiles.Count > 10)
        {
            Destroy(activeTiles.Dequeue());
        }*/
        if(randInt2 == 0 && randInt != 1)
        {
            Instantiate(coin, new Vector3(nextX, coin.transform.position.y, 1), new Quaternion(0, 0, 0, 0));
        }
        nextX += 10;
        tilesAdded++;
        if (tilesAdded == 10)
        {
            tilesAdded = 0;
            speed += 0.01f;
        }

        
    }

    public void OnTriggerEnter(Collider other)
    {
        /*if(other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinCount++;
        }(*/
    }
}
