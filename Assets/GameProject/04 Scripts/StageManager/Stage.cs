using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    /// ���� ����ƽ Ŭ���� Static singleton class
    public static Stage instance;

    /// ������������ȭ�� ī�޶�� �Ҵ� Camera for the Map assigned from inspector
    public GameObject mainCameraGO;

    /// ������ Ŭ���� �ʳ�� ���� Stores last clicked MapNode
    private StageNode lastMapNodeClicked;

    void Start()
    {
        //�̱��� �Ҵ� asign singleton
        instance = this;

        //�ڽ� ���� tell children to initiate
        this.gameObject.BroadcastMessage("Init", SendMessageOptions.DontRequireReceiver);
    }



    /// �̹����� Ŭ���� ��� ȣ�� Calld when map image clicked
    public void OnMapClicked()
    {
        ///hide last MapNode Battle Button
        if (lastMapNodeClicked != null)
            lastMapNodeClicked.ShowBattleButton(false);

    }

    /// �ʳ�� Ŭ���� ȣ�� Calld from MapNode when it gets clicked
    public void OnMapNodeClicked(StageNode stageNode)
    {

        ///hide last MapNode Battle Button
        if (lastMapNodeClicked != null)
            lastMapNodeClicked.ShowBattleButton(false);

        ///�ʳ�� ��Ʋ ��ư�� �����ش� show clicked mapNode battle button
        stageNode.ShowBattleButton(true);


        ///Ŭ���� ��带 �����Ѵ� store clicked MapNode
        lastMapNodeClicked = stageNode;
    }

    /// ���� Ȱ��ȭ ���� Set map visible if isVisible param is true
    public void SetVisibility(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
        Debug.Log(isVisible);
        //mainCameraGO.SetActive(isVisible);
    }

}
