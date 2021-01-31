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
  To acess the list of the selected objects you must create a Reference for the Selection Handler in another script, and access "currentSelection" property.
  E.g:

<code>     
      
      using UnityEngine;
      using SelectionSystem.Components;
      
      public class Example : MonoBehaviour
      {
          // Reference to the component.
          SelectionHandler selectionHandler;

          void Start()
          {
              // Grab from the game object.
              selectionHandler = GetComponent<SelectionHandler>();

              // Total number of elements in the list (selected objects).
              int selectionCount = selectionHandler.currentSelection.Count;

              // Accessing each element in the list.
              foreach(var selected in selectionHandler.currentSelection)
              {
                // Do something with each element.
              }
          }
      }
  
</code>

  Just keep in mind that: 
  
  The elements in the list are referenced by the Interface <code>ISelectable</code>. A small tip to work with specific Type is:
  
  - Have sure your class has a reference to the Selector component.
  - Make your main class implements ISelectable through this component.
  Like:
<code>
    
    using UnityEngine;
    using SelectionSystem;
    using SelectionSystem.Components;
    
    public class Example2 : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private Selector selector;

        void Start()
        {
            selector = GetComponent<Selector>(); // Or .. just drag-and-drop from the inspector.
        }

        public void Select()
        {
            ((ISelectable)selector).Select();
        }

        public void Deselect()
        {
            ((ISelectable)selector).Deselect;
        }
    }
  
</code>
  
  This way, when you're accessing the selection list you can:

<code>
  
    foreach(Example2 selected in selectionHandler.currentSelection)
    {
      	// Now the elements came as the class you wanted! 
    }
  
</code>
