using UnityEngine;

/// <summary>
/// ���׸� ���α׷��� ������� ���� �̱��� Ŭ����
/// TŸ���� MonoBehaviour�� ��ӹ޴� Ÿ��
/// �ν��Ͻ��� ���ٸ� �����ؼ� ��ü�� ������ �̱���.
/// �����ÿ� DoAwake()ȣ��
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object lockObj = new object();
    private static bool shuttingDown = false; // ��ü �ı���û ���Ŀ� �ٸ� ��ü���� ���Ӱ� ��ü�� ���������� ���� ����.

    public static T Instance
    {
        get
        {
            if(shuttingDown)
            {
                Debug.Log($"�˴ٿ ��ü ���� ���� - {typeof(T)}");
                return null;
            }

            // ��Ƽ�����忡�� �ߺ� ���� �Ǵ°��� �����ϱ� ����.
            // ����Ƽ�� �̱۾����������� �������� ���ÿ� ���ư��� ��׶��� ���α׷����� Ŭ���̾�Ʈ�� �����ؼ� �ν��Ͻ��� �����ϴ� ��찡 �߻��� �� �� �����Ƿ�
            lock(lockObj)
            {
                if(instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    //instance = (T)FindAnyObjectByType(typeof(T));

                    if (instance == null)// ���� T��ü�� ���ٸ�
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();

                        if(instance is Singleton<T> singleton)
                        {
                            singleton.DoAwake();
                        }
                    }
                    DontDestroyOnLoad(instance.gameObject);
                }                
                return instance;
            }
            
        }
    }

    protected virtual void DoAwake()
    {

    }

    protected virtual void OnApplicationQuit()
    {
        // ������ ����� ��
        shuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        // ��ü�� �ı��� ��
        shuttingDown = true;
    }
}
