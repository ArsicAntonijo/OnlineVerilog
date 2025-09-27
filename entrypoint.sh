#!/bin/bash

# Start background loop (every 2 min)
(
  while true; do
    /app/background-task.sh &
    sleep 120
  done
) &

# Start the ASP.NET Core app
exec dotnet OnlineVerilog.dll
