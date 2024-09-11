# Map Accessibility Project Unity Game/App

This project is a pilot test for Map Accessibility for legally blind individuals. The goal of this project is to show that navigation and exploration of a virtual environment is possible through the use of directional audio. This project has been built using a combination of Unity (for the building of the virtual environments) and WWise (for the processing of the directional audio)

## Branching Strategy

* `release`: Main branch for "finished" project
* `dev`: Main branch from which development branches will branch
* `feature/<name>`: Branches for implementing/experimenting with features

**Wwise and Unity**

# **IT'S PRONOUNCED "WISE", NOT "W" + "WISE".**

An integral part of the sound design process for this project utilizes AudioKinetic Wwise. This document will guide you through the installation process and also try to provide some troubleshooting assistance for common issues you may run into.

**Installation-**

My biggest recommendation would be to take an hour to watch and follow along with this video from Michael Wagner and skip my way of explaining:

[Game Audio with Unity and Wwise Part 1: Intro and Installation](https://www.youtube.com/watch?v=OchYfH0wb0U&list=PLzlEBXWjqM97U5rHMERc82sTXRBoSB_Fu)

[![Video titled: Game Audio with Unity and Wwise Part 1: Intro and Installation](RackMultipart20230416-1-o07orl_html_5dafa8c97e03c8dd.jpg)](https://www.youtube.com/watch?v=OchYfH0wb0U&list=PLzlEBXWjqM97U5rHMERc82sTXRBoSB_Fu)

It may be a bit long, and not super interesting, however, it will pay for itself many times over as you begin to setup and use Wwise.

Outside of this video, I will give you the quick and dirty on how to set it up. First thing's first, you will need and AudioKinetic account. The link to create one is: [https://www.audiokinetic.com/en/sign-up/](https://www.audiokinetic.com/en/sign-up/)

With your account created, you will need to download the Wwise launcher. I would think that whatever the latest version of the launcher is should suffice, but just in case, the project is currently running on version **2022.2.2.2282.**

The link to download the newest version of Wwise is: [https://www.audiokinetic.com/en/download](https://www.audiokinetic.com/en/download)

The link to download previous versions of Wwise is: [https://www.audiokinetic.com/en/customers/downloads/previous/](https://www.audiokinetic.com/en/customers/downloads/previous/)

When you have the launcher installed, you can then proceed to install Wwise through the launcher. The following screenshot shows what it looks like when Wwise is already installed, however the option should still be in the same location in the launcher. Currently, the project is using version **2022.1.1.8100** of Wwise, and I would recommend installing this version as well.



The first prompt you will get will ask for the packages you wish to install, and the deployment platforms you wish to have support for. You will pick " **Select All**" for **both** options and leave the target directory unchanged. You will notice that some options you click will have a "-" in the box. This is because there are some platforms that will not be selected, as they require paid licenses to deploy on. We do not care about these platforms.

After clicking "Next", you will be shown a huge list of plugins you can install. There are a number that are checked by default. You can leave everything in this list unchanged, and click "Next", as there is nothing more than what comes with the default plugins that is needed.

With the plugins taken care of, Wwise should have everything it needs to install.

Assuming you have Unity already installed and setup on your machine, Wwise will automatically detect all your projects, and show them in the "Unity" tab of the launcher.


You will notice in the above picture that one of my projects has the option to "Open in Wwise", and the other one does not. This is because Wwise has not been **integrated** into that second project. This should be automatically handled when you pull the project from the GitHub repository, however, if it is not, simply click the button to integrate.

**Using Wwise -**

Wwise is a fairly intuitive program to use, however the skill ceiling is IMMENSE. The spring 2023 team is by no means an audio engineering powerhouse, so we used the basics. If you are so inclined, the potential of this program is very powerful. I will get you going with a very rudimentary rundown, nothing fancy.

When you first open Wwise, it can be rather overwhelming, and seem very complex. You will soon see that there are really only three buttons you ever care about; Audio, Events, and SoundBanks. I will walk you through each of these areas breifly.


**Audio -**

The audio section here is where you store the actual audio files for your project. In this tab, you will see a lot of different hierarchies, the only one we care about is the "Actor-Mixer Hierarchy".

Within this Hierarchy, you will see a "Default Work Unit". This is where all the audio files you use will go. In order to add a file you want to use as a sound, right click on the Work Unit, and then select "Import Audio Files"

A couple things to note about files you can use here is that Wwise will not accept 8-bit sounds, and they need to be .wav format. Converting files from .aaic or .mp4 is very easy using online tools. When importing files, you can drag and drop from your file explorer, or import them through Wwise. Another thing to note here, is that you want to import them as SFX, which is the default setting.

You should now have a list of all the files you imported inside the Default Work Unit.


Here is where you can really take off with Wwise. When you click on a sound file that is inside the Work Unit, it will show you all kinds of options and ways to alter the sound. Many of these are interesting, however, there is one that is KEY to this project, and must be done on **ALL** sounds. That is making sure that the sound has 3D Spatialization, with both position and orientation.

Another key aspect to consider, in this same "Positioning" tab, is the attenuation of a sound. Attenuation adjusts the volume of the sound, relative to the position of the player and that object. This will be used for the vast majority of objects, and is very important, although I cannot say it will be a MUST for ALL objects. To access the attenuation settings, click the button with arrows that look like " **\> \>**". This will give you a list of all other attenuation settings that are elsewhere in the project, or allow you to make a new setting.

When you select either a previous attenuation, or create a new one, you will then need to click the button to the right that looks like a square with an arrow coming out of it in order to have it open a pop-up for you. You may also click the new "ATTENUATION" button towards the bottom, to switch to a mixer tab.

This is another screen where there is a LOT you can do. The main setting to be played with here is the red "Distance // Volume" tab. You will see a big red line graph that shows the relative volume to the player, based on how far away they are. If you right click on the line itself, you can change the shape of the curve, depending on how you want the sound to react to the players distance. Another important setting here to keep in mind is the Max Distance setting. Setting this too low, the player may not have enough time to react. Setting it too high, and the player may become inundated with sounds and cause erroneous behavior.

A couple of things that are not always needed, but I found I commonly used were making sounds loop and trimming the start/end times of sounds. These are easily changed in the General Settings tab of an Audio file. Loop is a simple check box, and the trimming can be done with a simple slider.

There is much, much more you can do with Wwise here, but this should be the minimum you'll need to know to get off the ground. Now to Events!

**Events -**

"Events" in Wwise are what Unity will actually play. From a single Audio file, you can make several different events. That is, you can trim an Audio file, make an Event, go back to the Audio file and adjust it, and make another Event. You don't need separate Audio files, just separate Events. Then you call the event you want from Unity. Thus far, we only have one Event per Audio file, and it is very straightforward. Creating Events is very easy, and before doing so, I would HIGHLY recommend making sure your Audio file name is what you want it to be. If you change the Audio file name later, it can become confusing to know what event is related to which Audio file. That being said, to create and Event, right click on the Audio file, go to "Create New Event", and select "Play". There are indeed several event types, we only deal with Play for the sake of simplicity.

Now, in the "Events" tab, you should see an event called "Play\__xyz"_. There isn't anything more to do here, although we will return to this tab when filling our SoundBank(s).

**SoundBanks -**

The way Wwise groups Events together is with SoundBanks. With large games, sound files can eat up a lot of memory, and this helps mitigate that issue. In Unity, you can load and unload SoundBanks from memory, so that not all sounds from the game are being loaded at once. At this point in our project, we only have one SoundBank, although if the number of sounds continues to grow, more may be needed in order to improve game performance. In order to creater a SoundBank, right click on the "Default Work Unit", click "New Child", and then select "New SoundBank".

To fill the SoundBank, we need to add Events to it. By clicking on the SoundBank in the Hierarchy, we will get a list of all events currently in the SoundBank. With this list open, click back to the Events tab on the left. Then drag and drop the Event(s) into the list.

The final step before you can go muck around in Unity with your new/altered sounds, is generating the SoundBanks. To do this, you will need to change your Wwise Layout. By default, Wwise will be in Designer Layout. We want to switch to SoundBank Layout. This is done quickly with the F7 key, or by using the Layout tab at the top, and selecting "SoundBank".

When in SoundBank Layout, you can generate the SoundBanks using the buttons in the upper right corner. Make sure to have the proper Platforms and Languages checkout before doing so. I would recommend using the Generate Checked, rather than Generate All when using multiple SoundBanks to make error handling more understandable.

You will get a log that shows errors/warnings, and if things go successfully, you can now add this SoundBank to your Unity project! (You will always get warnings. Yellow is ok, red is bad)

**Unity -**

So now we can go into what needs to happen in Unity, to play a sound. This is usually the worst bit. Lots of things can go wrong here, which I will try to go into a bit later. There are two maing things to aware of in the Unity project, pertaining to Wwise. There is a WwiseGlobal object in the hierarchy, and a "Wwise Picker" next to your console/debug log at the bottom.



The Wwise Picker will contain all the latest and greatest from Wwise, and allow you to Generate the SoundBanks in Unity. Yes, you need to generate them in Wwise and then in Unity as well.

From this Wwise Picker list, you need to add the **SoundBank** to the WwiseGlobal object. You will then see an "AkBank" component appear in the inspector tab to the right. You will need to make sure the AkBank loads on **Awake**. If the SoundBank and the Event load at the same time, you will get an error. The SoundBank needs to be loaded before the Event.


With the SoundBank in place, any object you want to make sound, will need to have two components to them. One of which MUST be the "AkGameObj" component. The other one can be a component provided by AudioKinetic, or one that is coded by you. Generally speaking, the easier way to do it is with AkAmbient. Using AkAmbient as an example, you need to set the Event to trigger on "Start", and also select which Event you want to play.


This setup, as shown above, works well for sounds you want to have happening constantly, from the start of the game/level. For more interactive Events, like making a sounds after a collision, scripts are better utilized, which may be a later section. Otherwise, this is the barebones of getting started in Unity, to make some N O I S E.

**Common Issues and How To Resolve Them (or more often, NOT) -**

The Wwise documentation, in my opinion, leaves a lot to be desired. The forums are bare, and all too often you can find the question you have has been asked, with no replies to it. That being said, I will try to go through the issues I have experienced and what I did about them .

**Wwise Picker Connection Refused-**

In this scenario, the Wwise Picker will not display anything. Refreshing the project does nothing, and no sounds will be available.

Solution: I have no clue. I do not know what causes this. Sometimes it will randomly fix itself. I usually try generating the SoundBanks in the Wwise project again. If your Wwise project is open, try closing it. Also just try restarting the Unity project.

**I/O Error-**

From time to time, everything will generate just fine, it works on everyone else's machine, and then you will hit "Play" in Unity, and get no sound and a big red I/O Error from Wwise.

Solution: There is none. I hate this one so much. I have no idea why this happens. Changing your audio output does nothing. Restarting often does nothing. There are no explanations that I have found for this. It will work again eventually, but if you need to try something right then, I can only suggest switching machines.

**Buffer Error-**

Sometimes a particular sound won't play and you will get a Buffer Error from Wwise.

Solution: This is most often because a sound is set to start at Awake, or a SoundBank is set to start at Start. Double check all your Events load after the SoundBank. Sometimes I think this can be caused by not having a SoundBank in the WwiseGlobal object at all.

**Missing Raw Audio Files-**

This only happened to me once, but there was a time where I pushed a change to GitHub, then pulled from a different machine and none of the new sounds I added to the Wwise project were there. All the Audio and Event files were there, but they didn't work.

Solution: The repository file directory path: ...\MapAccessibilityProjectApp\MapAccessibilityProjectApp\_WwiseProject\.cache\Windows\SFX contains the actual raw files that you imported. In the above scenario, these did not make it to GitHub, but the rest of the Wwise data did. In theory, reimporting all the files should work. Tbh, I did the barbaric approach and copied the files straight to the above folder, and pushed again. Maybe not the best route to take, but it worked...

**General Troubleshooting Advice -**

If you can't find something in Unity that is in Wwise, generate the SoundBanks again from Wwise. Generate them again in Unity.

Save and close the Wwise project when working in Unity. Wwise will sometimes put a lock on files while the editor is open, and will not allow Unity to use them until you close Wwise.

Good Luck :)

* 
## Developer Installation Instructions (Overview)
This project uses Unity 2021.3.17f1 and WWise 2022.1.1.8100.

1. Install [Unity 2021.3.17f1](https://unity.com/releases/editor/whats-new/2021.3.17) (this can be done using the link provided or using Unity Hub if it is already installed)
2. Install the [AudioKinetic (AK) Launcher](https://www.audiokinetic.com/en/download)
3. Install WWise 2022.1.1.8100 from the AK Launcher
4. Clone the [Github Repository](https://github.com/fmillion-mnsu/MapAccessibilityProjectApp)
5. Open the project using Unity Hub

## Developer Installation Instructions (Detailed)
#### Installing Unity Hub
If you already have the Unity Hub application installed, skip to ["Installing Unity"](#installing-unity)
1. Go to [this link](https://unity.com/download) and click "Download"
2. On that page, locate the "Download for Windows" button. Once the file is done downloading, open it.
3. Follow the installation instructions on the window which opens.
4. Once the installation completes, open Unity Hub. Sign in with your Unity account.
5. Unity Hub will prompt you to install the Unity Editor. Select "Skip install".
    
#### Installing Unity
Make sure that some version of Visual Studio or VS Code or some other code editor of choice for C#.
1. Once the Unity Hub is installed, go to [this link](https://unity.com/releases/editor/whats-new/2021.3.17) and click the "Install this version with Unity Hub" hyperlink.
2. On the "Add Modules" screen, no boxes are required to be checked, so you may click "continue."
3. Agree to the terms and conditions, and then wait for the unity version to install.
    (While the Unity Editor installs, feel free to begin [Installing the AudioKinetic Launcher](#installing-the-audiokinetic-ak-launcher))
#### Installing the AudioKinetic (AK) Launcher
If you already have the AK Launcher installed, skip to ["Installing WWise"](#installing-wwise)
1. Go to [this link](https://www.audiokinetic.com/en/download)
2. Sign into AudioKinetic on their website if you haven't already.
3. The site should download the WWise Launcher Installer, once it is done installing, open it.
4. Follow the installer instructions when prompted.
5. Once the WWise Launcher is open, sign in.
6. Begin installing WWise

#### Installing WWise
1. Go to the ""WWise" tab in the AudioKinetic Launcher.
2. Click the "Install..." button under "INSTALL NEW VERSION"
3. Under "Packages," make sure check "Authoring," "SDK C++," and "Offline Documentation"
4. Under "Development Platforms" make sure to check "Microsoft"
5. On the "Plug-ins" screen, make sure only the "Mastering Suite" is checked.
6. Wait for the installation to complete

#### Clone the GitHub repository
Make sure Git Bash as well as Unity 2021.3.17f1 and WWise 2022.1.1.8100 are installed before this section. If Git Bash is not installed, install it [here](https://gitforwindows.org/)
1. Find a place on your laptop to clone the repository. Copy the file path to that place.
2. Open the Command Prompt and type in `cd [REPOSITORY_FILE_PATH]` where `[REPOSITORY_FILE_PATH]` is the file path copied in the previous step
3. Type `git clone https://github.com/fmillion-mnsu/MapAccessibilityProjectApp` to clone the repository into your files.
4. In Unity Hub, open the project by clicking on the arrow on the "Open" button in the "Projects" tab and clicking "Add project from disk."
5. Navigate to the folder where the repository was cloned and click "Add Project"
    Make sure the folder you are in is the "MapAccessibilityProjectApp" folder
6. Open the project in the list on the Unity Hub Projects page.

Test