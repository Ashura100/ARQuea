# **ARQuea**

![0sCeIo](https://github.com/user-attachments/assets/a20bd9b7-8563-49d5-a8cb-f4ae262beca0)

## **Introduction**

![AUZ8eY](https://github.com/user-attachments/assets/9ecc48ac-047d-4f75-b48d-d055a47fc093)

- Objectif de l'application
- Explication de son fonctionnement
- Démonstration
- Conclusion

## **Objectifs de l'application**

Créer une application mobile style Ikea sur Unity, comportant des ScriptableObjects et un système d'AR (réalité augmenté).                  
L'application doit avoir un catalogue d'objets, minimum 3 objets fonctionnels et le système d'AR doit permettre de les positionner.
Les ScriptableObjects devront contenir les informations des articles/Objets.

## **Explication**

![ARQueaScreens](https://github.com/user-attachments/assets/c2d5fdf9-b6bd-428f-a59f-e600611d770b)

 - **UIToolkit**:

Les Interfaces utilisateurs ont été fait avec _**l'UI Toolkit**_ de _**Unity**_, les changements d'UI se font avec la barre de boutons en bas des écrans (cf les images) et/ou quand une selection d'article est faites (pour la partie categorie et article seulement).

- **AR Vuforia**:

Pour la partie AR le _**SDK**_ de _**Vuforia**_ a été téléchargé et installé dans Unity, le positionnement des objets en AR se fait grâce à un _**groundPlane**_ qui permet la detection de surface plane.

- **LeanTouch**:

  L'asset _Lean Touch_ permet la détection des doigts sur le portable et donc de déplacer et changer la rotation des objets en fonction du nombre de doigts appuyés sur l'écran.

- **ScriptableObjects**

  Les articles sont sous formes de _**scriptableObjects**_, ils permetent de stocker et modifier facilement différentes données de chaque article.

## **Démonstration**

![ARQueaG](https://github.com/user-attachments/assets/8f827497-c5da-47bc-b7ad-f3fe484c4898)


## **Conclusion**

Pour Conclure, l'application est fonctionnelle, permet de choisir des catégories d'article et et les articles de la catégorie choisie, de les mettrent en AR et de les mettrent dans le panier ou non.

![true-romance-tony-scott](https://github.com/user-attachments/assets/6d7612a3-c492-4003-ad20-21d691f44156)

Lien APK : https://drive.google.com/file/d/1Oj4hhIfsqHLJNxeLRtNdWrNuVCEQ1Bmx/view?usp=drive_link

Lien PHP : https://drive.google.com/drive/folders/1p5H2xrXtLDpQ0qYvktaI0sJBYothUJEZ?usp=drive_link

lien SQL : https://drive.google.com/file/d/1svjZYBV9GL9si7WDXMKejUqCkDKsaF-q/view?usp=drive_link

#**Installation Base de Donnée**

Pour installer la base de donnée cliqué sur votre application **Wamp**, puis **PHPMyAdmin**, créer une base de donnée en **latin_swedish** nommer la du nom que vous vuoulez, puis drag and drop le fichier DB.

#**Installation du PHP**

Pour installer les fichier Php, aller dans votre **Explorateur de fichier**, puis dans votre dossier **wamp64**, ensuite le dossier **www**, créer un dossier avec le nom que vous voulez, mettez les fichiers Php dedans.
