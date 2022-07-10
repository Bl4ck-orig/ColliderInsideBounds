# ColliderInsideBounds

Used for getting all colliders inside of bounds. Unity is used in the code.

The start situation are four points as shown. For it to work assign all points in a way so that all points can be connected either clockwise or counter clockwise!

![Setup](https://user-images.githubusercontent.com/38137603/178146203-8eaa5b98-cdd7-4508-b996-f90a8c92b811.png)

The algorithm does the following:

1. Get all colliders within the range of xMin/yMin and xMax/yMax:

![1](https://user-images.githubusercontent.com/38137603/178146209-d1e24983-0d8b-4c9a-bb11-a06cef1ee299.png)

2. Remove all colliders outside of the of the bounds.

![2](https://user-images.githubusercontent.com/38137603/178146216-2ebcb1e2-c297-414b-a8b9-4657db5df884.png)

3. Handle special case (TODO: adding buffers is sufficient)

![3](https://user-images.githubusercontent.com/38137603/178146220-af7b71a8-5f05-427f-ae21-175eee7aa7f5.png)
