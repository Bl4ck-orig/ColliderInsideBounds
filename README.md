# ColliderInsideBounds

Used for getting all colliders inside of bounds. Unity is used in the code.

The start situation are four points as shown. For it to work assign all points in a way so that all points can be connected either clockwise or counter clockwise!

![Setup](https://user-images.githubusercontent.com/38137603/178111287-1ac87d95-466b-429b-8170-c8503923d123.png)

The algorithm does the following:

1. Get all colliders within the range of xMin/yMin and xMax/yMax:

![1](https://user-images.githubusercontent.com/38137603/178111332-5f4f9179-37c9-4815-8477-c343cd172dbe.png)

2. Remove all colliders outside of the of the bounds.

![2](https://user-images.githubusercontent.com/38137603/178111354-94cb1fc7-4d45-4bff-aa06-f82b215ecdf3.png)

3. Handle special case (TODO)

![3](https://user-images.githubusercontent.com/38137603/178111366-99566f36-5533-439a-8cf9-e82fc04ef5ba.png)
