<b>Instructions:</b>

  For those who are not very experienced programmers, or at least, found it a bit confusing I'm going to clarify this implementation.
 If you are just going to use it AS IS, then all you need is the 2 Components on the Components folder.
  
- The <b>Selection Handler</b>:

  This component is the one that deals with the selection or deselection of Units (or whatever objects you're going to make selectable). 
  
  For this to work you need to drag-and-drop it on any GameObject as you judge necessary. There's no need to have more than one of it active. 
  (Pay attention to the position of this object, since it can affect the behaviour of this script).
  
  Once you have done it, open the "Rect Settings" property that must be shown in the Inspector. You'll see that the colors are all black, set the colors for whatever the values you feel confortable (also make sure that the alpha is not 255(100%)). The first color is the color of the inner Rectangle on the screen, the second is the color of the Border of this rectangle, and the Thickness is also related to the border.
  
  That's pretty much it. Now your Selections are beeing processed already automatically by it's own. If you, for some reason, wants to process the selections by another way, open the SelectionHandler.cs file and remove "ProcessSelections();" from the internal Update and call it from the outside.
  
- The <b>Selector</b>:
  
  This component is the one used by the Selection Handler to recognize this object as "selectable" internally.
  
  You can attach this object in any GameObject you want to make it selectable.
  
  Note: This gameObject must have a Rigidbody and a Collider or it won't work. If you're not intended to use Physics in your project so make sure this Rigidbody is marked as Kinematic and uncheck Use Gravity.


<b>General Tips:</b>
  - <b>Selection Handler</b>
  To acess the list of the selected objects you must create a Reference for the Selection Handler in another script, and acess "currentSelection" property.
  E.g:
<code>
using UnityEngine;
using SelectionSystem.Components;
  
  public SelectionHandler selectionHandler;
  
  void Start()
  {
    selectionHandler = GetComponent<SelectionHandler>();
    
    int selectionCount = selectionHandler.currentSelection.Count; // How much objects are selected at this moment
  
    foreach(var selected in selectionHandler.currentSelection)
    {
      // Do something for each object selected.
    }
  }
</code>

