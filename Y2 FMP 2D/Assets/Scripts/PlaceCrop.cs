using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections.Generic;
using System;

public class PlaceCrop : MonoBehaviour
{
    [SerializeField] private RuleTile highlightTile;
    [SerializeField] private Tilemap highlightMap;
    [SerializeField] private Tilemap cropMap;
    [SerializeField] private RuleTile cropTile;

    private Animator animator;
    public PlayerInput speedScript;

    private InventoryManager inventoryManager;

    public Item[] seedPouch;
    public RuleTile[] crop;
    private int index;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedScript = GetComponent<PlayerInput>();
    }


    //private void LateUpdate()
    //{
    //    Vector3Int currentCell = highlightMap.WorldToCell(transform.position);
    //    Vector3Int currentCrop1 = cropMap.WorldToCell(transform.position);
    //    var currentCrop = cropMap.GetTile(currentCrop1);
    //    
    //    var currentTile = highlightMap.GetTile(currentCell);
    //
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (currentTile == highlightTile && currentCrop == null)
    //        {
    //            if (seedPouch.Contains(inventoryManager.GetSelectedItem(false)))
    //            {
    //                for (int i = 0; i < seedPouch.Length; i++)
    //                {
    //                    if (seedPouch[i] == inventoryManager.GetSelectedItem(false)) {index = i; break; }
    //                }
    //
    //                cropTile = crop[index];
    //
    //                //speedScript.enabled = false;
    //
    //                animator.SetBool("IsHoeing", true);
    //
    //                animator.Play("Hoeing Blend Tree");
    //
    //                cropMap.SetTile(currentCell, cropTile);
    //
    //                animator.SetBool("IsHoeing", false);
    //            }
    //        }
    //
    //        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Hoeing Blend Tree")) == false)
    //        {
    //            //speedScript.enabled = true;
    //        }
    //    }
    //    
    //    
    //}

    public void PlantCrop()
    {
        Vector3Int currentCell = highlightMap.WorldToCell(transform.position);
        Vector3Int currentCrop1 = cropMap.WorldToCell(transform.position);
        var currentTile = highlightMap.GetTile(currentCell);
        var currentCrop = cropMap.GetTile(currentCrop1);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTile == highlightTile && currentCrop == null)
            {
                if (seedPouch.Contains(inventoryManager.GetSelectedItem(false)))
                {
                    for (int i = 0; i < seedPouch.Length; i++)
                    {
                        if (seedPouch[i] == inventoryManager.GetSelectedItem(false)) { index = i; break; }
                    }

                    cropTile = crop[index];

                    //speedScript.enabled = false;

                    animator.SetBool("IsHoeing", true);

                    animator.Play("Hoeing Blend Tree");

                    cropMap.SetTile(currentCell, cropTile);

                    animator.SetBool("IsHoeing", false);
                }
            }
        }
    }

    public void HarvestCrop()
    {
        Vector3Int currentCell = highlightMap.WorldToCell(transform.position);
        Vector3Int currentCrop1 = cropMap.WorldToCell(transform.position);
        var currentTile = highlightMap.GetTile(currentCell);
        var currentCrop = cropMap.GetTile(currentCrop1);

        if (currentTile == highlightTile && currentCrop != null)
        {
            cropMap.SetTile(currentCell, null);
        }
    }
}
