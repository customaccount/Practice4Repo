#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM trainingazure/asp.net:6.0.25 AS base

RUN mkdir -p /home/LogFiles /opt/startup \
     && echo "root:Docker!" | chpasswd \
     && echo "cd /home" >> /etc/bash.bashrc \
     && apt-get update \  
     && apt-get install --yes --no-install-recommends openssh-server vim curl wget tcptraceroute 

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

FROM trainingazure/dotnet:6.0.25 AS build
WORKDIR /src
COPY ["Remote-Debug-App/Remote-Debug-App.csproj", "Remote-Debug-App/"]
RUN dotnet restore "Remote-Debug-App/Remote-Debug-App.csproj"

COPY . .
WORKDIR "/src/Remote-Debug-App"
RUN dotnet build "Remote-Debug-App.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Remote-Debug-App.csproj" -c Debug -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


COPY ./Remote-Debug-App/launch.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/launch.sh

RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb

RUN curl -sL https://aka.ms/InstallAzureCLIDeb | bash
RUN  apt-get update \
     && apt-get install --yes dotnet-sdk-8.0 \
     && apt-get install --yes --no-install-recommends  ca-certificates curl apt-transport-https lsb-release gnupg \
     && dotnet tool install --global dotnet-dump

RUN curl -sL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor | tee /etc/apt/trusted.gpg.d/microsoft.gpg > /dev/null


ENV AZ_REPO $(lsb_release -cs)
RUN "deb [arch=amd64] https://packages.microsoft.com/repos/azure-cli/ $AZ_REPO main" | tee /etc/apt/sources.list.d/azure-cli.list

RUN apt-get update \
    && apt-get install --yes --no-install-recommends azure-cli

ENTRYPOINT ["bash","launch.sh"]