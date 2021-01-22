using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    public int bodyIndex;
    public float thickness;
    
    //Height to Angle
    [Range(-5.0f, 5.0f)]
    public float minHeight;

    [Range(-5.0f, 5.0f)]
    public float maxHeight;

    [Range(-90.0f, 90.0f)]
    public float minRotationAngle;

    [Range(-90.0f, 90.0f)]
    public float maxRotationAngle;

   

    private KinectManager kinect;
    // Start is called before the first frame update
    void Start()
    {
        kinect = KinectManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
    
        if (KinectManager.IsKinectInitialized() & kinect.IsUserDetected(bodyIndex)) //todo:adapt!
        {
            gameObject.transform.localScale =new Vector3(gameObject.transform.localScale.x,thickness, gameObject.transform.localScale.z);
            RotateByHeight();
        }
        else
        {
            gameObject.transform.localScale =new Vector3(gameObject.transform.localScale.x,0.0f, gameObject.transform.localScale.z);
            
        }
    }

    void RotateByHeight()
    {
        Debug.Log(kinect.GetJointPosition(kinect.GetUserIdByIndex(bodyIndex), 3));
        var jointPosition = kinect.GetJointPosition(kinect.GetUserIdByIndex(bodyIndex), 3); //Head is 3, see KinectInterop.cs
       var rotation = Helper.Map(jointPosition.y, minHeight, maxHeight, minRotationAngle, maxRotationAngle); //Head is 3, see KinectInterop.cs
       gameObject.transform.eulerAngles=new Vector3(0.0f,0.0f,rotation);
    }
}
