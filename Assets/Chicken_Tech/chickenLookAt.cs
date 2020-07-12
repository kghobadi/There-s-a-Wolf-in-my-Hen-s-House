using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class chickenLookAt : MonoBehaviour
{
    private Material chickenController;
    public GameObject wolf;
    //public LineRenderer line;
    private float target = 0;

    // Start is called before the first frame update
    void Start()
    {
        chickenController = GetComponent<Renderer>().material;
        target = 0;
        /*//if x < 0 add 180 to the angle
 else if y < 0 add 360 to the angle.
*/        //line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //cap rotation at -2, 2
        //float rot = transform.localEulerAngles.y/180;
        Vector2 wolfPos = new Vector2(wolf.transform.position.x, wolf.transform.position.z);
        Vector2 chickPos = new Vector2(transform.position.x, transform.position.z);

        float hyp = Vector3.Distance(wolf.transform.position, transform.position);
        float distZ = chickPos.y - wolfPos.y;
        float distX = chickPos.x - wolfPos.x;
        float distY = transform.position.y - wolf.transform.position.y;

        float tan = distZ/distX;

        float theta = Mathf.Atan(tan) * Mathf.Rad2Deg;

        if(distX < 0)
        {
            theta += 180;
        }else if(distZ < 0)
        {
            theta += 360;
        }


        theta -= 270-transform.eulerAngles.y;

        //Debug.Log(theta);


        if (theta > 90 || theta < -90)
        {
            target = Mathf.Lerp(target, 0, Time.deltaTime);
        }
        else
        {
            target = Mathf.Lerp(target, Mathf.Asin(distY / hyp) * Mathf.Rad2Deg, 3f*Time.deltaTime);
        }

        Debug.Log(target);
        float upAngle = target;


        //theta -= transform.eulerAngles.y;
        //theta = Mathf.Clamp(theta, -90, 90);



        //conditional needs to look at forward direction of model
        /*if(distX >= 0 && transform.forward.z > 0)
         {
             theta *= -1;
         }

         if(distX <= 0 && transform.forward.z < 0)
         {
             theta *= -1;
         }

         if(distX <= 0 && transform.forward.z < 1 && transform.forward.z > -1)
         {
             theta += 180f*Mathf.Deg2Rad;
         }*/
        //theta += Mathf.Deg2Rad * transform.eulerAngles.y;


        // Debug.Log(theta);
        chickenController.SetVector("_Rotation", new Vector4(upAngle * Mathf.Deg2Rad, -theta*Mathf.Deg2Rad, 0, 0));
        //send position of wolf
        //chickenController.SetVector("_WorldPoint", wolf.transform.position);
        //send position of chicken
        //chickenController.SetVector("_ChickenPosition", transform.position);
        //send rotation of chicken
        //chickenController.SetFloat("_ChickenRotation", transform.localEulerAngles.y);
       // line.SetPosition(0, transform.position);
        //.SetPosition(1, wolf.transform.position);
    }
}
