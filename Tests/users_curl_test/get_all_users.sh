#!/bin/bash

#Get all users 

test_results="get_all_users_results.txt"

curl --include --request GET "https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.4/user/" --header "accept: application/json" --header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9">> $test_results

#>>puts the output at the end of the file

#Expected output: response 200 OK