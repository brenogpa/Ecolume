FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.sln .
COPY Ecolume.Application/*.csproj ./Ecolume.Application/
COPY Ecolume.Infrastructure/*.csproj ./Ecolume.Infrastructure/
COPY Ecolume.Domain/*.csproj ./Ecolume.Domain/

RUN dotnet restore

COPY . .

RUN dotnet publish Ecolume.Application -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "Ecolume.Application.dll"]
