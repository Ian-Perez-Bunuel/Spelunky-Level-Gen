using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPosition;
    public GameObject[] rooms;  // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRBT
    public GameObject character;

    private int direction;
    public float moveAmount;

    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration;

    //will only detect rooms and not other objects
    public LayerMask room;

    private int downCounter;

    private float timeBetweenRoom;
    public float startTimeBetweenRoom = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPosition.Length);
        transform.position = startingPosition[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        //spawn player
        character.transform.position = transform.position;

        //random of 1-5 rooms
        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBetweenRoom <= 0 && stopGeneration == false) 
        {
            Move();
            timeBetweenRoom = startTimeBetweenRoom;
        }
        else
        {
            timeBetweenRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) //move right
        {
            if (transform.position.x < maxX)
            {
                //reset down counter
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                //spawn any room since all rooms have a right opening
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                //so levels dont spawn on eachother
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //move left
        {
            if (transform.position.x > minX)
            {
                //reset down counter
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                //spawn any room since all rooms have a left opening
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                //so levels dont spawn on eachother
                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5)
        {
            downCounter++;

            if (transform.position.y > minY)
            {
                //make invisible circle that will check the room type so that the room above has a bottom exit
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                //destroy room if no bottom exit
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        //destroy
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        //make room of type 3
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        //destroy
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        //make veriable that will only take rooms with bottom exit
                        int randBottomRoom = Random.Range(1, 3);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }

                        //replace with one that has exit
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                //Make sure the room you are going to has a top opening
                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);



                //allow level to spawn anywhere again incase they tried to double up on the last layer
                direction = Random.Range(1, 6);
            }
            else
            {
                //Stop level generation
                stopGeneration = true;
            }
        }
    }
}
