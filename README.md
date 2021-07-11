# Fruits vs Veggies

A game I've made for a job interview. Thought it would be neat to keep it here for future.

## Description

Two opposing guilds fight each other protecting their resources!

### Controls:  
- Keyboard: A/D for the top player and Left/Right arrows for the bottom player  
- Touchscreen: The screen is divided between two players. There's a line. Use your finger.

## Implementation notes

- The ball has different sprites depending on the size so players could easily identify it as opposed to just scaling the ball up and down.  
    Personally, I believe my approach would also make much more sense in the production of a real game.

- Balls parameters are configurable through Scriptable Objects of type BallConfig. These object identify all of the parameters of the possible balls, including their size which also requires a sprite.

- Depending on the speed the color of balls trail will change to the one set in the config so Players could identify it more easier.

- I decided to risk and use Unity's new Input System, because it's cool and all of that. And I was curious about implementing that multitouch controls with it.  
    You need to turn on Simulated Touch if you run the game in the Editor to control the game with a mouse. But it also supports controls from the keyboard (A/D for the top player and Left/Right arrows for the bottom player).  

    _Go to Window > Analysis > Input Debugger._  
    _Press 'Options' drop-down menu in the top-left corner and select "Simulate Touch Input ..." option._

- There's a different approach possible: to move the player by the distance that finger moves. Needs some changes in PlayerAvatar class.  
    But the implemented approach where the platforms just follows the player's finger just feels better to me personally.

- Also I chose to use URP because I believe it's the default future of Unity.

- Changing the colour of the ball is achieved by using a Unity's ShaderLab shader which implements palette swapping.  
    It takes a gray-scale sprite of the ball and a gradient texture and then maps one on another. This should help with scalability of the project thus allowing creating many more balls without the need to create a colored variation for each of them. Works well for pixel art games!

- The size of the play area is determined by offsets from the width and height of the screen. It's stored in Scriptable Object assets in the Assets/Level folder.  
    In the first iteration of the game I've made it so that the play area takes the entire screen, but realized that on mobile fingers would obstruct the view on rackets and get in the way so I've decided to add a buffer zone.  
    But it also serves a secondary fuction as it makes it more clear to see the hit/goal area.

## Ways to improve the implementation

- Separacte the trail colors from the BallConfig into different speed type assets (e.g. Fast, Slow, etc) where you would store a speed and a matching color for this speed to show in the game. So that when you design more balls you don't have to copy paste speed and trail color values, but you could just pick an asset that would store all of the data about speed things. Kind of like it's done with BallSize.
- ???

## Ways to improve the game design

- ???

## TODO

- [x] Test controls on mobile (kind of important)
- [ ] Implement the realtime multiplayer
- [x] Make the play area smaller than the screen size so there's space for fingers on the top and the bottom of the screen and for the UI
     - [x] Make the goal area behind players with resources stashed there
     - [ ] On the sides make walls get revealed when the ball or players get near it with a soft mask
     - [x] Stop players from going further than side walls would allow them to
- [ ] Update black color on the sprites to not so black
- [ ] A lot of VFXs and Juice
- [ ] Make environment more interesting with some sprites
