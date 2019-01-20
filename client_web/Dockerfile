FROM node:10.15.0 as node
WORKDIR /usr/src/app
COPY ./*.json ./
RUN npm install
RUN npm install -g @angular/cli
COPY . /usr/src/app
EXPOSE 4200/tcp
CMD ng serve --host 0.0.0.0