using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Common;

// 참고 - http://andwhy.tistory.com/77
public class PathFinder {
    private static PathFinder _instance = null;

    /// <summary>
    /// 싱글톤 패턴 적용, 인스턴스가 없으면 새로 생성해서 반환한다.
    /// </summary>
    public static PathFinder Instance
    {
        get
        {
            if ( _instance == null )
            {
                _instance = new PathFinder();                
            }
            return _instance;
        }
    }

    /// <summary>
    /// 생성자
    /// </summary>
    public PathFinder()
    {
        Init();
    }

    /// <summary>
    /// 패스파인더에 사용할 맵노드 클레스입니다.
    /// PathFinder.cs파일 아래쪽에 보시면 MapData라는 클래스를 하나 더 선언했는데, C#에선 한파일에 여러 클래스파일을 선언해서 사용할수 있습니다.
    /// MapData가 하는 역할은 갈수 없는 셀은 -1로 표시하고, 갈수 있는길은 0으로 표시해둡니다.
    /// 길을 찾을때 값이 0인 셀들만 검색해서 찾고 찾은셀은 seachCount 값을 넣어줄겁니다.
    /// </summary>
    private MapData mapData;

    /// <summary>
    /// 원본 맵입니다. 보통 길찾기라면 굳이 필요없지만, 이번엔 목표지점이 정해져있지않고, 특정인덱스를 찾아가기 때문에 필요합니다.
    /// </summary>
    private int[,] originalMap;
    private bool isSearching = false;
    private int searchCount = 0;

    /// <summary>
    /// 목표지점 포인트 좌표 입니다. 특정인덱스를 맵에서 검색해서 목표지점을 정해줍니다.
    /// </summary>
    private Point goalPoint;

    public void Init()
    {
        mapData = new MapData();
        isSearching = false;
        searchCount = 0;
    }

    public void SetMapData(int[,] _map)
    {
        originalMap = _map;
        mapData.SetMapData(originalMap);
    }

    /// <summary>
    /// 외부에서 호출하는 길찾기 매쏘드 입니다.
    /// 현재 위치와 목표 인덱스를 가지고 길을 찾습니다.
    /// 현재위치가 맵상에 없는 위치이거나, 목표인덱스가 맵상에서 찾을수 없을경우 굳이 검색을 하지 않아도 되기때문에 첫번째 줄에서
    /// 검사하여, return 시켜버립니다.
    /// 길찾기를 한번할때마다 mapData를 초기화 시켜주고, 
    /// 시작점부터 카운터를 올려가며 탐색을 시킵니다.("SetPathCount()" 재귀함수 사용).
    /// 탐색이 끝나고, 목표지점까지 갈수있는길이 있는지 없는지 확인후에, 
    /// 목표지점에서 시작점까지 오는 최단거리를 검색합니다.("FindPath()" 재귀함수 사용).
    /// </summary>
    /// <param name="current"></param>
    /// <param name="targetIndex"></param>
    /// <returns></returns>
    public Point[] GetPath(Point current, int targetIndex)
    {
        if (!CheckInMapPoint(current) || !CheckInMapIndex(targetIndex))
        {
            return null;
        }

        mapData.SetMapData(originalMap);
        Debug.Log("START SEARCHING.. ");
        isSearching = true;
        searchCount = 1;

        SetPathCount(new List<Point>() { current }, targetIndex, 100);

        if (goalPoint == null)
        {
            return null;
        }

        List<Point> pList = FindPath();

        if (pList == null)
        {
            return null;
        }

        return pList.ToArray();
    }

    
    private void SetPathCount(List<Point> pList, int targetIndex)
    {
        if (originalMap == null)
        {
            return;
        }

        SetPathCount(pList, targetIndex, originalMap.GetLength(0) * originalMap.GetLength(1));
    }

    /// <summary>
    /// 시작점부터 목표점까지 갈수있는 길을 표시합니다.
    /// pList는 현재 탐색중인 셀들의 리스트 입니다.처음엔 1개로 시작하지만, 상하좌우 의 인접한 셀들이 계속 추가 됩니다.
    /// pList의 Point좌표들을 돌면서 각각 셀에 searchCount를 표시하고(시작점의 searchCount는 1입니다.), 표시된 셀의 인접한 셀들을 다음검색목록(_List)에 추가합니다.
    /// 만일 pList의 Point들을 다 돌고도 인접한 셀이 더이상없다면, 더이상은 갈길이 없으므로 막혀있는길입니다.
    /// 최대치를 넣어서, 최대치 이상으로 검색될경우 길이 없다고 처리해버립니다.(없어도 되지만 재귀함수가 무한으로 도는걸방지합니다.)
    /// 다음검색목록이 비어있지않고, 목적지까지 길을 찾지도 못했다면 다음검색목록을 가지고 다시 setPathCount함수를 실행합니다.
    /// (길을 찾거나, 길이 없을때까지 이함수를 계속 실행하게 됩니다.만일 조건인 잘못되어서 무한으로 함수가 돌게 된다면 유니티를 강제 종료하는것이외엔 멈출수 없습니다
    /// </summary>
    /// <param name="pList"></param>
    /// <param name="targetIndex"></param>
    private void SetPathCount(List<Point> pList, int targetIdx, int max)
    {
        if (!isSearching) return;
        List<Point> _List = new List<Point>();
        foreach (Point p in pList)
        {
            List<Point> retList = RecordPath(p, targetIdx);
            if (retList != null && retList.Count > 0) { _List.AddRange(retList); }
        }
        if (_List.Count == 0) { Debug.Log("SEARCHING FAIL!!!!"); return; }
        searchCount++;
        if (searchCount >= max) { isSearching = false; return; }

        SetPathCount(_List, targetIdx, max);
    }

