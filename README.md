# Fruits vs Veggies

A game I've made for a job interview. Thought it would be neat to keep it here for future.

## Description

Two opposing guilds fight each other protecting their resources!

## Implementation notes

- The ball has different sprites depending on the size so players could easily identify it as opposed to just scaling the ball up and down. 

    Personally, I believe my approach would also make much more sense in the production of a real game.

- Balls parameters are configurable through Scriptable Objects of type BallConfig. These object identify all of the parameters of the possible balls, including their size which also requires a sprite.

- Depending on the speed the color of balls trail will change to the one set in the config so Players could identify it more easier.

- The project uses Unity's new Input System, because it's cool and all of that.

    There's a different approach possible: to move the player by the distance that finger moves. Needs some changes in PlayerAvatar class. 
    
    But the implemented approach where the platforms just follows the players just feels better to me personally.

- Also I chose to use URP because I believe it's the default future of Unity.

- Changing the colour of the ball is achieved by using a Unity's ShaderLab shader which implements palette swapping. 

    It takes a gray-scale sprite of the ball and a gradient texture and then maps one on another. This should help with scalability of the project thus allowing creating many more balls without the need to create a colored variation for each of them. Works well for pixel art games!

## Ways to improve the implementation

- Separacte the trail colors from the BallConfig into a different speed types assets (e.g. Fast, Slow, etc) where you would store a speed and a matching color for this speed to show in the game. So that when you design more balls you don't have to copy paste speed and trail color values, but you could just pick an asset that would store all of the data about speed things. Kind of like it's done with BallSize.
- ???

## Ways to improve the game design

- ???

## TODO

- [ ] Test controls on mobile (kind of important)
- [ ] Implement the realtime multiplayer