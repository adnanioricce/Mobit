#FROM node:20 AS build-angular


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Mobit.Web.csproj", "./"]
RUN dotnet restore "./Mobit.Web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Mobit.Web.csproj" -c Release -o /app/build

FROM build AS publish

RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs

COPY ./ClientApp/package*.json ./

RUN npm install

COPY ./ClientApp/ ./

RUN npm run build --prod
RUN dotnet publish "Mobit.Web.csproj" -c Release -r linux-x64 --self-contained false -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Mobit.Web.dll"]