    /// <summary>
    /// 위의 setPathCount 에서 pList의 Point 좌표에 seachCount를 실제로 넣어주고, 주변 셀들을 리턴해주는 함수입니다.
    /// 만일 현재 Point p가, targetIdx와 일치 한다면 goalPoint에 현재 p를 넣고, 더이상 탐색을 하지 않도록 다음 검색할 셀이 없다고 리턴해줍니다.
    /// isSearching 값도 false로 변경해서 더이상은 검색하지 않도록 합니다.
    /// 만일 p가 targetIdx가 아니라면 seachCount를 현재 포인트좌표에 넣어주고, 상,하,좌,우 셀들중 값이 '0'인 셀(한번도 검색하지 않은 셀)들만 찾아서 리턴해줍니다.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="targetIdx"></param>
    /// <returns></returns>
    private List<Point> RecordPath(Point p, int targetIdx)
    {
        if (!isSearching) return null;
        if (originalMap[p.x, p.y] == targetIdx)
        {
            isSearching = false;
            mapData.map[p.x, p.y] = searchCount;
            goalPoint = p;
            Debug.Log("SEARCHING COMPLETE!!!!   : " + searchCount);
            return null;
        }
        List<Point> rList = null;
        if (mapData.map[p.x, p.y] == 0)
        {
            mapData.map[p.x, p.y] = searchCount;
            rList = new List<Point>();
            GetNeighbours(p, ref rList);
        }
        return rList;
    }

    private void GetNeighbours(Point p, ref List<Point> rList)
    {
        if (p.x > 0 && mapData.map[p.x - 1, p.y] == 0) rList.Add(new Point(p.x - 1, p.y));
        if (p.y > 0 && mapData.map[p.x, p.y - 1] == 0) rList.Add(new Point(p.x, p.y - 1));
        if (p.x < originalMap.GetLength(0) - 1 && mapData.map[p.x + 1, p.y] == 0) rList.Add(new Point(p.x + 1, p.y));
        if (p.y < originalMap.GetLength(1) - 1 && mapData.map[p.x, p.y + 1] == 0) rList.Add(new Point(p.x, p.y + 1));
    }

    /// <summary>
    /// 도착점부터 시작점까지 가는길을 구하는 함수입니다.
    /// 도착점에 표시된 searchCount 값을 시작으로, point의 상,하,좌,우 의 셀을 검색해서 현재 count값보다 하나 작은 카운트    값을 찾아서("getPathPoint()") 이동합니다.
    /// 이동후엔 카운트값이 1 이될때까지 계속 검색("getPathPoint()" 재귀함수 사용)합니다.
    /// 카운트가 1인지점은 시작지점이기때문에 1까지 검색이 완료되었으면 검색한 Point값을 차례대로 리턴해줍니다.
    /// 마지막 findPath()에서 리턴된 List<Point> 값이 최종으로 찾아진 길입니다. 만일 null이 나오게 된다면,
    /// 갈수 없는길이란 이야기죠.
    /// </summary>
    /// <returns></returns>
    private List<Point> FindPath()
    {
        List<Point> pathList = new List<Point>();
        Point temPoint = goalPoint;
        int _count = searchCount - 1;
        while (_count > 0)
        {
            pathList.Insert(0, temPoint.Clone());
            GetPathPoint(ref temPoint, _count);
            _count--;
        }
        pathList.Insert(0, temPoint);
        if (isSearching)
        {
            isSearching = false;
            return null;
        }
        return pathList;
    }

    private void GetPathPoint(ref Point p, int count)
    {
        if (p.y < mapData.map.GetLength(1) - 1 && mapData.map[p.x, p.y + 1] == count)
        {
            p.y += 1;
            return;
        }
        if (p.x < mapData.map.GetLength(0) - 1 && mapData.map[p.x + 1, p.y] == count)
        {
            p.x += 1;
            return;
        }
        if (p.y > 0 && mapData.map[p.x, p.y - 1] == count)
        {
            p.y -= 1;
            return;
        }
        if (p.x > 0 && mapData.map[p.x - 1, p.y] == count)
        {
            p.x -= 1;
            return;
        }
    }

    private bool CheckInMapPoint(Point p)
    {
        if (originalMap == null)
        {
            return false;
        }

        if (p.x < 0 || p.y < 0)
        {
            return false;
        }

        if (p.x >= originalMap.GetLength(0) || p.y >= originalMap.GetLength(1))
        {
            return false;
        }

        return true;
    }

    private bool CheckInMapIndex(int idx)
    {
        if (originalMap == null) return false;
        foreach (int _idx in originalMap)
        {
            if (idx == _idx) return true;
        }
        return false;
    }
}


public class MapData
{
    public int[,] map;

    public MapData()
    {

    }

    /// <summary>
    /// gameManager  에 있는 wallMap을 MapData 로 변형해줍니다
    /// </summary>
    /// <param name="_map"></param>
    public void SetMapData(int[,] _map)
    {
        int w = _map.GetLength(0);
        int h = _map.GetLength(1);
        map = new int[w, h];
        int x, y;

        for (x = 0; x < w; x++)
        {
            for (y = 0; y < h; y++)
            {
                if (_map[x, y] > 0 && _map[x, y] < 10)
                {
                    map[x, y] = -1;
                }                    
                else
                {
                    map[x, y] = 0;
                }
            }
        }
    }
}