# Code Challenge/Take-home Assignment
## Requirements:

* Both of the following applications should be containerized using docker

* The completed assignment should be returned to us in a Git based repository, ideally Github, but Gitlab or other repositories that we can view are acceptable

* Please provide your solution within 7 days

## Assignment:

* Write a web application in your language of choice that returns the current date/time in JSON

* Write a simple test application that will query this “API” X times per second and record success/failure/TTLB (Time to last byte)

## How to Run:

* Make sure to have docker and docker compose set up correctly on your local machine

* Make sure that docker is up and running

* If running the application for the first time, cd into the root of the project, you can run the following command `docker-compose up`

* If images already exist on the local machine / server, , cd into the root of the project, you will have to rebuild with `docker-compose build`, then `docker-compose up`

* This will spin up two docker containers each hosting the its own API on two separate ports

## Testing Application API Endpoints

* GET http://localhost:81/api/test/{runs} where "runs" is the number of X concurrent calls to the TimeToGetAWatch API. Will return an array with of result objects with success, failure, and TTLB info

* GET http://localhost:81/api/test/{iterations}/{runs} where "iterations" is the number of times the number of "runs" is passed through. This endpoint calls the TimeToGetAWatch API X times, sleeps for 1000ms and then continues for the number of iterations passed through. Returns the same as the above.

* GET http://localhost:81/api/test/summary/{iterations}/{runs} This is the same as the above endpoint, but returns a summary object of all test results