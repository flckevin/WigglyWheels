using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * OBJECT HOLD: CANVAS IN MAIN MENU SCENE
 * PURPOSE: Menu manager to main menu in main scene
 */
public class MenuManager : MonoBehaviour
{
    //===================== GLOBAL ===============================
    private GameObject _currentMenu; // current menu being use
    //=============================================================


    #region MAIN MENU VARIBLES

    // Start is called before the first frame update
    [Header("MAIN MENU")]
    public GameObject mainMenu; // main menu

    #endregion

    #region LEVEL MENU VARIBLES

    [Space(20)]
    [Header("LEVEL MENU")]
    public Button[] LevelsButton; // all level buttons
    public LevelInfo[] levelInfo; // info of every level to display and load
    public Button playButton; // play button
    [Space(5)]
    [Header("LEVEL MENU_level selection")]
    public Image levelImg; // level image
    public Text levelDes; // level description

    #endregion


    #region SHOP MENU VARIBLES

    [Header("SHOP")]
    public Button[] itemButton;
    public ShopItemData[] vehicles;
    public ShopItemData[] characters;
    public Button buyButton;
    public Text price;
    public Text itemName;
    public Text moneyDisplay;
    #endregion



    void Start()
    {
        ActivateMenu(mainMenu);
        Initate_LevelMenu();
        ShopMenuSectionAssigner(0);
        ShopMenuInitiation();
    }

    #region GLOBAL

    /// <summary>
    /// function to assign current menu being activate
    /// </summary>
    /// <param name="_menu"> current menu in use </param>
    public void ActivateMenu(GameObject _menu)
    {
        //if current menu does exist
        if (_currentMenu != null)
        {
            //deactivate current menu
            _currentMenu.SetActive(false);
        }

        //activate new menu
        _menu.SetActive(true);
        //set current menu to be new menu
        _currentMenu = _menu;
    }

    #endregion

    //=================================================== LEVEL =====================================================================

    #region LEVEL MENU

    /// <summary>
    /// function to initiate at start
    /// </summary>
    void Initate_LevelMenu()
    {
        //add event to play button
        playButton.onClick.AddListener(PlayButton_LevelMenu);

        //loop all assigned button
        for (int i = 0; i < LevelsButton.Length; i++)
        {
            // Debug.Log(i);
            int temp = i;
            //assign event
            LevelsButton[i].onClick.AddListener(() => ButtonEventAssigner_LevelMenu(temp));

        }

    }

    /// <summary>
    /// function to assign event to every level button
    /// </summary>
    /// <param name="_ID"> level id </param>
    private void ButtonEventAssigner_LevelMenu(int _ID)
    {
        //Debug.Log(_ID);
        //change image sprite
        levelImg.sprite = levelInfo[_ID].levelImage;
        //change level text
        levelDes.text = levelInfo[_ID].levelText;
        //change level id
        GameData.LevelToLoadID = levelInfo[_ID].levelID;

        // Debug.Log("LOAD: " +  LevelData.levelToLoadID);   

    }


    /// <summary>
    /// function for play button in level menu
    /// </summary>
    private void PlayButton_LevelMenu()
    {
        //load main scene
        SceneManager.LoadScene(1);
    }
    #endregion

    //===============================================================================================================================


    //==================================================== SHOP ========================================================================

    #region SHOP MENU

    private void ShopMenuInitiation()
    {
        moneyDisplay.text = GameData.Money.ToString() + " $";

    }

    /// <summary>
    /// function to ASSIGN section
    /// </summary>
    /// <param name="_itemType"> type of item </param>
    public void ShopMenuSectionAssigner(int _itemType)
    {
        //if item is character
        if (_itemType == (int)ShopItemType.character)
        {
            //call section to change to character section
            ShopMenuSectionChanger(characters);
        }
        else //item is vehicle
        {
            //change to vehicle section
            ShopMenuSectionChanger(vehicles);
        }


    }

    /// <summary>
    /// function to CHANGE section
    /// </summary>
    /// <param name="_items"></param>
    void ShopMenuSectionChanger(ShopItemData[] _items)
    {
        int currentItem = 0;

        #region Button changing for section
        //loop all button
        for (int i = 0; i < itemButton.Length; i++)
        {
            //if item length less than item button length
            if (currentItem <= _items.Length - 1)
            {
                int temp = i;
                //keep assign all avalible buttons
                itemButton[i].onClick.AddListener(() => ButtonEventAssigner_Shop(_items[temp]));
                //activate button
                itemButton[i].gameObject.SetActive(true);
                //change button sprite to be item icon
                itemButton[i].image.sprite = _items[temp].itemIcon;
                currentItem++;
                //Debug.Log(currentItem);
            }
            else //there's no any item left to assign
            {
                //deactivate all other buttons
                itemButton[i].gameObject.SetActive(false);

            }

        }
        //Debug.Log("ASSIGNED");
        #endregion

    }


    /// <summary>
    /// function to assign to item button
    /// </summary>
    /// <param name="_ID"> array item id </param>
    /// <param name="_itemData"> data of the item </param>
    private void ButtonEventAssigner_Shop(ShopItemData _itemData)
    {

        //display item price
        price.text = _itemData.itemPrice.ToString();
        //display item name
        itemName.text = _itemData.name.ToString();
        buyButton.onClick.RemoveAllListeners();
        //assign buy event to be the same item we assigned
        buyButton.onClick.AddListener(() => BuyItem(_itemData));
    }


    /// <summary>
    /// buy function
    /// </summary>
    /// <param name="_itemData"> given item data </param>
    public void BuyItem(ShopItemData _itemData)
    {



        //if item data is vehicel
        if (_itemData.itemType == ShopItemType.vehicle)
        {
            //assign id to vehicle level data
            GameData.VehicleID = _itemData.itemID;
            Debug.Log(_itemData.itemID);
        }
        else //is character
        {
            //assign id to character level data
            GameData.PlayerID = _itemData.itemID;
            Debug.Log(_itemData.itemID);
        }

        moneyDisplay.text = GameData.Money.ToString() + " $";
    }


    #endregion

    //==============================================================================================================================


    #region OPTION MENU


    #endregion
}

