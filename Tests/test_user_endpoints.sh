#!/bin/bash

# results file
# >> puts the output at the end of the file
test_results="results.txt"

# testing server 
swagger_server="https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.5/"

# POST task to user by using id
curl --include --request POST \
	"${swagger_server}user/1/task" \
	--header "accept: application/json" \
	--header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" \
	--header "Content-Type: text/plain" \
	--data "\"81484e47aa46445ab8ab73f01be9dcc1\"" \
	>> ${test_results}

# Expected output: Response 201 created.

# Add Employee to manager using manager id
curl --include --request POST \
	"${swagger_server}user/1/employees" \
	--header "accept: application/json" \
	--header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" \
	--header "Content-Type: text/plain" \
	--data "\"81484e47aa46445ab8ab73f01be9dcc1\"" \
	>> ${test_results}

# Expected output: response 201 created

#Delete complete task of user by using id

curl --include --request DELETE \
	"${swagger_server}user/1/task" \
	--header "accept: application/json" \
	--header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" \
	--header "Content-Type: text/plain" \
	--data "\"81484e47aa46445ab8ab73f01be9dcc1\"" \
	>> ${test_results}

#Expected output: response 200 OK

#Delete employee under manager by using manager id and employee id

curl --include --request DELETE \
	"${swagger_server}user/1/employees" \
	--header "accept: application/json" \
	--header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" \
	--header "Content-Type: text/plain" \
	--header "\"81484e47aa46445ab8ab73f01be9dcc1\"" \
	>> ${test_results}

#Expected output: response 200 OK

#DELETE user by using id

curl --include --request DELETE \
	"${swagger_server}user/1" \
	--header "accept: application/json" \
	--header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" \
	--header "Content-Type: application/json" \
	--data "{ \"id\": \"72AD9DBC60AE485782D43A1AE09279A4\", \"firstName\": \"Rick\", \"lastName\": \"Sanchez\", \"email\": \"rick.sanchez@example.org\", \"phone\": \"989-999-2229\", \"type\": \"manager\", \"manager\": \"FD48B5381FCF4F3686C4DE095E3FEB3A\", \"employees\": [ \"72AD9DBC60AE485782D43A1AE09279A4\" ], \"workflow\": \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\", \"tasks\": [ \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\", \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\" ]}" \
	>> ${test_results}

#Expected output: response 200 OK

#Get user by id

curl --include --insecure --header "accept: application/json" --header "access-token: a thing" "${swagger_server}user/1" >> ${test_results}
#Expected output: response 200 OK

# post a new user test script

curl --include --request POST --header "accept: application/json" --header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" --header "Content-Type: application/json" "${swagger_server}user" --data "{ \"id\": \"12AD9DBC60AE485782D43A1AE09279A4\", \"firstName\": \"Morty\", \"lastName\": \"Smith\", \"email\": \"msmith@example.org\", \"phone\": \"999-989-9229\", \"type\": \"employee\"}" >> ${test_results}

#Expected output: response 201 created

#Change user name by using id

curl --include --request PUT "${swagger_server}user/1" --header "accept: application/json" --header "access-token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" --header "Content-Type: application/json" -d "{ \"id\": \"72AD9DBC60AE485782D43A1AE09279A4\", \"firstName\": \"John\", \"lastName\": \"Doe\", \"email\": \"rick.sanchez@example.org\", \"phone\": \"989-999-2229\", \"type\": \"manager\", \"manager\": \"FD48B5381FCF4F3686C4DE095E3FEB3A\", \"employees\": [ \"72AD9DBC60AE485782D43A1AE09279A4\" ], \"workflow\": \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\", \"tasks\": [ \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\", \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\" ]}" >> ${test_results}
#Expected output: response 201 created

# archive test results
timestamp=$(date +%s)
mv ./results.txt ./results/${timestamp}_user_test_results.txt