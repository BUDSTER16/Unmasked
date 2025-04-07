using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    private bool groceries = false, volunteer = false, court = false, dinner = false;

    private bool stoppedFinancier, stoppedPseudo, stoppedModder, stoppedDesire;

    private bool foughtFinancier = false, foughtPseudo = false, foughtModder = false, foughtDesire = false, foughtBoss = false;

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
                foughtFinancier = true;
                break;
            case Quest.Pseudo:
                stoppedPseudo = true;
                foughtPseudo = true;
                break;
            case Quest.Modder:
                stoppedModder = true;
                foughtModder = true;
                break;
            case Quest.Desire:
                stoppedDesire = true;
                foughtDesire = true;
                break;
        }
    }

    public void LeaveVillain()
    {
        switch (currentQuest)
        {
            case Quest.None:
                foughtBoss = true;
                break;
            case Quest.Financier:
                stoppedFinancier = false;
                foughtFinancier = true;
                break;
            case Quest.Pseudo:
                stoppedPseudo = false;
                foughtPseudo = true;
                break;
            case Quest.Modder:
                stoppedModder = false;
                foughtModder = true;
                break;
            case Quest.Desire:
                stoppedDesire = false;
                foughtDesire = true;
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
                SceneManager.LoadScene("End");
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

    public bool VillainFought()
    {
        bool fought = false;
        switch (currentQuest)
        {
            case Quest.None:
                fought = foughtBoss;
                break;
            case Quest.Financier:
                fought = foughtFinancier;
                break;
            case Quest.Pseudo:
                fought = foughtPseudo;
                break;
            case Quest.Modder:
                fought = foughtModder;
                break;
            case Quest.Desire:
                fought = foughtDesire;
                break;
        }
        return fought;
    }

    public string Task()
    {
        string task = "";

        switch(currentQuest)
        {
            case Quest.None:
                if(foughtBoss) { task = "Go to sleep"; }
                else { task = "Endgame"; }
                break;
            case Quest.Financier:
                if (foughtFinancier) { task = "Go get groceries"; }
                else { task = "Put on the mask in your apartment"; }
                break;
            case Quest.Pseudo:
                if (foughtPseudo) { task = "Volunteer at the homeless shelter"; }
                else { task = "Put on the mask in your apartment"; }
                break;
            case Quest.Modder:
                if (foughtModder) { task = "Go to the courthouse (on the far side of town)"; }
                else { task = "Put on the mask in your apartment"; }
                break;
            case Quest.Desire:
                if (foughtDesire) { task = "Take the bus"; }
                else { task = "Put on the mask in your apartment"; }
                break;
        }

        return task;
    }
}
