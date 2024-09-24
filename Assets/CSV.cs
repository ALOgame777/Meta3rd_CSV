using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class CSV : MonoBehaviour
{
    static CSV instance;

    public static CSV Get()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();

            go.name = nameof(CSV);

            go.AddComponent<CSV>();

            print("111111");
        }

        return instance;
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

            print("22222");
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public List<UserInfo> Parce(string fileName) // 오버로딩 오버 라이딩 알자 !! <<<<========== [[ 학습 용으로 사용 해 본 것 ]]★★★★★★★★★★★★★★★★
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv"; // fileName 에 해당되는 file 을 읽어 오자.

        print(path);

        string stringData = File.ReadAllText(path);

        print(stringData);

        // 엔터를 기준으로 한줄 한줄 자르자.

        string[] lines = stringData.Split("\n"); // "\n" 을 기준으로 자르자.

        for (int i = 0; i < lines.Length; i++)
        {
            string[] temp = lines[i].Split("\r"); // "\r" 을 기준으로 자르자.

            lines[i] = temp[0];

            print(lines[i]);
        }

        string[] variables = lines[0].Split(","); // , 를 기준으로 라인의 첫 번째 값을 나누자.

        List<UserInfo> list = new List<UserInfo>(); // 전체 UserInfo 를 가지고 있는 List 를 만들자

        for (int i = 1; i < lines.Length; i++) // , 를 기준으로 나머지 값들을 나누자.
        {
            string[] value = lines[i].Split(",");

            UserInfo info = new UserInfo(); // 잘라진 데이터를 UserInfo 에 세팅

            info.name = value[0];

            info.phone = value[1];

            info.age = int.Parse(value[2]);

            list.Add(info); // 리스트에 정보를 추가 한다.
        }

        return list;
    }

    public List<T> Parse<T>(string fileName) where T : new() // 오버로딩 오버 라이딩 알자 !! <<<<========== [[ 실제로 사용 해 볼 것!!!!! ]]★★★★★★★★★★★★★★
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv"; // fileName 에 해당되는 file 을 읽어 오자.

        print(path);

        string stringData = File.ReadAllText(path);

        print(stringData);

        // 엔터를 기준으로 한줄 한줄 자르자.

        string[] lines = stringData.Split("\n"); // "\n" 을 기준으로 자르자.

        for (int i = 0; i < lines.Length; i++)
        {
            string[] temp = lines[i].Split("\r"); // "\r" 을 기준으로 자르자.

            lines[i] = temp[0];

            print(lines[i]);
        }

        string[] variables = lines[0].Split(","); // , 를 기준으로 라인의 첫 번째 값을 나누자.

        List<T> list = new List<T>(); // 전체 T 를 가지고 있는 List 를 만들자

        for (int i = 1; i < lines.Length; i++) // , 를 기준으로 나머지 값들을 나누자.
        {
            string[] value = lines[i].Split(",");

            T info = new T(); // 잘라진 데이터를 담을 T 변수를 만들자.

            for (int j = 0; j < variables.Length; j++)
            {
                FieldInfo fieldInfo = typeof(T).GetField(variables[j]); // T 에 있는 변수들의 정보를 가져오자.

                TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType); // int.Parse, float.Parse 의 기능을 할 수 있는 변수를 가져 오자.

                fieldInfo.SetValue(info, typeConverter.ConvertFrom(value[j])); // value[i] 의 값을 typeConverter 를 이용 해서 변수에 세팅.
            }

            list.Add(info);
        }

        return list;
    }
}
