#Use a imagem base do .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

#Copie todos os arquivos primeiro
COPY . .

#Faça restore e build
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

#Use a imagem runtime mais leve para produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

#Crie um usuário não-root para segurança
RUN groupadd -r appuser && useradd -r -g appuser appuser

#Copie os arquivos publicados do estágio de build
COPY --from=build /app/publish .

#Configure as variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:8080/
ENV ASPNETCORE_ENVIRONMENT=Production

#Exponha a porta
EXPOSE 8080

#Mude para o usuário não-root
USER appuser

#Configure o ponto de entrada
ENTRYPOINT ["dotnet", "LivrariaService.dll"]