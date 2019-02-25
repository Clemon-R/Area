FROM node
WORKDIR /usr/src/app
COPY ./*.json ./
RUN npm install
RUN npm install -g @angular/cli
COPY . /usr/src/app
EXPOSE 4200
ENTRYPOINT npm start