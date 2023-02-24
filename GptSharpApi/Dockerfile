FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GptSharp/GptSharp.csproj", "GptSharp/"]
RUN dotnet restore "GptSharp/GptSharp.csproj"
COPY . .
WORKDIR "/src/GptSharp"
RUN dotnet build "GptSharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GptSharp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GptSharp.dll"]
