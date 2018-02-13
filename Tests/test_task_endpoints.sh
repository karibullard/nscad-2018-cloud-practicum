#!/bin/bash

# set output file for test results and clear/init the file
test_results="results.txt"
> test_results

# set base server path for swagger mock api
swagger_server="https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.4/"

# set base server path for dev api
development_server="dev-server-someday"

# endpoint name
task_endpoint="task/"

# task endpoint calls
# --include outputs both headers and body of response. Some of the calls from 

# POST task/

echo "Test POST task/\n" >> ${test_results}

json_payload_post="{  \"id\": \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\",\"name\": \"Setup Slack\",  \"description\": \"Add user to UST Slack account.\", \"employeeInstructions\": \"ECCD3A6E-D4C5-4D2D-A28C-9CDD28F6417E.md\",  \"managerInstructions\": \"ECCD3A6E-D4C5-4D2D-A28C-9CDD28F6417E.md\" }"

curl --request POST --include --insecure --header "accept: application/json" --header "access-token: a thing"  	\
	 --header "Content-Type: application/json" --data "${json_payload_post}"	\
	 "${swagger_server}${task_endpoint}" 	\
	 >> ${test_results}

# GET task/{id}
echo "Test GET task/id\n" >> ${test_results}

# does GET by default, no need for --request
curl --include --insecure --header "accept: application/json" --header "access-token: a thing"  	\
		"${swagger_server}${task_endpoint}1" 														\
		>> ${test_results}

# curl --include --insecure --header "accept: application/json" --header "access-token: a thing"  	\
# 		"${swagger_server}${task_endpoint}2" 	\
# 		>> ${test_results}

echo "\n" >> ${test_results}

# PUT task/{id}
echo "Test PUT task/id \n" >> ${test_results}

json_payload_put="{  \"id\": \"ECCD3A6ED4C54D2DA28C9CDD28F6417E\", \"name\": \"Setup Slack\",  \"description\": \"Add user to UST Slack account.\", \"employeeInstructions\": \"ECCD3A6E-D4C5-4D2D-A28C-9CDD28F6417E.md\",  \"managerInstructions\": \"ECCD3A6E-D4C5-4D2D-A28C-9CDD28F6417E.md\" }"

task_id="ECCD3A6ED4C54D2DA28C9CDD28F6417E"

curl --request PUT --include --insecure --header "accept: application/json" --header "access-token: a thing"  	\
	 --header "Content-Type: application/json" --data "${json_payload_post}"	\
	 "${swagger_server}${task_endpoint}${task_id}"	\
	 >> ${test_results}

echo "\n" >> ${test_results}

# DELETE task/{id}
echo "Test DELETE task/id \n" >> ${test_results}

curl --request DELETE --include --insecure --header "accept: application/json" --header "access-token: a thing" \
	"${swagger_server}${task_endpoint}${task_id}"	\
	>> ${test_results}

echo "\n" >> ${test_results}

# workflow endpoint calls


# archive test results
timestamp=$(date +%s)
mv ./results.txt ./results/${timestamp}_task_test_results.txt
