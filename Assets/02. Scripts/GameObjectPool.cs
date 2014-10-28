using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// 참고 http://cafe.naver.com/unityhub/5393
// http://hyunity3d.tistory.com/195

public class GameObjectPool : IEnumerable, System.IDisposable {

    private List<GameObject> list = new List<GameObject>();
    private GameObject originalObject;
    private int maxCount;

    public List<GameObject> GetList()
    {
        return list;
    }

    public IEnumerator GetEnumerator()
    {
        foreach(GameObject item in list)
        {
            yield return item;
        }
    }
    
    // 지정한 개수만큼 게임 오브젝트를 생성해서 메모리 풀 생성
    public bool Create(GameObject original, int maxCount)
    {
        this.originalObject = original;
        this.maxCount = maxCount;

        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = GameObject.Instantiate(originalObject) as GameObject;
            newItem.SetActive(false);
            list.Add(newItem);
        }
        return true;
    }

    // 게임 오브젝트들의 부모를 지정한다.
    public bool SetParent( Transform parent )
    {
        if ( parent == null )
        {
            return false;
        }
        if ( list == null )
        {
            return false;
        }

        foreach ( GameObject item in list )
        {
            item.transform.parent = parent;
        }

        return true;
    }

    // 리스트에 담긴 게임 오브젝트를 반환
    public GameObject NewItem()
    {
        if ( list == null )
        {
            return null;
        }

        if ( list.Count > 0 )
        {
            foreach ( GameObject item in list )
            {
                if ( item.gameObject.activeSelf == false )
                {
                    item.gameObject.SetActive( true );
                    return item.gameObject;
                }
            }
        }
        else if ( list.Count < maxCount )
        {
            GameObject newItem = GameObject.Instantiate(originalObject) as GameObject;
            list.Add(newItem);
            return newItem;
        }
        else
        {
            throw new UnityException("Memory Pool의 한도를 초과하였습니다.");
        }

        return null;
    }

    // 해당 게임 오브젝트를 비활성화 시킨다.
    public bool RemoveItem( GameObject gameObject )
    {
        if ( list == null )
        {
            return false;
        }

        if ( gameObject == null )
        {
            return false;
        }

        foreach ( GameObject item in list )
        {
            if ( item == gameObject )
            {
                item.SetActive(false);
                break;
            }
        }
        return true;
    }

    // 새로운 게임 오브젝트를 리스트에 추가
    public void InsertItem(GameObject gameObject)
    {
        if ( gameObject == null)
        {
            return;
        }

        gameObject.SetActive(false);
        list.Add(gameObject);
    }

    // 모든 게임 오브젝트들을 비활성화
    public void DeactivateItems()
    {
        foreach ( GameObject item in list )
        {
            item.SetActive( false );
        }
    }

    // 실제로 게임 오브젝트를 파괴하고 리스트를 비운다
    public void Dispose()
    {
        foreach( GameObject item in list )
        {
            GameObject.Destroy(item);
        }

        list.Clear();
        list = null;
    }
   
}
