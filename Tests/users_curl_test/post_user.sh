#!/bin/bash

# post a new user test script

test_results="post_user_results.txt"

curl --include --request POST "https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.4/user" --header "accept: application/json" --header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" --header "Content-Type: application/json" --data "{ \"id\": \"12AD9DBC60AE485782D43A1AE09279A4\", \"firstName\": \"Morty\", \"lastName\": \"Smith\", \"email\": \"msmith@example.org\", \"phone\": \"999-989-9229\", \"type\": \"employee\"}">> $test_results

#>> puts the output at the end of the file

#Expected output: response 201 created