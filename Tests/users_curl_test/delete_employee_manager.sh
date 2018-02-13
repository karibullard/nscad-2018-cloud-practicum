#!/bin/bash

#Delete employee under manager by using manager id and employee id

test_results="delete_employee_manager_results.txt"

curl --include --request DELETE "https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.4/user/1/employees" --header "accept: application/json" --header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" --header "Content-Type: text/plain" --header "\"81484e47aa46445ab8ab73f01be9dcc1\"">>$test_results

#>>puts the output at the end of the file

#Expected output: response 200 OK