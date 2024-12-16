using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Node : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 1f; //�������� �ӵ�
    public float destroyHeight = -6f;


    void Update()
    {
        //�Ʒ��� ������
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // ȭ�� �Ʒ��� ������� ��� ����
        if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject);
        }
    }

}
