Jnr_BackEnd_Technical_Assesment: Janco Steenkamp

Installation Instructions:

1. 	Clone the repo (https://github.com/jancosteen/Jnr_BackEnd_Specno.git)
2. 	Open Reddit_Api.sln
3. 	Right click the solution in the solution explorer -> click Restore Nuget Packages
4. 	In the nuget Package Manager Console, run "Update-Database"
5.	Set Reddit_Api as the Startup Project
6. 	Build the project ("CTRL+SHIFT+B")
7. 	Run the project (quick run by pressing "CTRL + F5")
8. 	Open Postman
9. 	Import the "Reddit_Api.postman_collection.json"
10. 	Register a new user
	- The user details have already been added to the body
11. Authenticate the user
	- The user details have already been added to the body
12.	You will need to use the accessToken for all the requests
	- Copy the accessToken (without "")
13. Set the postman requests authorization as "Bearer" and paste the access token
14. Test the requests provided (The DB will be empty, you need to start by creating a post)

Postman (All of the postman requests have valid bodies, feel free to test incorrect bodies)

Authentication:
1. Register User (POST)
https://localhost:5001/api/authentication
2. Authenticate User (POST) (You will need to authenticate to get the accessToken required for the requests)
https://localhost:5001/api/authentication/login
3. Get User ID (GET)
https://localhost:5001/api/authentication/userName/JanDoe

Posts: 
1. Create Post (POST)
https://localhost:5001/api/posts/<Add the userId here>
2. Get All Posts (GET)
https://localhost:5001/api/posts/
3. Get Post (GET)
https://localhost:5001/api/posts/postId/<postId here>
4. Update Post (PUT)
https://localhost:5001/api/posts/postId/<postId here>
5. Delete Post (DELETE)
https://localhost:5001/api/posts/postId/<postId here>
6. Upvote Post (PUT)
https://localhost:5001/api/posts/upvotePost/userId/<userId here>/postId/<postId here>
7. Downvote Post (PUT)
https://localhost:5001/api/posts/downVotePost/userId/<userId here>/postId/<postId here>
8. Get Posts by Username (GET)
https://localhost:5001/api/posts/JanDoe
9. Get Posts that a user has voted on (GET)
https://localhost:5001/api/posts/postsVoted/<userId here>
	
Comments:
1. Create Comment (POST)
https://localhost:5001/api/comments/userId/<userId here>/postId/<postId here>
2. Get All Comments (GET)
https://localhost:5001/api/comments/
3. Get Comment (GET)
https://localhost:5001/api/comments/<commentId here>
4. Update Comment (PUT)
https://localhost:5001/api/comments/<commentId here>
5. Delete Comment (DELETE)
https://localhost:5001/api/comments/<commentId here>
6. Upvote Comment (PUT)
https://localhost:5001/api/comments/upvoteComment/userId/<userId here>/commentId/<commentId here>
7. Downvote Comment (PUT)
https://localhost:5001/api/comments/downvoteComment/userId/<userId here>/commentId/<commentId here>
