#!/bin/bash
# A script to test workflow endpoints
log_date=$(date +"%Y%m%d")log_name="workflow_test_results.log"
logfile=$log_date$log_name
base_path="https://localhost:44335/workflows"
#
# Workflow POST Tests
#
echo "POST TESTS"
echo ""
#
# 201 TEST
#
curlCmd="curl -k -X POST --header 'Content-Type: application/json' --header 'Accept: application/json' -d @'workflows/post/201.json' 'https://localhost:44335/workflows' "
eval $curlCmd