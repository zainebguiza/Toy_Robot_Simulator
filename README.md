# Simulateur de Robot-Jouet

Une implémentation .NET 8 du kata _Toy Robot Simulator_, réalisée selon les principes de l’architecture propre (Clean Architecture) et les patterns SOLID.

## Vue d’ensemble

Cette application simule le déplacement d’un robot-jouet sur une table carrée de 5 unités × 5 unités.  
Le robot peut se déplacer librement tant qu’il ne tombe pas du plateau.

## Architecture

Organisation du code :

```
├── src/
│   ├── ToyRobotSimulator.Domain/          # Logique métier et entités
│   ├── ToyRobotSimulator.Application/     # Cas d’usage et services
│   ├── ToyRobotSimulator.Infrastructure/  # Accès externes (fichiers, etc.)
│   └── ToyRobotSimulator.Console/         # Point d’entrée en ligne de commande
└── tests/
    ├── ToyRobotSimulator.UnitTests/        # Tests unitaires
```

### Couche Domaine

- **Entités :** `Robot`, `Table`
- **Value Object :** `Position`
- **Enum :** `Direction`
- **Interfaces :** `IRobotSimulator`, `ICommandParser`

### Couche Application

- **Services :** `RobotSimulatorService`, `CommandExecutorService`

### Couche Infrastructure

- **Parsers :** `CommandParser`

## Commandes prises en charge

- `PLACE X,Y,F` : place le robot en (X,Y) orienté vers F (`NORTH`, `SOUTH`, `EAST`, `WEST`)
- `MOVE` : avance d’une unité dans la direction courante
- `LEFT` : tourne de 90° vers la gauche
- `RIGHT` : tourne de 90° vers la droite
- `REPORT` : affiche la position et la direction actuelles

## Utilisation

### Mode interactif

```bash
dotnet run --project src/ToyRobotSimulator.Console
```

### Mode fichier

```bash
dotnet run --project src/ToyRobotSimulator.Console chemin/vers/commands.txt
```

### Compilation

```bash
dotnet build
```

### Lancement des tests

```bash
dotnet test
```

## Exemples

### Exemple A

```
PLACE 0,0,NORTH
MOVE
REPORT
```

Sortie attendue : `0,1,NORTH`

### Exemple B

```
PLACE 0,0,NORTH
LEFT
REPORT
```

Sortie attendue : `0,0,WEST`

### Exemple C

```
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
```

Sortie attendue : `3,3,NORTH`

## Principes de conception

### SOLID

- **Responsabilité unique** : chaque classe a un but clair
- **Ouvert/Fermé** : extensible sans modification interne
- **Substitution de Liskov** : les dérivés remplacent les bases sans surprise
- **Séparation d’interface** : interfaces petites et ciblées
- **Inversion de dépendance** : le haut niveau ne dépend pas du bas niveau

### Bonnes pratiques

- Noms explicites
- Méthodes courtes et focalisées
- Tests unitaires étendus
- Commentaires pour expliquer le _pourquoi_
- Formatage cohérent

### Gestion des erreurs

- Commandes invalides ignorées
- Le robot ne peut pas tomber
- Les commandes avant le premier `PLACE` sont ignorées
- Validation stricte des entrées

## Tests

- **26 tests unitaires** couvrant toute la logique
- Tests d’entités `Robot` et `Table`
- Tests des services avec tous les exemples du kata
- Vérification des limites du plateau
- Fichiers de données pour tests d’intégration

## Points clés

- Architecture propre
- Respect de SOLID
- Couverture de tests complète
- Entrée interactive ou par fichier
- Gestion d’erreurs robuste
- Conformité totale aux exigences du kata

## Dépendances

- .NET 8.0
