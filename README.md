# Error 490
[Project Proposal Link](https://docs.google.com/document/d/1PstmsY7LsY0eRR9c_-MilDjLpJQR41U93yHbYdIdhLE/edit?usp=sharing)

# Getting Started
1. Install [Unity Hub](https://unity.com/download)
2. Sign into Unity Hub with a Unity account, or register if you don't already have one.
3. In the `Installs` sidebar, click `Install Editor` and download the `2021.3.18f1` version of Unity. When asked which modules to download, it is recommended that you uncheck all of them including Visual Studio, assuming you already have a text editor you're happy editing code in. The only module you should download is the Build Support module for your platform: either `Windows Build Support (Mono)`, `Mac Build Support (IL2CPP)`, or `Linux Build Support (Mono)`.
4. You can now go to `Projects` in the sidebar, and press Open. Select the folder where you cloned this repository.
5. After a while, Unity will load all necessary libraries and you will be able to see the game in the Unity editor!

# Making Changes
You can browse the `Assets/Scenes` folder to see the different scenes of the game. If you want to add new things, it's probably best to start in the `Gameplay` scene. Here, you can create new `GameObjects`, attach components to them, and render them in the scene. You can edit and create new scripts in the `Assets/Scripts` folder; save new sprites to the `Assets/Sprites` folder; and create prefabs in the `Assets/Prefabs` folder. Note that if you have a prefab that will be instantiated on the network using PUN, the library requires that the prefab exists in the `Assets/Resources` folder.

# Building the Game
To build the game, simply click File in the top menu bar, then press build and run.
