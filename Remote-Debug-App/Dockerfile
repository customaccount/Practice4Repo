#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

RUN mkdir -p /home/LogFiles /opt/startup \
     && echo "root:Docker!" | chpasswd \
     && echo "cd /home" >> /etc/bash.bashrc \
     && apt-get update \  
     && apt-get install --yes --no-install-recommends openssh-server vim curl wget tcptraceroute openrc

RUN rm -f /etc/ssh/sshd_config
RUN mkdir -p /tmp
COPY ./Remote-Debug-App/sshd_config /etc/ssh/sshd_config

COPY ./Remote-Debug-App/ssh_setup.sh /tmp/ssh_setup.sh

RUN chmod +x /tmp/ssh_setup.sh 
RUN ls -la /tmp
RUN /tmp/ssh_setup.sh 
RUN rm -rf /tmp/* 

ENV SSH_PORT 2222
EXPOSE 2222

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Remote-Debug-App/Remote-Debug-App.csproj", "Remote-Debug-App/"]
RUN dotnet restore "Remote-Debug-App/Remote-Debug-App.csproj"

COPY . .
WORKDIR "/src/Remote-Debug-App"
RUN dotnet build "Remote-Debug-App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Remote-Debug-App.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


COPY ./Remote-Debug-App/launch.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/launch.sh

ENTRYPOINT ["bash","launch.sh"]