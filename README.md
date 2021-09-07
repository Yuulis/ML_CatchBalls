# ML_CatchBalls
Learning catching balls by [ML-Agents](https://github.com/Unity-Technologies/ml-agents).

## **Basic**
## Environment
![Environment](https://user-images.githubusercontent.com/79734873/132329233-3cc79605-9425-455d-99ef-589f8e3de0b4.png)
### Training Area
1. One Training is 30 seconds. It is default, and can change freely.
2. There are **TWO** trainingArea in `Basic` Environment:  
    Wall height is 20  
    Wall height is 40 (Image)
### Agent
1. Agent can move right or left on Floor.
2. Agent learn to catch GreenBall and to avoid BlueBall.

### Balls
1. Balls fall to a random position from the top of the stage, and are destroyed when they hit Agent or Floor.
2. The appearance rate and interval of green and blue can be changed freely.  
**Default** (`Basic_WallHigh`):  
    Appearance rate of BlueBall: 40%  
    Interval: 0.75s

## Reward
1. Catching GreenBall: **+1.0**.
2. Catching BlueBall: **-1.0**.
3. Missing GreenBall: **-1.0 / (Number of green balls that fall in one training)**.