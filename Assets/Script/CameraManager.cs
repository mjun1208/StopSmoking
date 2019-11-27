using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static CameraManager instance;
    public GameObject Target;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            Vector3 Dir = Vector3.Lerp(this.transform.position, new Vector3(Target.transform.position.x, 0, -10), 0.8f);
            if (4.5f > Mathf.Abs(Dir.x))
            transform.position = Dir;
        }
    }
}
