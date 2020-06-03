import matplotlib.pyplot as plt

X = list(map(int,input().split()))
Xmed = list(map(int,input().split()))
S = int(input())

plt.plot(list(range(100)),X, 'ro', markersize=3)
plt.plot(list(range(5,95,10)),Xmed, 'bo')
plt.plot([0,99],[S,S])
#plt.axis([0,100,1e7,2e7])
plt.show()

