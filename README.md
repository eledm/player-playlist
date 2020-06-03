## PLAYLIST PLAYER

The Playlist Player script allows the user to play music in Unity as if it was a regular music player platform. The user will need to put the audio clips into the Audio folder, and Unity will recognize them and automatically import them into the playlist. 

Then, the player can control the music with the User Interface, playing it, stopping it, changing the volume, and selecting if the music order will be random, or if they want to loop any song.

The user will just follow the user interface and won't need to interact with the Unity project.

### Functions

1. Play
2. Stop
3. Pause
4. Unpause
5. Shuffle
6. Loop
7. SetVolume

### What can it do?

- Load the audio files from a folder using ResourcesLoad()
- You will be able to:
    - Play, pause and stop
    - Choose the type of transition between songs: With a fade or without a fade
    - Loop a song
    - Shuffle the playlist
    - Skip to the next or previous song
    - Control the volume
    - Know what song is playing at the moment via the "Now playing:" text block
