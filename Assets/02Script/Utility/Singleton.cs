using UnityEngine;

/// <summary>
/// 제네릭 프로그래밍 기반으로 만든 싱글톤 클래스
/// T타입은 MonoBehaviour를 상속받는 타입
/// 인스턴스가 없다면 생성해서 객체를 만들어내는 싱글톤.
/// 생성시에 DoAwake()호출
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object lockObj = new object();
    private static bool shuttingDown = false; // 객체 파괴요청 이후에 다른 객체에서 새롭게 객체를 만들어버리는 것을 방지.

    public static T Instance
    {
        get
        {
            if(shuttingDown)
            {
                Debug.Log($"셧다운에 객체 새로 생성 - {typeof(T)}");
                return null;
            }

            // 멀티쓰레드에서 중복 실행 되는것을 방지하기 위함.
            // 유니티는 싱글쓰레드이지만 여러가지 동시에 돌아가는 백그라운드 프로그램들이 클라이언트에 접근해서 인스턴스를 참조하는 경우가 발생할 수 도 있으므로
            lock(lockObj)
            {
                if(instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    //instance = (T)FindAnyObjectByType(typeof(T));

                    if (instance == null)// 씬에 T객체가 없다면
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
        // 게임이 종료될 때
        shuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        // 객체가 파괴될 때
        shuttingDown = true;
    }
}
