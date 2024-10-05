#!/bin/bash
dotnet test Mobit.Tests/Mobit.Tests.csproj
docker-compose up -d
curl --fail localhost:5080
