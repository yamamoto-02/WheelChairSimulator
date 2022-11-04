using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove;
using Fove.Unity;
using System.IO;
using System.Text;

public class PlayerController : MonoBehaviour
{
    public FoveInterface hmd;
    private StreamWriter sw;
    //[SerializeField] float move_power = 200f;
    //Rigidbody rig = null;
    public float amountOfRotation;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //if (rig == null) rig = GetComponent<Rigidbody>();
        sw = new StreamWriter(@"result.csv",true,Encoding.GetEncoding("Shift_JIS"));
        string[] s1 ={"time","x","y","key_input"};
        string s2 = string.Join(",",s1);
        sw.WriteLine(s2);
    }

    void ResultData(string t1,string t2,string t3,string t4)
    {
        string[] s1 = {t1,t2,t3,t4};
        string s2 = string.Join(",",s1);
        sw.WriteLine(s2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GetCombinedGazeScreenPosition();
        
    }

    public int GetKeyInput()
    {
        if (Input.GetKey (KeyCode.Space))
        {
            var num = 1;
            return num;
        }
        else
        {
            var num = 0;
            return num;
        }
    }

    public Vector2 GetCombinedGazeScreenPosition()
    {
        //Get combined gaze
        Vector3 ray = hmd.GetCombinedGazeRay().value.direction;

        //Get projection matrix
        Matrix44 l = FoveManager.Headset.GetProjectionMatricesLH(0.001f,1000.0f).value.left;

        //Project
        float projX = l.m00 * ray.x + l.m01 * ray.y + l.m02 * ray.z + l.m03;
        float projY = l.m10 * ray.x + l.m11 * ray.y + l.m12 * ray.z + l.m13;
        float projW = l.m30 * ray.x + l.m31 * ray.y + l.m32 * ray.z + l.m33;

        var outPos = new Vector2(projX /projW, projY / projW);
        var ray_x = projX / projW;
        var ray_y = projY / projW;
        var elapsed_time = Time.time;
        var get_key_input = GetKeyInput();
        Debug.Log("ScreenVector2: " + outPos.ToString() + ",GazeVector3: " + ray.ToString() + "ray_x:" + ray_x.ToString() + "time:" + elapsed_time.ToString());

        if(ray_x<0.1 && -0.1<ray_x)
        //if(Input.GetKey(KeyCode.W) == true)
        {
            //rig.AddForce(transform.forward * move_power,ForceMode.Force);
            transform.position += transform.forward * movementSpeed;
        }

        ResultData(elapsed_time.ToString(),ray_x.ToString(),ray_y.ToString(),get_key_input.ToString());
        return outPos;
    }
}