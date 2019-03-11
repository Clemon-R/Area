FROM node
WORKDIR /usr/src/app
RUN npm install -g @angular/cli
COPY ./*.json ./
RUN npm install
COPY . /usr/src/app
EXPOSE 4200
ENTRYPOINT npm start