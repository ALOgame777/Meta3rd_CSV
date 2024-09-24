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

    public List<UserInfo> Parce(string fileName) // �����ε� ���� ���̵� ���� !! <<<<========== [[ �н� ������ ��� �� �� �� ]]�ڡڡڡڡڡڡڡڡڡڡڡڡڡڡڡ�
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv"; // fileName �� �ش�Ǵ� file �� �о� ����.

        print(path);

        string stringData = File.ReadAllText(path);

        print(stringData);

        // ���͸� �������� ���� ���� �ڸ���.

        string[] lines = stringData.Split("\n"); // "\n" �� �������� �ڸ���.

        for (int i = 0; i < lines.Length; i++)
        {
            string[] temp = lines[i].Split("\r"); // "\r" �� �������� �ڸ���.

            lines[i] = temp[0];

            print(lines[i]);
        }

        string[] variables = lines[0].Split(","); // , �� �������� ������ ù ��° ���� ������.

        List<UserInfo> list = new List<UserInfo>(); // ��ü UserInfo �� ������ �ִ� List �� ������

        for (int i = 1; i < lines.Length; i++) // , �� �������� ������ ������ ������.
        {
            string[] value = lines[i].Split(",");

            UserInfo info = new UserInfo(); // �߶��� �����͸� UserInfo �� ����

            info.name = value[0];

            info.phone = value[1];

            info.age = int.Parse(value[2]);

            list.Add(info); // ����Ʈ�� ������ �߰� �Ѵ�.
        }

        return list;
    }

    public List<T> Parse<T>(string fileName) where T : new() // �����ε� ���� ���̵� ���� !! <<<<========== [[ ������ ��� �� �� ��!!!!! ]]�ڡڡڡڡڡڡڡڡڡڡڡڡڡ�
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv"; // fileName �� �ش�Ǵ� file �� �о� ����.

        print(path);

        string stringData = File.ReadAllText(path);

        print(stringData);

        // ���͸� �������� ���� ���� �ڸ���.

        string[] lines = stringData.Split("\n"); // "\n" �� �������� �ڸ���.

        for (int i = 0; i < lines.Length; i++)
        {
            string[] temp = lines[i].Split("\r"); // "\r" �� �������� �ڸ���.

            lines[i] = temp[0];

            print(lines[i]);
        }

        string[] variables = lines[0].Split(","); // , �� �������� ������ ù ��° ���� ������.

        List<T> list = new List<T>(); // ��ü T �� ������ �ִ� List �� ������

        for (int i = 1; i < lines.Length; i++) // , �� �������� ������ ������ ������.
        {
            string[] value = lines[i].Split(",");

            T info = new T(); // �߶��� �����͸� ���� T ������ ������.

            for (int j = 0; j < variables.Length; j++)
            {
                FieldInfo fieldInfo = typeof(T).GetField(variables[j]); // T �� �ִ� �������� ������ ��������.

                TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType); // int.Parse, float.Parse �� ����� �� �� �ִ� ������ ���� ����.

                fieldInfo.SetValue(info, typeConverter.ConvertFrom(value[j])); // value[i] �� ���� typeConverter �� �̿� �ؼ� ������ ����.
            }

            list.Add(info);
        }

        return list;
    }
}
