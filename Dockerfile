FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["./CSharp.AspNetCore.Spa.Vuejs.csproj", "app/"]
RUN dotnet restore "app/CSharp.AspNetCore.Spa.Vuejs.csproj"
COPY . .
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get install -y nodejs
RUN dotnet build "/src/CSharp.AspNetCore.Spa.Vuejs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/CSharp.AspNetCore.Spa.Vuejs.csproj" -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharp.AspNetCore.Spa.Vuejs.dll"]
