using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// 참고 - http://cafe.naver.com/unityhub/5393
// 참고 - http://hyunity3d.tistory.com/195


//전체 사용안함
public class GameObjectPool : IEnumerable, System.IDisposable {

    private Queue<GameObject> queue = new Queue<GameObject>();
    private GameObject originalObject;
    private int maxCount = 0;

    public Queue<GameObject> GetQueue()
    {
        return queue;
    }

    public IEnumerator GetEnumerator()
    {
        foreach(GameObject item in queue)
        {
            yield return item;
        }
    }
    
    /// 지정한 개수만큼 게임 오브젝트를 생성해서 메모리 풀 생성
    public bool Create(GameObject original)
    {
        this.originalObject = original;
        
        GameObject newItem = GameObject.Instantiate(originalObject) as GameObject;
        newItem.SetActive(false);            
        queue.Enqueue(newItem);
        
        return true;
    }

    /// 게임 오브젝트들의 부모를 지정한다.
    public bool SetParent( Transform parent )
    {
        if ( parent == null )
        {
            return false;
        }
       
        if ( queue == null )
        {
            return false;
        }

        foreach ( GameObject item in queue )
        {
            item.transform.parent = parent;
        }

        return true;
    }

    /// 리스트에 담긴 게임 오브젝트를 반환
    public GameObject NewItem()
    {
        
        if ( queue == null )
        {
            return null;
        }

        if ( queue.Count > 0 )
        {
            foreach ( GameObject item in queue )
            {
                if ( item.gameObject.activeSelf == false )
                {
                    item.gameObject.SetActive( true );
                    return item.gameObject;
                }
            }
        }
        else if (queue.Count < maxCount)
        {
            GameObject newItem = GameObject.Instantiate(originalObject) as GameObject;            
            queue.Enqueue(newItem);
            return newItem;
        }
        else
        {
            throw new UnityException("Memory Pool의 한도를 초과하였습니다.");
        }

        return null;
    }

    /// 해당 게임 오브젝트를 비활성화 시킨다.
    public bool RemoveItem( GameObject gameObject )
    {
        if ( queue == null )
        {
            return false;
        }

        if ( gameObject == null )
        {
            return false;
        }

        foreach ( GameObject item in queue )
        {
            if ( item == gameObject )
            {
                item.SetActive(false);
                break;
            }
        }
        return true;
    }

    /// 새로운 게임 오브젝트를 리스트에 추가
    public void InsertItem(GameObject gameObject)
    {
        if ( gameObject == null)
        {
            return;
        }

        gameObject.SetActive(false);
        queue.Enqueue(gameObject);
    }

    /// 모든 게임 오브젝트들을 비활성화
    public void DeactivateItems()
    {
        foreach ( GameObject item in queue )
        {
            item.SetActive( false );
        }
    }

    /// 실제로 게임 오브젝트를 파괴하고 리스트를 비운다
    public void Dispose()
    {
        foreach( GameObject item in queue )
        {
            GameObject.Destroy(item);
        }

        queue.Clear();
        queue = null;
    }
   
}
