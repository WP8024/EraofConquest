using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StageNodeStatus { Locked, Unlocked, Completed };
public class StageNode : MonoBehaviour
{
    public StageNodeStatus stageNodeStatus;

    public StageNode[] nextStage;

    public string sceneNameToLoad;

    public GameObject Lockedimage, Unlockedimage, Completedimage;
    public GameObject battleButton;

    public void Init()
    {
   

        ///set startup status
        SetStatus(stageNodeStatus);

        ///hide battle button on start
        ShowBattleButton(false);
    }

    public void SetStatus(StageNodeStatus _stageNodeStatus)
    {
        ///return if this MapNode completed
        if (_stageNodeStatus == StageNodeStatus.Completed)
            return;

        ///set global status
        stageNodeStatus = _stageNodeStatus;

        ///hide all images
        Lockedimage.SetActive(false);
        Unlockedimage.SetActive(false);
        Completedimage.SetActive(false);

        if (_stageNodeStatus == StageNodeStatus.Locked)
        {
            ///show lock image
            Lockedimage.SetActive(true);
        }
        else if (_stageNodeStatus == StageNodeStatus.Unlocked)
        {

            Unlockedimage.SetActive(true);
            ///set button to be interactable
            GetComponent<Button>().interactable = true;
        }
        else if (_stageNodeStatus == StageNodeStatus.Completed)
        {
            ///show complete image
           Completedimage.gameObject.SetActive(true);

            ///disable button
            GetComponent<Button>().interactable = false;
        }
    }
    public void ShowBattleButton(bool button)
    {
        battleButton.gameObject.SetActive(button);
    }
    public void OnStageNodeClicked()
    {
        ///call function on Map class and pass this MapNode
        Stage.instance.OnMapNodeClicked(this);
    }
    public void OnStartBattleClicked()
    {
        StageManager.instance.LoadNewBattle(this);
        
    }
    public void OnNodeComplete()
    {
        ///return if this node is locked
        if (stageNodeStatus == StageNodeStatus.Locked)
            return;

        ///return if this node is completed
        if (stageNodeStatus == StageNodeStatus.Completed)
            return;

        ///set status of the node
        SetStatus(StageNodeStatus.Completed);

        ///unlocks connected nodes
        for (int i = 0; i < nextStage.Length; i++)
        {
            nextStage[i].SetStatus(StageNodeStatus.Unlocked);
        }
    }
}
