FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

RUN echo 'America/Sao_Paulo' > /etc/timezone
RUN dpkg-reconfigure -f noninteractive tzdata

# Copy everything else and build
COPY . ./
RUN dotnet restore backend/RegisterProductsService/RegisterProductsService.sln
RUN dotnet publish backend/RegisterProductsService/RegisterProductsService.sln -c Release -o /out
#COPY Repository.EF/Migrations/Scripts/. /out

# Copy everything else and build
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
ARG COMPOSE_PROJECT_NAME
WORKDIR /app
COPY --from=build-env /out .
EXPOSE 5001

ENTRYPOINT ["dotnet", "Api.dll"]