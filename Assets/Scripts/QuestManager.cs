using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private bool groceries = false, volunteer = false, court = false, dinner = false;

    private bool stoppedFinancier, stoppedPseudo, stoppedModder, stoppedDesire;

    public enum Quest
    {
        Financier,
        Pseudo,
        Modder,
        Desire,
        None
    }

    private Quest currentQuest;

    private void Start()
    {
        currentQuest = Quest.None;
        DontDestroyOnLoad(gameObject);
    }

    public void StopVillain()
    {
        switch (currentQuest)
        {
            case Quest.Financier:
                stoppedFinancier = true;
                break;
            case Quest.Pseudo:
                stoppedPseudo = true;
                break;
            case Quest.Modder:
                stoppedModder = true;
                break;
            case Quest.Desire:
                stoppedDesire = true;
                break;
        }
    }

    public void LeaveVillain()
    {
        switch (currentQuest)
        {
            case Quest.Financier:
                stoppedFinancier = false;
                break;
            case Quest.Pseudo:
                stoppedPseudo = false;
                break;
            case Quest.Modder:
                stoppedModder = false;
                break;
            case Quest.Desire:
                stoppedDesire = false;
                break;
        }
    }

    public void CompleteQuest()
    {
        Quest nextQuest = Quest.None;
        switch (currentQuest)
        {
            case Quest.None:
                nextQuest = Quest.Financier;
                break;
            case Quest.Financier:
                groceries = true;
                nextQuest = Quest.Pseudo;
                break;
            case Quest.Pseudo:
                volunteer = true;
                nextQuest = Quest.Modder;
                break;
            case Quest.Modder:
                court = true;
                nextQuest = Quest.Desire;
                break;
            case Quest.Desire:
                dinner = true;
                break;
        }
        currentQuest = nextQuest;
    }

    public Quest CurrentQuest()
    {
        return currentQuest;
    }

    public void Nuke()
    {
        Destroy(gameObject);
    }

    public bool VillainStopped(Quest villain)
    {
        bool stopped = false;
        switch (villain)
        {
            case Quest.Financier:
                stopped = stoppedFinancier;
                break;
            case Quest.Pseudo:
                stopped = stoppedPseudo;
                break;
            case Quest.Modder:
                stopped = stoppedModder;
                break;
            case Quest.Desire:
                stopped = stoppedDesire;
                break;
        }
        return stopped;
    }
}
