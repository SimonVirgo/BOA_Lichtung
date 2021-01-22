using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class controlSidesByHeadMovement : MonoBehaviour

{
    public float minPositionX;
    public float maxpositionX;
    
    private float leftlimit = -15.5f; //xposition of most left column in local game space
    private float rightlimit =15.5f ; //xposition of most right column in local game space
    
    public int bodyIndex; 
    public float movementThreshold;
    public int nFrames; //number of fixed frames over which a position is tracked

    public List<GameObject> list1;
    public List<GameObject> list2;
    public List<GameObject> list3;
    public List<GameObject> list4;

    private Vector3[] positions;

    private KinectManager kinect;
 
    void Start()
    {
        kinect = KinectManager.Instance;
        initializePositionArray();
    }


    void FixedUpdate()
    {
        if (KinectManager.IsKinectInitialized() & kinect.IsUserDetected(bodyIndex))
        {
            UpdatePositions(kinect.GetJointPosition(kinect.GetUserIdByIndex(bodyIndex), 3));
            var distance = positions[0].x - positions[nFrames-1].x; // distance in X between nFrames
     
            if (0 > distance & distance < -1 * movementThreshold & positions[0].x != 0.0f)
            {
                MoveLeft();
            } 
            else if (0 < distance & distance > movementThreshold & positions[0].x != 0.0f)
            {
                MoveRight();
            }
            else
            {
                NoMovement();
            }
        }
        
    }

    void MoveLeft()
    {
      // activateColumnsList(list1);
       deactivateColumnsList(list2);
      activateFiltered(list1, kinect.GetJointPosition(kinect.GetUserIdByIndex(bodyIndex), 3), false);
    }

    void MoveRight()
    {
       // activateColumnsList(list2);
        deactivateColumnsList(list1);
       activateFiltered(list2, kinect.GetJointPosition(kinect.GetUserIdByIndex(bodyIndex), 3), true);
    }

    void NoMovement()
    {
        deactivateColumnsList(list1);
        deactivateColumnsList(list2);
    }

    void initializePositionArray()
    {
        var arr = new Vector3[nFrames];
        for (int i = 0; i < nFrames; i++)
        {
            arr[i]=new Vector3(0f,0f,0f );
        }

        positions = arr;
    }
    
    void UpdatePositions(Vector3 newPosition)
    {
        var arr = new Vector3[nFrames];
        for (int i = 0; i < nFrames-1; i++)
        {
            arr[i]= positions[i+1];
        }

        arr[nFrames-1] = newPosition;
        positions = arr;
    }

    void activateFiltered(List<GameObject> side, Vector3 position, bool invertDirection = false)
    {
        foreach (GameObject go in side)
        {
            var mappedPos = Helper.Map(position.x, minPositionX, maxpositionX, leftlimit, rightlimit);
            
            if (mappedPos > go.transform.localPosition.x)
            {
                go.SetActive(!invertDirection);
            }
            else
            {
                go.SetActive(invertDirection);
            }
        }
    } 
    void activateColumnsList(List<GameObject> side)
    {
        foreach (GameObject go in side)
        {
            go.SetActive(true);
        }
    }

    void deactivateColumnsList(List<GameObject> side)
    {
        foreach (GameObject go in side)
        {
            go.SetActive(false);
        }
    }

}
