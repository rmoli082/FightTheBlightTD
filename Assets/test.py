import numpy as np
grid = np.array([[-1,-1,-1,-1,-1,-1,-1],
                [-1,-1,-1,-1,-1,-1,-1],
                [-1,-1,-1,-1,-1,-1,-1],
                [-1,-1,-1,-1,-1,-1,-1],
                [-1,-1,-1,-1,-1,-1,-1],
                [-1,-1,-1,-1,-1,-1,-1],
                [-1,-1,-1,-1,-1,-1,-1]])

def extendable(x,y,d):
    global grid
    if d==2:
        if grid[x-1][y-1]!=2:
            if (grid[x][y-1]!=1 and grid[x-1][y]!=1):
                return True
            else:
                return False
        else:
            return False
    elif d==1:
        if grid[x-1][y+1]!=1:
            if (grid[x][y-1]!=2 and grid[x-1][y]!=2):
                return True
            else:
                return False
        else:
            return False
    elif d==0:
        if np.sum(grid[1:6,1:6]==0)<=9:
            return True
        else:
            return False

vals = 16
def solve(startPoint, maxPoints,zeros=0,grid=grid):
    x, y = (np.floor_divide(startPoint,5)+1, np.mod(startPoint,5)+1)
    if x>5:
        print(grid[1:6,1:6])
        return
        
    if zeros+vals < 25:
        grid[x][y]=0
        solve(startPoint+1,maxPoints,zeros+1)
        
    grid[x][y]=1
    if extendable(x,y,1):
        solve(startPoint+1, maxPoints, zeros)
    grid[x][y]=2
    if extendable(x,y,2):
        solve(startPoint+1, maxPoints, zeros)

solve(0,24)
