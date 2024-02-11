from initial.Connection import *
from Routes import Routes
import random


con=Connection.getConnect()
c=Routes(1, "1", 2, 3, 2.34, "sh", "ss", "Fair")
s=[]
s.append(1)
s.append(2)
s.append(3)
a=random.choice(s)
c.getAllroads(con, a)
c.insertBadRoad(con)