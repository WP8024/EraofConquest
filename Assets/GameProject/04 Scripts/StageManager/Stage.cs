using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    /// 정적 스태틱 클래스 Static singleton class
    public static Stage instance;

    /// 스테이지선택화면 카메라로 할당 Camera for the Map assigned from inspector
    public GameObject mainCameraGO;

    /// 마지막 클릭한 맵노드 저장 Stores last clicked MapNode
    private StageNode lastMapNodeClicked;

    void Start()
    {
        //싱글톤 할당 asign singleton
        instance = this;

        //자식 생성 tell children to initiate
        this.gameObject.BroadcastMessage("Init", SendMessageOptions.DontRequireReceiver);
    }



    /// 이미지를 클릭한 경우 호출 Calld when map image clicked
    public void OnMapClicked()
    {
        ///hide last MapNode Battle Button
        if (lastMapNodeClicked != null)
            lastMapNodeClicked.ShowBattleButton(false);

    }

    /// 맵노드 클릭시 호출 Calld from MapNode when it gets clicked
    public void OnMapNodeClicked(StageNode stageNode)
    {

        ///hide last MapNode Battle Button
        if (lastMapNodeClicked != null)
            lastMapNodeClicked.ShowBattleButton(false);

        ///맵노드 배틀 버튼을 보여준다 show clicked mapNode battle button
        stageNode.ShowBattleButton(true);


        ///클릭한 노드를 저장한다 store clicked MapNode
        lastMapNodeClicked = stageNode;
    }

    /// 시점 활성화 여부 Set map visible if isVisible param is true
    public void SetVisibility(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
        Debug.Log(isVisible);
        //mainCameraGO.SetActive(isVisible);
    }

}
