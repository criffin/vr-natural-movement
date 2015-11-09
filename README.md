# VR Natural Movement

Collection of movement algorithms to be used in VR apps implemented using Unity3D.

[![Alt Demo video](https://i.vimeocdn.com/video/542467007_640.webp)](https://vimeo.com/144592305)

Mouse + keyboard or even gamepad dont work well when it comes to movement in virtual reality environments. This task requires another kind of hardware. While new hardware is not yet available we would like to encourage developers to use maximum of existing VR devices and other available gadgets in order to create better immersive experiences.
You may call it a hack because some of the provided algorithms use hardware not the way it was intended.

This project is based on [Oculus Natural Movement](https://github.com/taphos/oculus-natural-movement) by Filipp Keks. Current project is work in progress and accepts pull requests.

 * Supported Unity3D version 5.2.2f1
 * Supported Oculus SDK version 0.8

## Currently available algorithms

### Flying

VR device head turn angle is used as a camera rotation velocity. This allows player to turn any direction with only a few degrees of head turn, gives natural feeling of motion and reduces motion sickness as camera is rotated together with players head. Be careful to to use large acceleration when changing flying speed as it contributes to motion sickness.
See example in scene [FlyingExample](blob/master/Assets/VRNaturalMovement/Scenes/FlyingExample.unity)

### Hand movement

Mobile device accelerometer is used to track the hand position. Accelerometer data together with screen taps are sent from mobile device to PC using local WiFi connection. This is a kind of game remote control optimized to be used with one hand. Use it to control gun, sword or a robot hand movement.

 * Compile and run scene [GyroController](blob/master/Assets/GyroController/Scenes/GyroControllerDevice.unity) for PC
 * Disable virtual reality support in player settings->other
 * Compile and run scene [GyroControllerDevice](blob/master/Assets/GyroController/Scenes/GyroControllerDevice.unity) for your mobile device platform 
 * Create an Ad-Hoc WiFi hotspot and connect devices (Usually works better then WiFi router)
 * Enter PCs local ip address in the text field of mobile device
 * Hand controlled gun should appear on PC screen

### Walking

VR device head up-down motion is used to control the forward movement. This basically works as step detection. Player may make a few steps around  while inside VR tracker and device cord range and if he needs to move further just make steps in spot.
See example in scene [WalkingExample](blob/master/Assets/VRNaturalMovement/Scenes/WalkingExample.unity)

### Running

Same as walking but player have to jog in spot instead of just making steps. Be careful not to make your player jog too fast as it is easy to loose balance and fall. VR headset shake while joggings also benefits to motion sickness.
See example in scene [WalkingExample](blob/master/Assets/VRNaturalMovement/Scenes/WalkingExample.unity)

### Jumping

Sudden changes of head height used to detect jumps. Be careful as it is easy to loose balance and fall. VR headset shake while jumping also benefits to motion sickness.
See example in scene [WalkingExample](blob/master/Assets/VRNaturalMovement/Scenes/WalkingExample.unity)

### Crouching

Same as walking and jumping the head height is used to detect the crouching pose and forward motion. Virtual avatar collider size is changed so it is possible to crouch under ingame obstacles.
See example in scene [WalkingExample](blob/master/Assets/VRNaturalMovement/Scenes/WalkingExample.unity)

