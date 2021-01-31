<b>Instructions:</b>

  For those who are not very experienced programmers, or at least, found it a bit confusing I'm going to clarify this implementation.
 If you are just going to use it AS IS, then all you need is the 2 Components on the Components folder.
  
- The <b>Selection Handler</b>:

  This component is the one that deals with the selection or deselection of Units (or whatever objects you're going to make selectable). 
  
  For this to work you need to drag-and-drop it on any GameObject as you judge necessary. There's no need to have more than one of it active. 
  (Pay attention to the position of this object, since it can affect the behaviour of this script).
  
  Once you have done it, open the "Rect Settings" property that must be shown in the Inspector. You'll see that the colors are all black, set the colors for whatever the values you feel confortable (also make sure that the alpha is not 255(100%)). The first color is the color of the inner Rectangle on the screen, the second is the color of the Border of this rectangle, and the Thickness is also related to the border.
  
  At the end, 
