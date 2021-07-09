#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["Mini.Web/Mini.Web.csproj", "Mini.Web/"]
#COPY ["Mini.Common/Mini.Common.csproj", "Mini.Common/"]
#COPY ["Mini.Entity/Mini.Model.csproj", "Mini.Entity/"]
#COPY ["Mini.service/Mini.Services.csproj", "Mini.service/"]
#COPY ["Mini.Repository/Mini.Repository.csproj", "Mini.Repository/"]
#COPY ["Mini.IServives/Mini.IServives.csproj", "Mini.IServives/"]
#COPY ["Mini.Extensions/Mini.Extensions.csproj", "Mini.Extensions/"]
#RUN dotnet restore "Mini.Web/Mini.Web.csproj"
COPY . .
#WORKDIR "/src/Mini.Web"
#RUN dotnet build "Mini.Web.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Mini.Web.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mini.Web.dll"]

