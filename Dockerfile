# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and restore dependencies
COPY ReceiptReimbursementAPI.sln ./
COPY ReceiptReimbursementAPI.API/*.csproj ./ReceiptReimbursementAPI.API/
COPY ReceiptReimbursementAPI.Application/*.csproj ./ReceiptReimbursementAPI.Application/
COPY ReceiptReimbursementAPI.Data/*.csproj ./ReceiptReimbursementAPI.Data/
COPY ReceiptReimbursementAPI.Model/*.csproj ./ReceiptReimbursementAPI.Model/

RUN dotnet restore

COPY . ./
RUN dotnet publish ReceiptReimbursementAPI.API/ReceiptReimbursementAPI.API.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "ReceiptReimbursementAPI.API.dll"]