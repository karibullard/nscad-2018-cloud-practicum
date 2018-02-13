#!/bin/bash

test_results="results.txt"
server="https://virtserver.swaggerhub.com/kari_bullard/Cloud-Practicum/1.0.2/user/2"

curl -i $server >> $test_results #the -i flag includes headers, >> puts the output at the end of the file

#next steps: how do we verify that the results are expected?