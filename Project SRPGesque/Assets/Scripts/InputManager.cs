﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{

    private OWPlayerController playerController;
    private BattleUI battleUI;
    public Vector2 inputAxis;
    private GameManager gameManager;


    

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<OWPlayerController>();
        battleUI = GameObject.FindGameObjectWithTag("UI").GetComponent<BattleUI>();
        gameManager = GetComponent<GameManager>();
        
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.PauseToggle();
        }
        if (gameManager.isPaused) return;
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        
        inputAxis.y = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.LeftShift)) playerController.isRunning = true;
        else playerController.isRunning = false;

        

         
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (gameManager.gameState == GameManager.GameState.Battle)
            {
                if (gameManager.selecting == GameManager.SelectingMenu.selectingAction) gameManager.ConfirmSelectedCommand();
                else if (gameManager.selecting == GameManager.SelectingMenu.selectingMove)
                {
                    if (!gameManager.tileScript.tiles[gameManager.selectedTile].isOccupied)
                    {
                        gameManager.tileScript.tiles[gameManager.charControl[0].tileID].isOccupied = false;//this one first before its tile gets changed
                        gameManager.MoveFormation(0, gameManager.tileSelection);
                        gameManager.tileScript.tiles[gameManager.selectedTile].isOccupied = true;
                    }
                }
                else if (gameManager.selecting == GameManager.SelectingMenu.selectingAttack)
                {
                    gameManager.ConfirmAttackSelection();
                }
                else if(gameManager.selecting == GameManager.SelectingMenu.selectingTarget)
                {
                    gameManager.ConfirmAttackTarget();
                }
                else if(gameManager.selecting == GameManager.SelectingMenu.victoryScreen)
                {
                    battleUI.victoryPanel.SetActive(false);
                    gameManager.EndBattle();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (gameManager.selecting != GameManager.SelectingMenu.selectingTarget) gameManager.ReturnToCommandSelection();
            else if (gameManager.selecting == GameManager.SelectingMenu.selectingTarget) { gameManager.ReturnToAttackSelect(); }
        }
        
        
        if(gameManager.gameState == GameManager.GameState.Overworld)
        {
            playerController.SetAxis(inputAxis);
        }
        else if(gameManager.gameState == GameManager.GameState.Battle)
        {
            gameManager.SetAxis(inputAxis);
            battleUI.SetAxis(inputAxis);
        }
        
        

        //Konami Code for debug
        if(Input.GetKeyDown(KeyCode.F10)) gameManager.ToggleDebug();
        if(gameManager.debug)
        {
            if(Input.GetKeyDown(KeyCode.B)) gameManager.InitializeEncounter();
            if(Input.GetKeyDown(KeyCode.N)) gameManager.randomEcountersOn = !gameManager.randomEcountersOn;
        }

    }

}

