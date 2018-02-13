#!/bin/bash

#Change user name by using id

test_results="put_user_results.txt"

curl --include --request PUT "https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.4/user/1" --header "accept: application/json" --header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" --header "Content-Type: application/json" -d "{ \"id\": \"72AD9DBC60AE485782D43A1AE09279A4\", \"firstName\": \"John\", \"lastName\": \"Doe\", \"email\": \"rick.sanchez@example.org\", \"phone\": \"989-999-2229\", \"type\": \"manager\", \"manager\": \"FD48B5381FCF4F3686C4DE095E3FEB3A\", \"employees\": [ \"72AD9DBC60AE485782D43A1AE09279A4\" ], \"workflow\": \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\", \"tasks\": [ \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\", \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\" ]}">>$test_results

#>>puts the output at the end of the file

#Expected output: response 201 created