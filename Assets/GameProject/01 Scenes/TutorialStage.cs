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


    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject Tutorial3;
    public GameObject Tutorial4;
    public GameObject Group1;


    bool test1=false;

    private IEnumerator Start()
    {
        
        Group1.SetActive(false);

        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(false);
        yield return new WaitUntil(() => dialogSystem01.UpdateDialog());
        Tutorial1.SetActive(true);


        while (test1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                test1 = true;
            }
        }

        yield return StartCoroutine(Wait());
        Tutorial1.SetActive(false);
        yield return new WaitUntil(() => dialogSystem02.UpdateDialog());
        Tutorial2.SetActive(true);

        yield return StartCoroutine(Wait());

        Group1.SetActive(true);
        Tutorial2.SetActive(false);



        yield return new WaitUntil(() => dialogSystem03.UpdateDialog());
        Tutorial3.SetActive(true);


        yield return StartCoroutine(Wait());
        Tutorial3.SetActive(false);


        yield return new WaitUntil(() => dialogSystem04.UpdateDialog());
        Tutorial4.SetActive(true);


        yield return StartCoroutine(Wait());
        Tutorial4.SetActive(false);

        Destroy(Group1);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(4f);
    }

    public void OnNext()
    {
        LoadingSceneController.LoadScene("Stage1 Scene");
    }
}
