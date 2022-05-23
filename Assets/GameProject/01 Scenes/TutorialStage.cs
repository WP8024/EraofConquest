using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage : MonoBehaviour
{
    [SerializeField]
    private DialogSystem dialogSystem01;
    [SerializeField]
    private DialogSystem dialogSystem02;
    [SerializeField]
    private DialogSystem dialogSystem03;
    [SerializeField]
    private DialogSystem dialogSystem04;

    public GameObject Group1;
    public GameObject Group2;
    public GameObject Group3;

    private IEnumerator Start()
    {
        Group1.SetActive(false);
        Group2.SetActive(false);
        Group3.SetActive(false);

        yield return new WaitUntil(() => dialogSystem01.UpdateDialog());

        Group1.SetActive(true);







        yield return new WaitUntil(() => dialogSystem02.UpdateDialog());
    }
}
