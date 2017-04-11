﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaceSetup : MonoBehaviour
{
    public int playerCount = 2;

    public Camera minimap;

    public bool startEmbyMode = false;

    private GameObject[] players;

	// Use this for initialization
	void Awake ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        //FindObjectOfType<StateManager>().InstantiateDemCharacters(players);

        Vector2 rectSize = new Vector2(1.0f, 1.0f);
        bool vert = true;
        if (StateManager.numPlayers != 0) playerCount = StateManager.numPlayers;
        int divides = playerCount;

        while (divides / 2 != 0)
        {
            if (vert)
            {
                rectSize.x /= 2;
            }
            else
            {
                rectSize.y /= 2;
            }

            divides = (divides) / 2 + (divides % 2);
            vert = !vert;
        }

        for (int i = 0; i < players.Length; i++)
        {
            int j = int.Parse(players[i].name.Substring(1));// GetComponent<Character>().GetID();
            if (j <= playerCount)
            {
                //Vector2 refRes = GameObject.Find("InGameMenuP" + j.ToString()).GetComponent<CanvasScaler>().referenceResolution;
                //refRes.x /= rectSize.x;
                //refRes.y /= rectSize.y;
                //GameObject.Find("InGameMenuP" + j.ToString()).GetComponent<CanvasScaler>().referenceResolution = refRes;
                //GameObject.Find("OptionsMenuP" + j.ToString()).GetComponent<CanvasScaler>().referenceResolution = refRes;
                //GameObject.Find("InGameMenuP" + j.ToString()).SetActive(false);
                //GameObject.Find("OptionsMenuP" + j.ToString()).SetActive(false);
                //Vector3 scale = GameObject.Find("SpeedMeter" + j.ToString()).transform.localScale;
                //scale.x *= rectSize.x;
                //scale.y = scale.x;
                //GameObject.Find("SpeedMeter" + j.ToString()).transform.localScale = scale;
                //GameObject.Find("ChargeMeter" + j.ToString()).transform.localScale = scale;

                if (StateManager.GlobalIndexedSelection != null)
                    players[i].GetComponent<CharacterSelector>().characterSelection = StateManager.GlobalIndexedSelection[j - 1];

                switch (j - 1)
                {
                    case 0:
                        GameObject.Find("PlayerCamera" + j.ToString()).GetComponent<Camera>().rect = new Rect(new Vector2(0.0f, 1.0f - rectSize.y), rectSize);
                        break;
                    case 1:
                        GameObject.Find("PlayerCamera" + j.ToString()).GetComponent<Camera>().rect = new Rect(new Vector2(0.5f, 1.0f - rectSize.y), rectSize);
                        break;
                    case 2:
                        GameObject.Find("PlayerCamera" + j.ToString()).GetComponent<Camera>().rect = new Rect(new Vector2(0.0f, 0.0f), rectSize);
                        break;
                    case 3:
                        GameObject.Find("PlayerCamera" + j.ToString()).GetComponent<Camera>().rect = new Rect(new Vector2(0.5f, 0.0f), rectSize);
                        break;
                }
            }
            else
            {
                if (playerCount == 3)
                {
                    minimap.rect = new Rect(new Vector2(0.5f, 0.0f), rectSize);
                }
                players[i].SetActive(false);
            }
        }

        RaceManager[] rms = GetComponents<RaceManager>();

        foreach (RaceManager r in rms)
        {
            if (r.GetID() > playerCount)
            {
                r.enabled = false;
                GameObject hud = GameObject.Find("HUD" + r.GetID().ToString());
                hud.SetActive(false);
            }
        }

        GameObject.Find("CheckPoints").transform.parent = transform;

        //FindObjectOfType<StateManager>().InstantiateDemCharacters();
	}

    private void Update()
    {
        if (startEmbyMode)
        {
            foreach (GameObject g in players)
            {
                if (g.activeInHierarchy) g.GetComponent<CharacterSelector>().EmbyMode();
            }

            startEmbyMode = false;
        }
    }
}
