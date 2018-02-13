#!/bin/bash

#POST complete task to user by using id

test_results="results.txt"

curl --include --request POST "https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.4/user/1/task" --header "accept: application/json" --header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" --header "Content-Type: text/plain" --data "\"81484e47aa46445ab8ab73f01be9dcc1\"">>$test_results

#>>puts the output at the end of the file

#Expected output: Response 201 created.

# archive test results
timestamp=$(date +%s)
mv ./results.txt ./results/${timestamp}_task_test_results.txt