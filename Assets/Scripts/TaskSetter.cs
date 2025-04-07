using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskSetter : MonoBehaviour
{
    private QuestManager questManager;

    [SerializeField] private TMP_Text taskText;

    void Start()
    {
        questManager = FindAnyObjectByType<QuestManager>();
    }

    private void Update()
    {
        taskText.text = "To do: " + questManager.Task();
    }



}
