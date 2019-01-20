# Utiliser une runtime officielle de Python comme image d'origine
FROM python:2.7-slim

# Définir le répertoire de travail de l'applicaiton à /app
WORKDIR /app

# Copier le contenu du répertoire courant dans le conteneur à l'adresse /app
ADD . /app

# Installer tous les packets nécessaires définis dans le fichier requirements.txt
RUN pip install -r requirements.txt

# Rendre le port 80 accessible à l'extérieur du conteneur
EXPOSE 81

# Définir une variable d'environnement
ENV NAME World

# Lancer app.py quand le conteneur s'exécute
CMD ["python", "app.py"]