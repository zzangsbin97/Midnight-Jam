using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hitpointObject; //Hitpoint 오브젝트
    public GameObject nodePrefab; // 생성할 노드 프리팹
    private KeyCode deleteButton; // 노드 삭제하는 버튼
    private Color nodeColor;    //노드 색
    private float spawnInterval; // 노드 생성 간격
    private float nodeDestroyedYPos;  //노드가 사라진 y좌표

    private List<GameObject> spawnedNodes = new List<GameObject>(); // 생성된 노드 관리 리스트

    private float timer = 0f;

    void Start()
    {
        SetNode();
        spawnInterval = Random.Range(3f, 8f);
    }

    void Update()
    {
        timer += Time.deltaTime; // 경과 시간 누적

        if (timer >= spawnInterval) // 생성 간격에 도달했을 때
        {
            SpawnNode(); // 노드 생성
            timer = 0f; // 타이머 초기화
            spawnInterval = Random.Range(2f, 5f); //임시 생성 시간 지정
        }

        //눌린 키 줄의 첫 번째 노드를 삭제
        if (Input.GetKeyDown(deleteButton) && spawnedNodes.Count > 0)
        {
            DestroyFirstNode();
            Debug.Log(deleteButton + " deleted");
        }
    }


    void SpawnNode()
    {
        if (nodePrefab != null)
        {
            GameObject newNode = Instantiate(nodePrefab, transform.position, Quaternion.identity); // 노드 생성
            Renderer nodeRenderer = newNode.GetComponent<Renderer>();
            if (nodeRenderer != null)
            {
                nodeRenderer.material.color = nodeColor; // 색상 적용
            }
            spawnedNodes.Add(newNode); // 생성된 노드를 리스트에 추가
        }
    }

    void DestroyFirstNode()
    {
        // 가장 아래 노드 삭제
        GameObject firstNode = spawnedNodes[0];
        spawnedNodes.RemoveAt(0); // 첫 번째 노드 리스트에서 제거
        nodeDestroyedYPos = firstNode.transform.position.y; //노드의 y좌표 기록
        CompareYPositionWithHitpoint();     //hitpoint와 차이값 계산
        Destroy(firstNode); // 해당 노드를 삭제
    }


    void CompareYPositionWithHitpoint()
    {
        if (hitpointObject != null)
        {
            float hitpointY = hitpointObject.transform.position.y; //hitpoint의 y좌표 가져오기

            // 마지막으로 사라진 노드의 y좌표와 히트포인트 y좌표 비교
            float difference = nodeDestroyedYPos - hitpointY;

            // 차이값 출력
            Debug.Log("Y좌표 차이값: " + difference);
        }
        else
        {
            Debug.LogWarning("Hitpoint Object is not assigned!");
        }
    }

    void SetNode()
    {
        if (CompareTag("Q"))
        {
            deleteButton = KeyCode.Q;
            nodeColor = Color.red;
        }
        if (CompareTag("W"))
        {
            deleteButton = KeyCode.W;
            nodeColor = Color.green;
        }
        if (CompareTag("Space"))
        {
            deleteButton = KeyCode.Space;
            nodeColor = Color.blue;
        }
        if (CompareTag("O"))
        {
            deleteButton = KeyCode.O;
            nodeColor = Color.magenta;
        }
        if (CompareTag("P"))
        {
            deleteButton = KeyCode.P;
            nodeColor = Color.cyan;
        }

    }
}
