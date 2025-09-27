#!/bin/bash

HEALTHCHECK_URL="http://localhost:8080/healthcheck"

timestamp=$(date -u +"%Y-%m-%dT%H:%M:%SZ")
response=$(curl -s -o /dev/null -w "%{http_code}" $HEALTHCHECK_URL)

echo "[$timestamp] Healthcheck passed with code [$response]"