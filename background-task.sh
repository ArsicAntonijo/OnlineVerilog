#!/bin/bash

HEALTHCHECK_URL="https://veron.onrender.com/Healthcheck"

timestamp=$(date -u +"%Y-%m-%dT%H:%M:%SZ")
response=$(curl -s -o /dev/null -w "%{http_code}" $HEALTHCHECK_URL)

echo "[$timestamp] Healthcheck passed with code [$response]"