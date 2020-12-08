FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
EXPOSE 80
EXPOSE 443
WORKDIR /app
RUN apk update && \
    apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
ARG VERSION=1.0.0.0
COPY [".", "/build/"]
WORKDIR /build/
RUN dotnet restore PointOfSale/PointOfSale.csproj -s https://api.nuget.org/v3/index.json  && \
	dotnet restore PointOfSaleServiceTests/PointOfSaleServiceTests.csproj -s https://api.nuget.org/v3/index.json  && \
    dotnet publish -c Release -o /app --no-restore /p:Version=$VERSION

FROM base AS final
COPY --from=build /app .
ENTRYPOINT ["dotnet", "PointOfSale.dll"]