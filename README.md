# DevelopmentTools
Tools for Development.

New tools puts in ToolsLibrary project.
Needs use MVVM, implements IToolBaseInfo.

Main project use Reflection to list all tools and display in main window.
It create tool's instance when MainViewModel created.

Properties in IToolBaseInfo interface:
 1. ToolName        Tool's name
 2. ToolDesc        Tool's description
 3. ToolWindow      Tool's entry window
 4. ToolIcon        Tool's icon showed in main window
 5. ToolAuthor      Tool's author
 6. ToolThemeColor  Tool's theme color displayed in main window
![image](https://user-images.githubusercontent.com/66509354/132986415-87b97094-e7ee-43b2-9c85-a08fc40040c9.png)
