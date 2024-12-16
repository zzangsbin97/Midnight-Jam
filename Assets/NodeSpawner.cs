using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hitpointObject; //Hitpoint ������Ʈ
    public GameObject nodePrefab; // ������ ��� ������
    private KeyCode deleteButton; // ��� �����ϴ� ��ư
    private Color nodeColor;    //��� ��
    private float spawnInterval; // ��� ���� ����
    private float nodeDestroyedYPos;  //��尡 ����� y��ǥ

    private List<GameObject> spawnedNodes = new List<GameObject>(); // ������ ��� ���� ����Ʈ

    private float timer = 0f;

    void Start()
    {
        SetNode();
        spawnInterval = Random.Range(3f, 8f);
    }

    void Update()
    {
        timer += Time.deltaTime; // ��� �ð� ����

        if (timer >= spawnInterval) // ���� ���ݿ� �������� ��
        {
            SpawnNode(); // ��� ����
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
            spawnInterval = Random.Range(2f, 5f); //�ӽ� ���� �ð� ����
        }

        //���� Ű ���� ù ��° ��带 ����
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
            GameObject newNode = Instantiate(nodePrefab, transform.position, Quaternion.identity); // ��� ����
            Renderer nodeRenderer = newNode.GetComponent<Renderer>();
            if (nodeRenderer != null)
            {
                nodeRenderer.material.color = nodeColor; // ���� ����
            }
            spawnedNodes.Add(newNode); // ������ ��带 ����Ʈ�� �߰�
        }
    }

    void DestroyFirstNode()
    {
        // ���� �Ʒ� ��� ����
        GameObject firstNode = spawnedNodes[0];
        spawnedNodes.RemoveAt(0); // ù ��° ��� ����Ʈ���� ����
        nodeDestroyedYPos = firstNode.transform.position.y; //����� y��ǥ ���
        CompareYPositionWithHitpoint();     //hitpoint�� ���̰� ���
        Destroy(firstNode); // �ش� ��带 ����
    }


    void CompareYPositionWithHitpoint()
    {
        if (hitpointObject != null)
        {
            float hitpointY = hitpointObject.transform.position.y; //hitpoint�� y��ǥ ��������

            // ���������� ����� ����� y��ǥ�� ��Ʈ����Ʈ y��ǥ ��
            float difference = nodeDestroyedYPos - hitpointY;

            // ���̰� ���
            Debug.Log("Y��ǥ ���̰�: " + difference);
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
