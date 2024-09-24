using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UserInfo
{
    public string name;
    public string phone;
    public int age;
}
public class Contant : MonoBehaviour
{
    public List<UserInfo> allUser = new List<UserInfo>();
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            allUser = CSV.Get().Parse<UserInfo>("UserInfo");
        }
    }
}
