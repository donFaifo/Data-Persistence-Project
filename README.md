# Data-Persistence-Project
 
Project from Unity's Junior Programmer Pathway to learn how to make data persistence between scenes and between sessions. 
In the main menu, you can set your name and there's a button which allows you to go to the settings scene and change the ball maximum speed. After saving this setting, you go back to the main menu.
When you start the game, on top of the screen will be displayed the best score and below the player's name and the current score. When the ball hits the ground, the best scores screen will be displayed with the three bests scores. From this scene you can hit space and start the game again or you can press escape and go back to the main menu.
I've made a class named *GameData* to store all the game information ready to serialize. The best scores are in a **List** of **Scores** in which the player will be added each time the game is over. Then, the **List** will be sorted so the bests scores will be at the start of the List and then, the three firsts elements will be kept and the rest will be removed.
For the settings, I've used a **Scrollbar** so the user can set the maximum speed between 3 and 10 (with the latter, it's highly difficult to keep playing more than a few seconds, I challenge you! ;-P).
All these scores and settings will be saved in a file called *data.json* in the persistent data path from the user's computer (or device) using a **JSON** object.
