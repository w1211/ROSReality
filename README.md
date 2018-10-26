# ROSReality
Repo for the Unity / Windows sides of things

If you're pushing to this Repo, make sure your git has lfs installed (https://git-lfs.github.com/)

Notes:
1. To make the two connect youll need RosBridge 
  - Install on VM with "sudo apt-get install ros-kinetic-rosbridge-suite"
  - Make sure ROS can find it by running  "source /opt/ros/kinetic/setup.bash"
  - Run with "roslaunch rosbridge_server rosbridge_websocket.launch"
2. For simulating the turtlebot / making the Unity turtle move you'll needto install the turtlebot sim (instructions for running simulation here - "http://emanual.robotis.com/docs/en/platform/turtlebot3/simulation/#turtlebot3-simulation-using-fake-node"

3. Setup in Unity:
 - The Mixed reality toolkit should be in the project already, but if its not then youll need to install that too (think this is in the Hololens101 tutorials
 - If it's not laoded already, load the main scene by clicking on it in the Assets folder IN Unity
 - In the hiearchy, click on TurtleParent, then click on "inspector" on the right hand side
 - Replace the ROS Bridge IP with the ROS core IP
 
4. If everything is running correctly on the ROS side, you should be able to hit the play on the top of the screen and the turtle on the screen will start moving around!
