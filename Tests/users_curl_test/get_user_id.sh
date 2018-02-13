#!/bin/bash

#Get all users 

test_results="get_user_id_results.txt"

curl --include --request GET "https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.4/user/1" --header "accept: application/json">>$test_results

#>>puts the output at the end of the file

#Expected output: response 200 OK