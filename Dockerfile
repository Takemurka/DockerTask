# Base image to use
FROM mcr.microsoft.com/dotnet/runtime:6.0


# Copy the application binaries to the actual container /app directory
COPY bin/Release/net6.0 /app


# Make sure that the sync directory exists even if not mounted as a volume.
# The /sync directory can be mounted using docker run '-v' parameter
VOLUME /sync


# Start the program
CMD ["dotnet", "app/FileSystem.dll"]
