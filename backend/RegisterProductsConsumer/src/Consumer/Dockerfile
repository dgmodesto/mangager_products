#Imagem de execucao
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

#Imagem para build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

#Define o diretorio de trabalho
WORKDIR /RegisterProductsConsumer

#Copia tudo para pasta do workdir, faz o restore e copia os pacotes
COPY . .
RUN dotnet restore backend/RegisterProductsConsumer/RegisterProductsConsumer.sln
COPY . .

#Define o diretorio de trabalho sobre o projeto
RUN dotnet build backend/RegisterProductsConsumer/src/Consumer/Consumer.csproj -c Release -o /app

#Pela imagem de build, gera o deploy
FROM build AS publish
RUN dotnet publish backend/RegisterProductsConsumer/src/Consumer/Consumer.csproj -c Release -o /app

#Copia o deploy para imagem base
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Consumer.dll"]
