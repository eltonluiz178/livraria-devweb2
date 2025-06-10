# Use a imagem base do Node.js (versão Alpine para leveza)
FROM node:22-alpine

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos de dependências primeiro (para otimizar cache)
COPY package*.json ./

# Instale as dependências
RUN npm install

# Copie o código fonte
COPY . .

# Execute o build do Angular (gera os arquivos na pasta dist)
RUN npm run build

# Instale o http-server globalmente
RUN npm install -g http-server

# Exponha a porta 4200
EXPOSE 4200

# Comando para iniciar o http-server apontando para a pasta dist
CMD ["http-server", "-p", "4200", "dist/livraria-virtual/browser"]
