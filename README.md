MonoGame Development Tutorial
=============================

A simple side-scroller style game written from Tara Walker's MonoGame
development tutorial located at:
http://blogs.msdn.com/b/tarawalker/archive/2012/12/04/windows-8-game-development-using-c-xna-and-monogame-3-0-building-a-shooter-game-walkthrough-part-1-overview-installation-monogame-3-0-project-creation.aspx

Unfortunately, the tutorial is not complete so it doesn't go any further than collision detection between the enemy ships and the player ship.  I went a little further on my own and implemented ship explosions, the start and game over screens, shooting, and the collision detection for lasers and enemy ships.

I intend to continue work on this for my own educational benefit.  All
graphical and sound assets were provided by the above blog.

## Getting started

Installation instructions for Linux:

### Install Mono

Follow instructions here: http://www.mono-project.com/docs/getting-started/install/linux/#debian-ubuntu-and-derivatives

Or...

```bash
$ sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
$ echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
$ echo "deb http://download.mono-project.com/repo/debian wheezy-apache24-compat main" | sudo tee -a /etc/apt/sources.list.d/mono-xamarin.list
$ sudo apt-get update
$ sudo apt-get install mono-devel mono-complete referenceassemblies-pcl ca-certificates-mono
```

### Install MonoDevelop

http://www.monodevelop.com/download/linux/

```bash
$ sudo apt-get install monodevelop monodevelop-nunit monodevelop-versioncontrol
```
### Install MonoGame

Follow instructions here: http://www.monogame.net/documentation/?page=Setting_Up_MonoGame_Linux


## Game Controls

Keyboard:
* Start - Enter
* Movement - Arrow keys
* Shoot - Space
* Quit - Escape

Xbox360 Controller:
* Start - Start button
* Movement - Left analog
* Shoot - A button
* Quit - Back button
