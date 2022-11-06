# IconViewer
The IconViewer, as the name suggests, gives you a good overview of your .svg files throughout the system in one place.

#### Before you start
Note that .NET 6 Runtime is __required__ in order for the program to run. You can download .NET 6 Runtime [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime).


## Getting started
On the first Launch the Icon Collection is going to look pretty empty, so the first step is to add a Path of a Folder you have containing .svg Files.
To add a Path go to Settings, in the Settings you'll find the "Add Folder" Button.  
Clicking it opens up the Windows Explorer, navigate to your Folder containing the Icons you wish to display and select it.

After adding your Paths you will see them listed in the Settings, they are automatically enabled for display.

<img src=https://user-images.githubusercontent.com/97343298/171405298-d1007d5f-dbac-4d92-8ab3-73f78c5ebd67.png width="623" height="450">

Clicking on the "Settings" Button again it will bring you to the Homepage. If the Icon is valid you will see it displayed in a similar way to this:

<img src=https://user-images.githubusercontent.com/97343298/171407808-b131d632-9265-43db-b14b-80cea06729d0.png width="623" height="450">

In case you only want or don't want to see a specific Folder you can navigate back to the Settings and use the Toggle-Button next to each Path to enable/disable them.  
By default, every new Path added is enabled.

## Interface

<img src=https://user-images.githubusercontent.com/97343298/171417234-3de7e900-5450-43d7-98ce-cf51b8f83fcd.png width="623" height="450">

1. Amount of Icons currently displayed. 
2. A single Icon-Cell, displays the Filename on Mouseover and has it's own Context menu. 
3. In the Icons Context menu you get the Option to Copy the File itself, the Icons PathData to the Clipboard or open the File in the Explorer. 
4. Refreshes the Icons Collection.
5. Changes the Color or all Icons _temporary_.
    - For a _permanent_ Change modify the Value found in the Settings.
6. Search Icons by their Filename. 
7. Opens/Closes the Settings. 
